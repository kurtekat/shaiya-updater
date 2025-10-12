using Parsec.Shaiya.Data;
using System.IO;

namespace Updater.Data
{
    public static class DataBuilder
    {
        private static BinaryReader? _safReader;
        private static BinaryWriter? _safWriter;

        /// <summary>
        /// Builds a new archive at the specified location.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="destDirName"></param>
        /// <param name="progressCallback"></param>
        public static void Build(Parsec.Shaiya.Data.Data data, string destDirName, Action? progressCallback = null)
        {
            try
            {
                var directoryInfo = Directory.CreateDirectory(destDirName);
                var sah = new Sah(Path.Combine(directoryInfo.FullName, "data.sah"), new SDirectory(), 0);
                var saf = new Saf(Path.Combine(directoryInfo.FullName, "data.saf"));

                _safReader = new BinaryReader(File.OpenRead(data.Saf.Path));
                _safWriter = new BinaryWriter(File.OpenWrite(saf.Path));

                LocalFunction(data.Sah.RootDirectory);
                data.Sah.Write(sah.Path);

                _safReader.Close();
                _safWriter.Close();
            }
            finally
            {
                _safReader?.Dispose();
                _safReader = null;
                _safWriter?.Dispose();
                _safWriter = null;
            }

            void LocalFunction(SDirectory currentFolder)
            {
                var files = currentFolder.Files.Values;
                foreach (var file in files)
                {
                    var bytes = ReadBytes(file.Offset, file.Length);
                    file.Offset = WriteBytes(bytes);
                    progressCallback?.Invoke();
                }

                var subfolders = currentFolder.Directories.Values;
                foreach (var subfolder in subfolders)
                    LocalFunction(subfolder);
            }
        }

        /// <summary>
        /// Reads bytes from the archive.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns>The bytes read from the archive.</returns>
        private static byte[] ReadBytes(long offset, int length)
        {
            if (_safReader == null)
                throw new InvalidOperationException();

            _safReader.BaseStream.Seek(offset, SeekOrigin.Begin);
            return _safReader.ReadBytes(length);
        }

        /// <summary>
        /// Appends bytes to the archive.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>The offset where the bytes were written to the archive.</returns>
        private static long WriteBytes(byte[] bytes)
        {
            if (_safWriter == null)
                throw new InvalidOperationException();

            _safWriter.BaseStream.Seek(_safWriter.BaseStream.Length, SeekOrigin.Begin);
            var offset = _safWriter.BaseStream.Position;
            _safWriter.Write(bytes);
            return offset;
        }
    }
}

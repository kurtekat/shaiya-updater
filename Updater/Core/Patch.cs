using System.IO;
using System.IO.Compression;
using Updater.Common;

namespace Updater.Core
{
    /// <summary>
    /// Represents a zip archive with a .patch extension.
    /// </summary>
    public class Patch
    {
        public readonly string FileName = string.Empty;
        public readonly string Path = string.Empty;
        public readonly string Url = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Patch"/> class with the <paramref name="version"/> parameter.
        /// </summary>
        /// <param name="version">The patch version number.</param>
        /// <exception cref="ArgumentException">The specified version is less than 2 or greater than 9999.</exception>
        public Patch(uint version)
        {
            if (version < 2 || version > 9999)
                throw new ArgumentException(null, nameof(version));

            // e.g., ps0002.patch
            FileName = $"ps{version:D4}.patch";
            Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), FileName);
            Url = $"{Constants.Source}/shaiya/patch/{FileName}";
        }

        /// <summary>
        /// Deletes the patch file.
        /// </summary>
        public void Delete()
        {
            if (Exists())
                File.Delete(Path);
        }

        /// <summary>
        /// Determines whether the patch file exists.
        /// </summary>
        /// <returns></returns>
        public bool Exists() => File.Exists(Path);

        /// <summary>
        /// Extracts all of the files in the archive to the current working directory of the application.
        /// </summary>
        /// <returns><see langword="false"/> if the file does not exist or an exception is caught. Otherwise, <see langword="true"/>.</returns>
        public bool ExtractToCurrentDirectory()
        {
            if (Exists())
            {
                try
                {
                    using (var zipArchive = ZipFile.OpenRead(Path))
                        zipArchive.ExtractToDirectory(Directory.GetCurrentDirectory(), true);

                    return true;
                }
                catch { }
            }

            return false;
        }
    }
}

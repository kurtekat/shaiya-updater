using System.IO;
using System.IO.Compression;
using Updater.Common;

namespace Updater.Core
{
    /// <summary>
    /// Represents a zip archive with a .patch extension.
    /// </summary>
    public sealed class Patch
    {
        public readonly string FileName = string.Empty;
        public readonly string Path = string.Empty;
        public readonly string Url = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Patch"/> class.
        /// </summary>
        /// <param name="version">The patch version number.</param>
        /// <exception cref="ArgumentException"></exception>
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
        /// Extracts all of the files in the archive to the current working directory of the application.
        /// </summary>
        /// <returns>A value that indicates whether the operation was successful.</returns>
        public bool ExtractToCurrentDirectory()
        {
            try
            {
                using (var zipArchive = ZipFile.OpenRead(Path))
                    zipArchive.ExtractToDirectory(Directory.GetCurrentDirectory(), true);

                return true;
            }
            catch { }
            return false;
        }
    }
}

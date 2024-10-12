using System.IO;

namespace Updater.Core
{
    public class Patch
    {
        public readonly string FileName = string.Empty;
        public readonly string Path = string.Empty;
        public readonly string Url = string.Empty;

        public Patch(uint version)
        {
            const uint minVersion = 2;
            const uint maxVersion = 9999;

            if (version > maxVersion)
                throw new ArgumentOutOfRangeException(nameof(version));

            if (version < minVersion)
                version = minVersion;

            // e.g., ps0002.patch
            FileName = $"ps{version:D4}.patch";
            Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), FileName);
            Url = $"{Constants.Source}/shaiya/patch/{FileName}";
        }
    }
}

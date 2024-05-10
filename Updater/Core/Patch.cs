using System.IO;
using Updater.Common;

namespace Updater.Core
{
    public class Patch
    {
        private const uint MaxVersion = 9999;
        public readonly string FileName = string.Empty;
        public readonly string Path = string.Empty;
        public readonly string Url = string.Empty;

        public Patch(uint version)
        {
            try
            {
                if (version > MaxVersion)
                    throw new ArgumentOutOfRangeException(nameof(version), version, $"Expected <= {MaxVersion}");

                // e.g., ps0002.patch
                FileName = $"ps{version:D4}.patch";
                Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), FileName);
                Url = $"{Constants.Source}/shaiya/patch/{FileName}";
            }
            catch (Exception ex)
            {
                var log = new Log();
                log.Write(ex.ToString());
            }
        }
    }
}

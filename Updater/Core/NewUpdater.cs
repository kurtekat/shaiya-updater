using System.IO;
using Updater.Common;

namespace Updater.Core
{
    public class NewUpdater
    {
        public const string FileName = "new_updater.exe";
        public readonly string Path = string.Empty;
        public readonly string Url = string.Empty;

        public NewUpdater()
        {
            Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), FileName);
            Url = $"{Constants.Source}/shaiya/{FileName}";
        }
    }
}

using System.IO;
using Updater.Helpers;
using Updater.Imports;

namespace Updater.Core
{
    /// <summary>
    /// Represents the client-side configuration file.
    /// </summary>
    public class ClientConfiguration
    {
        public const string FileName = "Version.ini";
        public readonly string Path = string.Empty;
        public uint CheckVersion { get; }
        public uint CurrentVersion { get; set; } = 1;
        public string StartUpdate { get; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConfiguration"/> class.
        /// </summary>
        public ClientConfiguration()
        {
            Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), FileName);
            CheckVersion = Kernel32.GetPrivateProfileIntW("Version", "CheckVersion", 0, Path);
            CurrentVersion = Kernel32.GetPrivateProfileIntW("Version", "CurrentVersion", 1, Path);
            StartUpdate = IniHelper.GetPrivateProfileString("Version", "StartUpdate", "", Path);
        }
    }
}

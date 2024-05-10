using System.IO;
using Updater.Common;

namespace Updater.Core
{
    /// <summary>
    /// Represents the client-side configuration file.
    /// </summary>
    public class ClientConfiguration
    {
        private const string FileName = "Version.ini";
        private readonly string Path = string.Empty;
        public uint CheckVersion { get; } = 0;
        public uint CurrentVersion { get; set; } = 1;
        public string StartUpdate { get; } = string.Empty;

        public ClientConfiguration()
        {
            try
            {
                // Microsoft: If this parameter does not contain a full path to the file,
                // the system searches for the file in the Windows directory.
                Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), FileName);
                CheckVersion = Win32.GetPrivateProfileIntW("Version", "CheckVersion", 0, Path);
                CurrentVersion = Win32.GetPrivateProfileIntW("Version", "CurrentVersion", 1, Path);
                StartUpdate = Util.GetPrivateProfileString("Version", "StartUpdate", "", Path);
            }
            catch (Exception ex)
            {
                var log = new Log();
                log.Write(ex.ToString());
            }
        }

        public int Write(string key, string value)
        {
            return Win32.WritePrivateProfileStringW("Version", key, value, Path);
        }
    }
}

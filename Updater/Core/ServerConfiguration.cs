using System.IO;
using System.Net.Http;
using Updater.Common;

namespace Updater.Core
{
    /// <summary>
    /// Represents the server-side configuration file.
    /// </summary>
    public class ServerConfiguration
    {
        public const string FileName = "UpdateVersion.ini";
        private readonly string Url = string.Empty;
        public uint CheckVersion { get; } = 0;
        public uint PatchFileVersion { get; } = 2;
        public uint UpdaterVersion { get; } = 0;

        public ServerConfiguration(HttpClient httpClient)
        {
            try
            {
                Url = $"{Constants.Source}/shaiya/{FileName}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), FileName);
                Util.DownloadToFile(httpClient, Url, path);

                if (File.Exists(path))
                {
                    CheckVersion = Win32.GetPrivateProfileIntW("Version", "CheckVersion", 0, path);
                    PatchFileVersion = Win32.GetPrivateProfileIntW("Version", "PatchFileVersion", 2, path);
                    UpdaterVersion = Win32.GetPrivateProfileIntW("Version", "UpdaterVersion", 0, path);
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                var log = new Log();
                log.Write(ex.ToString());
            }
        }
    }
}

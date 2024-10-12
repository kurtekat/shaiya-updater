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
        public uint CheckVersion { get; } = 0;
        public uint PatchFileVersion { get; } = 2;
        public uint UpdaterVersion { get; } = 0;

        public ServerConfiguration(HttpClient httpClient)
        {
            var requestUri = $"{Constants.Source}/shaiya/{FileName}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), FileName);
            Util.DownloadToFile(httpClient, requestUri, path);

            if (File.Exists(path))
            {
                CheckVersion = Win32.GetPrivateProfileIntW("Version", "CheckVersion", 0, path);
                PatchFileVersion = Win32.GetPrivateProfileIntW("Version", "PatchFileVersion", 2, path);
                UpdaterVersion = Win32.GetPrivateProfileIntW("Version", "UpdaterVersion", 0, path);
                File.Delete(path);
            }
        }
    }
}

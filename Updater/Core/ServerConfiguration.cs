using System.IO;
using System.Net.Http;
using Updater.Common;
using Updater.Imports;

namespace Updater.Core
{
    /// <summary>
    /// Represents the server-side configuration file.
    /// </summary>
    public class ServerConfiguration
    {
        public const string FileName = "UpdateVersion.ini";
        public uint CheckVersion { get; }
        public uint PatchFileVersion { get; } = 2;
        public uint UpdaterVersion { get; }

        public ServerConfiguration(HttpClient httpClient)
        {
            var requestUri = $"{Constants.Source}/shaiya/{FileName}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), FileName);
            Util.DownloadToFile(httpClient, requestUri, path);

            if (File.Exists(path))
            {
                CheckVersion = Kernel32.GetPrivateProfileIntW("Version", "CheckVersion", 0, path);
                PatchFileVersion = Kernel32.GetPrivateProfileIntW("Version", "PatchFileVersion", 2, path);
                UpdaterVersion = Kernel32.GetPrivateProfileIntW("Version", "UpdaterVersion", 0, path);
                File.Delete(path);
            }
        }
    }
}

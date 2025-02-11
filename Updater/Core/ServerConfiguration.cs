using System.IO;
using System.Net.Http;
using Updater.Common;
using Updater.Extensions;

namespace Updater.Core
{
    /// <summary>
    /// Represents the server-side configuration file.
    /// </summary>
    public sealed class ServerConfiguration
    {
        public const string FileName = "UpdateVersion.ini";
        public uint CheckVersion { get; }
        public uint PatchFileVersion { get; } = 2;
        public uint UpdaterVersion { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConfiguration"/> class with the <paramref name="httpClient"/> parameter.
        /// </summary>
        /// <param name="httpClient"></param>
        public ServerConfiguration(HttpClient httpClient)
        {
            var requestUri = $"{Constants.Source}/shaiya/{FileName}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), FileName);
            httpClient.DownloadFile(requestUri, path);

            if (File.Exists(path))
            {
                CheckVersion = DllImport.GetPrivateProfileIntW("Version", "CheckVersion", 0, path);
                PatchFileVersion = DllImport.GetPrivateProfileIntW("Version", "PatchFileVersion", 2, path);
                UpdaterVersion = DllImport.GetPrivateProfileIntW("Version", "UpdaterVersion", 0, path);
                File.Delete(path);
            }
        }
    }
}

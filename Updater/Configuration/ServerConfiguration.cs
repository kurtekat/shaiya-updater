using Microsoft.Extensions.Configuration.Ini;
using System.Collections.Immutable;
using System.IO;
using System.Net.Http;
using Updater.Common;
using Updater.Extensions;

namespace Updater.Configuration
{
    /// <summary>
    /// Represents the server-side configuration file.
    /// </summary>
    public sealed class ServerConfiguration : IniConfigurationProvider
    {
        public const string FileName = "UpdateVersion.ini";
        public int CheckVersion { get; }
        public int PatchFileVersion { get; } = 2;
        public int UpdaterVersion { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConfiguration"/> class.
        /// </summary>
        public ServerConfiguration(HttpClient httpClient) : base(new IniConfigurationSource())
        {
            Source.Path = Path.Combine(Directory.GetCurrentDirectory(), FileName);
            Source.ResolveFileProvider();

            var requestUri = $"{Constants.Source}/shaiya/{FileName}";
            httpClient.DownloadFile(requestUri, Source.Path);

            if (File.Exists(Source.Path))
            {
                Load();

                var data = Data.ToImmutableDictionary();
                CheckVersion = Convert.ToInt32(data.GetValueOrDefault("Version:CheckVersion"));
                PatchFileVersion = Convert.ToInt32(data.GetValueOrDefault("Version:PatchFileVersion", "2"));
                UpdaterVersion = Convert.ToInt32(data.GetValueOrDefault("Version:UpdaterVersion"));
                File.Delete(Source.Path);
            }
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using System.Collections.Immutable;
using System.IO;

namespace Updater.Configuration
{
    /// <summary>
    /// Represents the client-side configuration file.
    /// </summary>
    public sealed class ClientConfiguration : IniConfigurationProvider
    {
        public const string FileName = "Version.ini";
        private int _checkVersion;
        private int _currentVersion;
        private string? _startUpdate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConfiguration"/> class.
        /// </summary>
        public ClientConfiguration() : base(new IniConfigurationSource())
        {
            Source.Path = Path.Combine(Directory.GetCurrentDirectory(), FileName);
            Source.ResolveFileProvider();

            if (File.Exists(Source.Path))
                Load();

            var data = Data.ToImmutableDictionary();
            _checkVersion = Convert.ToInt32(data.GetValueOrDefault("Version:CheckVersion"));
            _currentVersion = Convert.ToInt32(data.GetValueOrDefault("Version:CurrentVersion", "1"));
            _startUpdate = data.GetValueOrDefault("Version:StartUpdate");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void Save()
        {
            if (string.IsNullOrEmpty(Source.Path))
                throw new InvalidOperationException();

            var contents = new List<string>();
            var sections = GetChildKeys(Enumerable.Empty<string>(), null).Distinct();

            foreach (var section in sections)
            {
                contents.Add($"[{section}]");
                var pairs = Data.Where(pair => pair.Key.StartsWith(section))
                  .Select(pair => $"{ConfigurationPath.GetSectionKey(pair.Key)}={pair.Value}");
                contents.AddRange(pairs);
            }

            File.WriteAllLines(Source.Path, contents);
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public int CheckVersion
        {
            get => _checkVersion;
            set
            {
                _checkVersion = value;
                Set("Version:CheckVersion", _checkVersion.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the current version of the client.
        /// </summary>
        public int CurrentVersion
        {
            get => _currentVersion;
            set
            {
                _currentVersion = value;
                Set("Version:CurrentVersion", _currentVersion.ToString());
            }
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public string? StartUpdate
        {
            get => _startUpdate;
            set
            {
                _startUpdate = value;
                Set("Version:StartUpdate", _startUpdate);
            }
        }
    }
}

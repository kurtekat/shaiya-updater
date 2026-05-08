using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Beware of trailing comments.
//
// [Section] ; Not OK
// Key=Value ; Not OK
//
// ; OK
// [Section]
// ; OK
// Key=Value

namespace Updater.Configuration
{
    public class Ini
    {
        private readonly Dictionary<string, string> _data;

        public Ini()
        {
            _data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo">The file to open for reading.</param>
        public void Parse(FileInfo fileInfo)
        {
            var rawData = File.ReadAllText(fileInfo.FullName);
            using (var reader = new StringReader(rawData))
                Parse(reader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData">A string containing all the text in the file.</param>
        public void Parse(string rawData)
        {
            using (var reader = new StringReader(rawData))
                Parse(reader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public void Parse(StringReader reader)
        {
            _data.Clear();

            var rawLine = string.Empty;
            var section = string.Empty;

            while ((rawLine = reader.ReadLine()) != null)
            {
                var line = rawLine.Trim();
                if (line.Length == 0)
                    continue;

                if (line.StartsWith(';') || line.StartsWith('#') || line.StartsWith('/'))
                    continue;

                if (line.StartsWith('[') && line.EndsWith(']'))
                {
                    section = line.Substring(1, line.Length - 2).Trim();
                    section += ':';
                    continue;
                }

                int index = line.IndexOf('=');
                if (index == -1)
                    continue;

                // { Section:Key, Value }
                var key = section + line.Substring(0, index).Trim();
                var value = line.Substring(index + 1).Trim();

                if (value.Length > 1 && value.StartsWith('"') && value.EndsWith('"'))
                    value = value.Substring(1, value.Length - 2);

                if (_data.ContainsKey(key))
                    continue;

                _data.Add(key, value);
            }
        }

        /// <summary>
        /// Writes the current <see cref="Ini"/> to the specified file.
        /// </summary>
        /// <param name="fileInfo">The file to write to.</param>
        public void Write(FileInfo fileInfo)
        {
            Write(fileInfo.FullName);
        }

        /// <summary>
        /// Writes the current <see cref="Ini"/> to the specified file.
        /// </summary>
        /// <param name="fileName">The file to write to.</param>
        public void Write(string fileName)
        {
            var contents = new List<string>();
            var sections = _data.Select(pair => GetFirstSegment(pair.Key)).Distinct();

            foreach (var section in sections)
            {
                contents.Add($"[{section}]");

                var pairs = new List<string>();
                foreach (var pair in _data)
                {
                    var segment = GetFirstSegment(pair.Key);
                    if (string.Compare(segment, section, true) != 0)
                        continue;

                    var key = GetLastSegment(pair.Key);
                    var value = pair.Value;
                    pairs.Add($"{key}={value}");
                }

                contents.AddRange(pairs);
            }

            File.WriteAllLines(fileName, contents);
        }

        /// <summary>
        /// Gets the value associated with the specified key, or a default value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string GetValueOrDefault(string key, string defaultValue)
        {
            if (!_data.TryGetValue(key, out var value))
                return defaultValue;

            return value ?? defaultValue;
        }

        /// <summary>
        /// Gets the value associated with the specified key, or a default value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int GetValueOrDefault(string key, int defaultValue)
        {
            if (!_data.TryGetValue(key, out var value))
                return defaultValue;

            if (value == null)
                return defaultValue;

            if (!int.TryParse(value, out int result))
                return defaultValue;

            return result;
        }

        /// <summary>
        /// Sets the value associated with the specified key, or creates a new element with the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetOrAddValue(string key, string value)
        {
            _data[key] = value;
        }

        /// <summary>
        /// Sets the value associated with the specified key, or creates a new element with the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetOrAddValue(string key, int value)
        {
            _data[key] = value.ToString();
        }

        private static string GetFirstSegment(string value)
        {
            int index = value.IndexOf(':');
            if (index == -1)
                return string.Empty;

            return value.Substring(0, index);
        }

        private static string GetLastSegment(string value)
        {
            int index = value.LastIndexOf(':');
            if (index == -1)
                return string.Empty;

            return value.Substring(index + 1);
        }
    }
}

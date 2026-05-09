using System;

namespace Updater.Configuration
{
    public static class Web
    {
        public const string Host = "kurtekat.github.io";

        /// <summary>
        /// Gets a URL containing the scheme (HTTPS), scheme delimiter, 
        /// and host components, without a trailing slash.
        /// </summary>
        public static string BaseUrl
        {
            get => $"{Uri.UriSchemeHttps}{Uri.SchemeDelimiter}{Host}";
        }
    }
}

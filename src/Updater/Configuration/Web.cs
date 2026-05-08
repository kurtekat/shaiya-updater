using System;

namespace Updater.Configuration
{
    public static class Web
    {
        public const string Host = "kurtekat.github.io";

        public static string BaseUrl
        {
            get => $"{Uri.UriSchemeHttps}{Uri.SchemeDelimiter}{Host}";
        }
    }
}

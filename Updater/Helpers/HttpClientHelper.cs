using System.IO;
using System.Net.Http;

namespace Updater.Helpers
{
    public static class HttpClientHelper
    {
        /// <summary>
        /// Downloads the resource with the specified URI to a local file.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestUri">The URI specified as a <see cref="String"/>, from which to download data.</param>
        /// <param name="fileName">The name of the local file that is to receive the data.</param>
        public static void DownloadFile(this HttpClient httpClient, string? requestUri, string fileName) =>
            DownloadFile(httpClient, CreateUri(requestUri), fileName);

        /// <summary>
        /// Downloads the resource with the specified URI to a local file.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestUri">The URI from which to download data.</param>
        /// <param name="fileName">The name of the local file that is to receive the data.</param>
        public static void DownloadFile(this HttpClient httpClient, Uri? requestUri, string fileName)
        {
            var bytes = httpClient.GetByteArrayAsync(requestUri).Result;
            File.WriteAllBytes(fileName, bytes);
        }

        private static Uri? CreateUri(string? uri) =>
            string.IsNullOrEmpty(uri) ? null : new Uri(uri, UriKind.RelativeOrAbsolute);
    }
}

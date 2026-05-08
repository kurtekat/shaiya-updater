using System;
using System.IO;
using System.Net.Http;

namespace Updater.Core.Extensions
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Downloads the resource with the specified URI to a local file.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestUri">The URI specified as a string, from which to download data.</param>
        /// <param name="fileName">The name of the local file that is to receive the data.</param>
        public static void DownloadFile(this HttpClient httpClient, string? requestUri, string fileName) 
            => httpClient.DownloadFile(CreateUri(requestUri), fileName);

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

        /// <summary>
        /// Downloads the requested resource as a string.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestUri">A string containing the URI to download.</param>
        /// <returns>A string containing the requested resource.</returns>
        public static string DownloadString(this HttpClient httpClient, string? requestUri) 
            => httpClient.DownloadString(CreateUri(requestUri));

        /// <summary>
        /// Downloads the requested resource as a string.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestUri">A <see cref="Uri"/> object containing the URI to download.</param>
        /// <returns>A string containing the requested resource.</returns>
        public static string DownloadString(this HttpClient httpClient, Uri? requestUri)
        {
            return httpClient.GetStringAsync(requestUri).Result;
        }

        private static Uri? CreateUri(string? uri)
        {
            return string.IsNullOrEmpty(uri)
            ? null
            : new Uri(uri, UriKind.RelativeOrAbsolute);
        }
    }
}

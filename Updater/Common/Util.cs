using System.IO;
using System.Net.Http;
using System.Windows.Media.Imaging;
using System.Windows;
using System.IO.Compression;

namespace Updater.Common
{
    public static class Util
    {
        public static string DownloadString(HttpClient httpClient, string requestUri)
        {
            try
            {
                using (var response = httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead).Result)
                    response.EnsureSuccessStatusCode();

                return httpClient.GetStringAsync(requestUri).Result;
            }
            catch (Exception ex)
            {
                var log = new Log();
                log.Write(ex.ToString());
                return string.Empty;
            }
        }

        public static void DownloadToFile(HttpClient httpClient, string requestUri, string path)
        {
            try
            {
                using (var response = httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead).Result)
                    response.EnsureSuccessStatusCode();

                using var stream = httpClient.GetStreamAsync(requestUri).Result;
                using var output = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                stream.CopyTo(output);
            }
            catch (Exception ex)
            {
                var log = new Log();
                log.Write(ex.ToString());
            }
        }

        public static BitmapImage BitmapImageFromManifestResource(string name)
        {
            var bitmapImage = new BitmapImage();

            try
            {
                using var stream = Application.ResourceAssembly.GetManifestResourceStream(name);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
            }
            catch (Exception ex)
            {
                var log = new Log();
                log.Write(ex.ToString());
            }

            return bitmapImage;
        }

        public static string GetPrivateProfileString(string? section, string? key, string? defaultValue, string fileName)
        {
            var buffer = new char[short.MaxValue];
            var length = Win32.GetPrivateProfileStringW(section, key, defaultValue, buffer, (uint)buffer.Length, fileName);
            return new string(buffer, 0, (int)length);
        }

        public static int ExtractZipFile(string archiveFileName, string destinationDirectoryName)
        {
            try
            {
                using var archive = ZipFile.OpenRead(archiveFileName);
                foreach (var entry in archive.Entries)
                {
                    var destinationFileName = Path.Combine(destinationDirectoryName, entry.Name);
                    entry.ExtractToFile(destinationFileName, true);
                }

                return 0;
            }
            catch (Exception ex)
            {
                var log = new Log();
                log.Write(ex.ToString());
                return -1;
            }
        }
    }
}

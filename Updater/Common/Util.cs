using System.IO;
using System.Net.Http;
using System.IO.Compression;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Updater.Common
{
    public static class Util
    {
        public static string DownloadString(HttpClient httpClient, string requestUri)
        {
            try
            {
                using var response = httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead).Result;
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
                using var response = httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead).Result;
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

        public static int ExtractZipFile(string fileName)
        {
            try
            {
                using var archive = ZipFile.OpenRead(fileName);
                foreach (var entry in archive.Entries)
                    ZipFileExtensions.ExtractToFile(entry, entry.FullName, true);

                return 0;
            }
            catch (Exception ex)
            {
                var log = new Log();
                log.Write(ex.ToString());
                return -1;
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

        public static string GetPrivateProfileString(string? section, string? key, string? _default, string fileName)
        {
            var output = new char[byte.MaxValue];
            var count = Win32.GetPrivateProfileStringW(section, key, _default, output, (uint)output.Length, fileName);
            return new string(output, 0, (int)count);
        }

        public static bool WindowExists(string? className, string? windowName)
        {
            return Win32.FindWindowW(className, windowName) != IntPtr.Zero;
        }
    }
}

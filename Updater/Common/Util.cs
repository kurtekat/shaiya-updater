using System.IO;
using System.Net.Http;
using System.IO.Compression;
using System.Windows.Controls;
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

        public static Image? GetImageFromResource(string name, int width, int height)
        {
            try
            {
                using var stream = Application.ResourceAssembly.GetManifestResourceStream(name);
                if (stream is null)
                    return null;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                stream.Seek(0, SeekOrigin.Begin);
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();

                var image = new Image
                {
                    Height = height,
                    Width = width,
                    Source = bitmapImage
                };

                return image;
            }
            catch (Exception ex)
            {
                var log = new Log();
                log.Write(ex.ToString());
                return null;
            }
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

using System.Windows.Media.Imaging;
using System.Windows;
using Updater.Imports;

namespace Updater.Common
{
    public static class Util
    {
        public static BitmapImage? BitmapImageFromManifestResource(string name)
        {
            try
            {
                var bitmapImage = new BitmapImage();
                using (var stream = Application.ResourceAssembly.GetManifestResourceStream(name))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                }

                return bitmapImage;
            }
            catch (Exception ex)
            {
                var caption = Application.ResourceAssembly.GetName().Name;
                MessageBox.Show(ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static string GetPrivateProfileString(string? section, string? key, string? defaultValue, string fileName)
        {
            var buffer = new char[short.MaxValue];
            var length = Kernel32.GetPrivateProfileStringW(section, key, defaultValue, buffer, (uint)buffer.Length, fileName);
            return new string(buffer, 0, (int)length);
        }
    }
}

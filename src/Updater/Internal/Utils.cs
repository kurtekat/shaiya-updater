using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Updater.Internal
{
    internal static class Utils
    {
        internal static Image? ImageFromManifestResource(string name)
        {
            try
            {
                using (var stream = Application.ResourceAssembly.GetManifestResourceStream(name))
                {
                    if (stream == null)
                        return null;

                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();

                    return new Image
                    {
                        Source = bitmapImage
                    };
                }
            }
            catch { }
            return null;
        }
    }
}

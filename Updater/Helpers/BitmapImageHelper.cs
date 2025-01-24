using System.Windows.Media.Imaging;
using System.Windows;

namespace Updater.Helpers
{
    public static class BitmapImageHelper
    {
        /// <summary>
        /// Creates a <see cref="BitmapImage"/> from the specified manifest resource.
        /// </summary>
        /// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
        /// <returns></returns>
        public static BitmapImage? FromManifestResource(string name)
        {
            try
            {
                string? value = Application.ResourceAssembly.GetManifestResourceNames()
                    .SingleOrDefault(x => x.EndsWith(name));

                if (string.IsNullOrEmpty(value))
                    return null;

                var bitmapImage = new BitmapImage();
                using (var stream = Application.ResourceAssembly.GetManifestResourceStream(value))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                }

                return bitmapImage;
            }
            catch { }
            return null;
        }
    }
}

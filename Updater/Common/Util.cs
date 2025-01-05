using System.Windows.Media.Imaging;
using System.Windows;
using Updater.Imports;

namespace Updater.Common
{
    public static class Util
    {
        /// <summary>
        /// Loads the specified manifest resource from this assembly and intiailizes a new instance of the 
        /// <see cref="BitmapImage"/> class.
        /// </summary>
        /// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
        /// <returns><see langword="null"/> if an exception is caught. Otherwise, a new <see cref="BitmapImage"/>.</returns>
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
            catch { }
            return null;
        }

        /// <summary>
        /// Retrieves a string from the specified section in an initialization file.
        /// </summary>
        /// <param name="section">The name of the section containing the key name. 
        /// If this parameter is <see langword="null"/>, the function returns all section names in the file.</param>
        /// <param name="key">The name of the key whose associated string is to be retrieved. 
        /// If this parameter is <see langword="null"/>, all key names in the section are returned.</param>
        /// <param name="defaultValue">A default string. 
        /// If the key cannot be found in the initialization file, the function returns the default string. 
        /// If this parameter is <see langword="null"/>, the default is an empty string.</param>
        /// <param name="fileName">The name of the initialization file. 
        /// If this parameter does not contain a full path to the file, the system searches for the file in the Windows directory.</param>
        /// <returns></returns>
        public static string GetPrivateProfileString(string? section, string? key, string? defaultValue, string fileName)
        {
            var buffer = new char[short.MaxValue];
            var length = Kernel32.GetPrivateProfileStringW(section, key, defaultValue, buffer, (uint)buffer.Length, fileName);
            return new string(buffer, 0, (int)length);
        }
    }
}

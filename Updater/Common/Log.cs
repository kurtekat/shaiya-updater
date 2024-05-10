using System.IO;
using System.Windows;

namespace Updater.Common
{
    public class Log
    {
        private readonly string FileName = "Updater.Log.txt";

        public Log()
        {
        }

        public void Write(string contents)
        {
            try
            {
                File.AppendAllText(FileName, $"{DateTime.Now}\n{contents}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

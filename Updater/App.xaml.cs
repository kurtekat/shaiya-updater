using System.IO;
using System.Windows;
using Updater.Resources;

namespace Updater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex? _mutex = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            var fileName = Path.GetFileNameWithoutExtension(Environment.ProcessPath);
            _mutex = new Mutex(true, $"{fileName}_SingleInstanceMutex", out bool createdNew);

            if (createdNew)
            {
                base.OnStartup(e);
            }
            else
            {
                MessageBox.Show(Strings.SingleInstance, fileName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Current.Shutdown(0);
            }
        }
    }
}

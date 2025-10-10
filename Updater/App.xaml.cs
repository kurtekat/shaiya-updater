using System.Reflection;
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
            _mutex = new Mutex(true, $"{ProductName}_SingleInstanceMutex", out bool createdNew);

            if (createdNew)
            {
                base.OnStartup(e);
            }
            else
            {
                MessageBox.Show(Strings.SingleInstance, ProductName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Current.Shutdown(0);
            }
        }

        /// <summary>
        /// Gets the product name associated with this application.
        /// </summary>
        public static string? ProductName => Assembly.GetEntryAssembly()
                    ?.GetCustomAttributes(typeof(AssemblyProductAttribute))
                    ?.OfType<AssemblyProductAttribute>()
                    ?.FirstOrDefault()?.Product;
    }
}

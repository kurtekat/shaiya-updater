using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Updater.Common;
using Updater.Imports;

namespace Updater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly BackgroundWorker _backgroundWorker1 = new();
        private static HttpClient _httpClient = new();
        private static readonly Image _icon3 = new();
        private static readonly Image _image167 = new();
        private static readonly Image _image168 = new();
        private static readonly Image _image169 = new();
        private static readonly Image _image170 = new();
        private static readonly Image _image185 = new();
        private static readonly Image _image187 = new();
        private static readonly Image _image188 = new();

        public MainWindow()
        {
            InitializeComponent();
            _backgroundWorker1.WorkerReportsProgress = true;
            _backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            _backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;

            var handler = new ProgressMessageHandler(new HttpClientHandler());
            handler.HttpReceiveProgress += ProgressMessageHandler_HttpReceiveProgress;
            _httpClient = new HttpClient(handler, true);
        }

        private void ProgressMessageHandler_HttpReceiveProgress(object? sender, HttpProgressEventArgs e)
        {
            if (sender is null)
                return;

            _backgroundWorker1.ReportProgress(e.ProgressPercentage, new ProgressReport(ByProgressBar: 1));
        }

        private void BackgroundWorker1_DoWork(object? sender, DoWorkEventArgs e)
        {
            Program.DoWork(_httpClient, _backgroundWorker1);
        }

        private void BackgroundWorker1_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is null)
                return;

            if (e.UserState is ProgressReport progressReport)
            {
                if (!string.IsNullOrEmpty(progressReport.Message))
                    _textBox1.Text = progressReport.Message;

                if (progressReport.ByProgressBar == 1)
                    _progressBar1.Value = e.ProgressPercentage;       
                else if (progressReport.ByProgressBar == 2)
                    _progressBar2.Value = e.ProgressPercentage;
            }
        }

        private void Window1_Initialized(object sender, EventArgs e)
        {
            var windowName = Application.Current.MainWindow.Title;
            if (User32.FindWindowW(null, windowName) != IntPtr.Zero)
            {
                var message = "Updater already in operation.";
                var caption = Application.ResourceAssembly.GetName().Name;
                MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Application.Current.Shutdown(0);
            }

            if (User32.FindWindowW("GAME", "Shaiya") != IntPtr.Zero)
            {
                var message = "Game still in operation. Please try again after closing the game.";
                var caption = Application.ResourceAssembly.GetName().Name;
                MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Application.Current.Shutdown(0);
            }

            var icon3 = Util.BitmapImageFromManifestResource("Updater.Resources.Icon.Icon3.ico");
            if (icon3.StreamSource is not null)
            {
                _icon3.Width = 48;
                _icon3.Height = 48;
                _icon3.Source = icon3;
            }

            var image167 = Util.BitmapImageFromManifestResource("Updater.Resources.Bitmap.Bitmap167.bmp");
            if (image167.StreamSource is not null)
            {
                _image167.Width = 750;
                _image167.Height = 550;
                _image167.Source = image167;
            }

            var image168 = Util.BitmapImageFromManifestResource("Updater.Resources.Bitmap.Bitmap168.bmp");
            if (image168.StreamSource is not null)
            {
                _image168.Width = 148;
                _image168.Height = 38;
                _image168.Source = image168;
            }

            var image169 = Util.BitmapImageFromManifestResource("Updater.Resources.Bitmap.Bitmap169.bmp");
            if (image169.StreamSource is not null)
            {
                _image169.Width = 148;
                _image169.Height = 38;
                _image169.Source = image169;
            }

            var image170 = Util.BitmapImageFromManifestResource("Updater.Resources.Bitmap.Bitmap170.bmp");
            if (image170.StreamSource is not null)
            {
                _image170.Width = 148;
                _image170.Height = 38;
                _image170.Source = image170;
            }

            var image185 = Util.BitmapImageFromManifestResource("Updater.Resources.Bitmap.Bitmap185.bmp");
            if (image185.StreamSource is not null)
            {
                _image185.Width = 26;
                _image185.Height = 24;
                _image185.Source = image185;
            }

            var image187 = Util.BitmapImageFromManifestResource("Updater.Resources.Bitmap.Bitmap187.bmp");
            if (image187.StreamSource is not null)
            {
                _image187.Width = 26;
                _image187.Height = 24;
                _image187.Source = image187;
            }

            var image188 = Util.BitmapImageFromManifestResource("Updater.Resources.Bitmap.Bitmap188.bmp");
            if (image188.StreamSource is not null)
            {
                _image188.Width = 26;
                _image188.Height = 24;
                _image188.Source = image188;
            }
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            _window1.Background = new ImageBrush(_image167.Source);
            _window1.Icon = _icon3.Source;
            _button1.Content = _image185;
            _button2.Content = _image168;
            _webBrowser1.Navigate(Constants.WebBrowserSource);
            _backgroundWorker1.RunWorkerAsync();
        }

        private void Window1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            _button1.Content = _image188;

            if (_backgroundWorker1.IsBusy)
                return;

            Application.Current.Shutdown(0);
        }

        private void Button1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _button1.Content = _image187;
        }

        private void Button1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _button1.Content = _image185;
        }

        private void Button1_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _button1.Content = _image187;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            _button2.Content = _image170;

            if (_backgroundWorker1.IsBusy)
                return;

            try
            {
                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "game.exe");
                Process.Start(fileName, "start game");
                Kernel32.TerminateProcess(Kernel32.GetCurrentProcess(), 0);
            }
            catch (Exception ex)
            {
                var caption = Application.ResourceAssembly.GetName().Name;
                MessageBox.Show(ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(ex.HResult);
            }
        }

        private void Button2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _button2.Content = _image169;
        }

        private void Button2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _button2.Content = _image168;
        }

        private void Button2_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _button2.Content = _image169;
        }
    }
}

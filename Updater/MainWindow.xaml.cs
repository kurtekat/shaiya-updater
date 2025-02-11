using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Updater.Common;
using Updater.Core;
using Updater.Helpers;
using Updater.Resources;

namespace Updater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly BackgroundWorker _backgroundWorker1 = new();
        private static HttpClient _httpClient = new();
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

            _button1.AddHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(Button1_Click), true);
            _button2.AddHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(Button2_Click), true);
        }

        private void ProgressMessageHandler_HttpReceiveProgress(object? sender, HttpProgressEventArgs e)
        {
            if (sender == null)
                return;

            _backgroundWorker1.ReportProgress(e.ProgressPercentage, new ProgressReport(1));
        }

        private void BackgroundWorker1_DoWork(object? sender, DoWorkEventArgs e)
        {
            Program.DoWork(_httpClient, _backgroundWorker1);
        }

        private void BackgroundWorker1_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            if (e.UserState == null)
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
            if (DllImport.FindWindowW(null, Application.Current.MainWindow.Title) != IntPtr.Zero)
            {
                var caption = Application.ResourceAssembly.GetName().Name;
                MessageBox.Show(Strings.Message1, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Application.Current.Shutdown(0);
            }

            if (DllImport.FindWindowW("GAME", "Shaiya") != IntPtr.Zero)
            {
                var caption = Application.ResourceAssembly.GetName().Name;
                MessageBox.Show(Strings.Message2, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Application.Current.Shutdown(0);
            }

            var image167 = BitmapImageHelper.FromManifestResource("Bitmap167.bmp");
            if (image167 != null)
            {
                _image167.Width = image167.PixelWidth;
                _image167.Height = image167.PixelHeight;
                _image167.Source = image167;
            }

            var image168 = BitmapImageHelper.FromManifestResource("Bitmap168.bmp");
            if (image168 != null)
            {
                _image168.Width = image168.PixelWidth;
                _image168.Height = image168.PixelHeight;
                _image168.Source = image168;
            }

            var image169 = BitmapImageHelper.FromManifestResource("Bitmap169.bmp");
            if (image169 != null)
            {
                _image169.Width = image169.PixelWidth;
                _image169.Height = image169.PixelHeight;
                _image169.Source = image169;
            }

            var image170 = BitmapImageHelper.FromManifestResource("Bitmap170.bmp");
            if (image170 != null)
            {
                _image170.Width = image170.PixelWidth;
                _image170.Height = image170.PixelHeight;
                _image170.Source = image170;
            }

            var image185 = BitmapImageHelper.FromManifestResource("Bitmap185.bmp");
            if (image185 != null)
            {
                _image185.Width = image185.PixelWidth;
                _image185.Height = image185.PixelHeight;
                _image185.Source = image185;
            }

            var image187 = BitmapImageHelper.FromManifestResource("Bitmap187.bmp");
            if (image187 != null)
            {
                _image187.Width = image187.PixelWidth;
                _image187.Height = image187.PixelHeight;
                _image187.Source = image187;
            }

            var image188 = BitmapImageHelper.FromManifestResource("Bitmap188.bmp");
            if (image188 != null)
            {
                _image188.Width = image188.PixelWidth;
                _image188.Height = image188.PixelHeight;
                _image188.Source = image188;
            }
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            _window1.Background = new ImageBrush(_image167.Source);
            _button1.Content = _image185;
            _button2.Content = _image168;
            _webBrowser1.Navigate(Constants.WebBrowserSource);
            _backgroundWorker1.RunWorkerAsync();
        }

        private void Window1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (_backgroundWorker1.IsBusy)
                return;

            Application.Current.Shutdown(0);
        }

        private void Button1_MouseEnter(object sender, MouseEventArgs e)
        {
            _button1.Content = _image187;
        }

        private void Button1_MouseLeave(object sender, MouseEventArgs e)
        {
            _button1.Content = _image185;
        }

        private void Button1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _button1.Content = _image188;
        }

        private void Button1_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _button1.Content = _image187;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (_backgroundWorker1.IsBusy)
                return;

            try
            {
                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "game.exe");
                Process.Start(fileName, "start game");
                _ = DllImport.TerminateProcess(DllImport.GetCurrentProcess(), 0);
            }
            catch (Exception ex)
            {
                var caption = Application.ResourceAssembly.GetName().Name;
                MessageBox.Show(ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(ex.HResult);
            }
        }

        private void Button2_MouseEnter(object sender, MouseEventArgs e)
        {
            _button2.Content = _image169;
        }

        private void Button2_MouseLeave(object sender, MouseEventArgs e)
        {
            _button2.Content = _image168;
        }

        private void Button2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _button2.Content = _image170;
        }

        private void Button2_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _button2.Content = _image169;
        }
    }
}

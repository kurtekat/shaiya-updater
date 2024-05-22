using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Windows;
using Updater.Common;
using Updater.Core;

namespace Updater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly BackgroundWorker _backgroundWorker1 = new();
        private static HttpClient _httpClient = new();

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

            if (sender is HttpRequestMessage request && request.RequestUri is not null)
            {
                if (request.RequestUri.AbsolutePath.EndsWith(ServerConfiguration.FileName))
                    return;
            }

            _backgroundWorker1.ReportProgress(e.ProgressPercentage, new ProgressReport("", 1));
        }

        private void BackgroundWorker1_DoWork(object? sender, DoWorkEventArgs e)
        {
            Program.DoWork(_httpClient, _backgroundWorker1);

            if (_backgroundWorker1.CancellationPending)
                e.Cancel = true;
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
            if (Util.WindowExists(null, "Shaiya Updater"))
            {
                MessageBox.Show(Constants.Message1, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(0);
            }

            if (Util.WindowExists("GAME", "Shaiya"))
            {
                MessageBox.Show(Constants.Message2, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(0);
            }
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            _webBrowser1.Source = new Uri(Constants.WebBrowserSource);
            _backgroundWorker1.RunWorkerAsync();
        }

        private void Window1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (_backgroundWorker1.IsBusy)
                return;

            Application.Current.Shutdown(0);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (_backgroundWorker1.IsBusy)
                return;

            try
            {
                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "game.exe");
                Process.Start(fileName, "start game");
                Application.Current.Shutdown(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}

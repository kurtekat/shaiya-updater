using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Windows;
using Updater.Core;

namespace Updater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static HttpClient HttpClient { get; private set; } = new();
        internal static BackgroundWorker BackgroundWorker { get; private set; } = new();

        public App()
        {
            var handler = new ProgressMessageHandler(new HttpClientHandler());
            handler.HttpReceiveProgress += ProgressMessageHandler_HttpReceiveProgress;
            HttpClient = new HttpClient(handler, true);

            BackgroundWorker.WorkerReportsProgress = true;
            BackgroundWorker.DoWork += BackgroundWorker_DoWork;
            BackgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            BackgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        private void ProgressMessageHandler_HttpReceiveProgress(object? sender, HttpProgressEventArgs e)
        {
            if (sender == null)
                return;

            BackgroundWorker.ReportProgress(e.ProgressPercentage, ProgressBarNumber.One);
        }

        private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            Program.DoWork();
        }

        private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is string text)
            {
                var window = MainWindow as MainWindow;
                if (window == null)
                    return;

                window.TextBox1.Text = text;
            }

            if (e.UserState is ProgressBarNumber number)
            {
                var window = MainWindow as MainWindow;
                if (window == null)
                    return;

                switch (number)
                {
                    case ProgressBarNumber.One:
                        window.ProgressBar1.Value = e.ProgressPercentage;
                        break;
                    case ProgressBarNumber.Two:
                        window.ProgressBar2.Value = e.ProgressPercentage;
                        break;
                    default:
                        break;
                }
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                var window = MainWindow as MainWindow;
                if (window == null)
                    return;

                window.TextBox1.Text = "Error";
                Program.Log(e.Error.ToString());
            }
        }
    }
}

using Parsec.Shaiya.Data;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Windows;
using Updater.Configuration;
using Updater.Core;
using Updater.Core.Extensions;
using Updater.Internal;
using Updater.Resources;

namespace Updater
{
    public static class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            var guid = new Guid("b380d033-1607-4d2f-bc1e-f92c0bef4a01");
            var name = $"Global\\{{{guid}}}";

            using (var mutex = new Mutex(false, name))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show(
                        Strings.MessageBox_SingleInstance,
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    return 0;
                }

                if (User32.FindWindowW("GAME", "Shaiya") != nint.Zero)
                {
                    MessageBox.Show(
                        Strings.MessageBox_ClientInstance,
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    return 0;
                }

                var app = new App();
                app.InitializeComponent();
                return app.Run();
            }
        }

        public static void Log(string contents)
        {
            File.WriteAllText("Updater.Log.txt", contents);
        }

        public static void DoWork()
        {
            var url = $"{Web.BaseUrl}/shaiya/UpdateVersion.ini";
            var rawData = App.HttpClient.DownloadString(url);

            var serverSettings = new Ini();
            serverSettings.Parse(rawData);

            int updaterVersion = serverSettings.GetValueOrDefault("Version:UpdaterVersion", 0);
            if (updaterVersion > Constants.UpdaterVersion)
            {
                App.BackgroundWorker.ReportProgress(0, Strings.UserState_DownloadingUpdater);

                var newUpdater = new NewUpdater();
                App.HttpClient.DownloadFile(newUpdater.Url, newUpdater.FileName);

                UpdaterPatcher();
                return;
            }

            int serverVersion = serverSettings.GetValueOrDefault("Version:PatchFileVersion", 2);
            int clientVersion = 1;
            var clientSettings = new Ini();

            var ini = new FileInfo("Version.ini");
            if (ini.Exists)
            {
                clientSettings.Parse(ini);
                clientVersion = clientSettings.GetValueOrDefault("Version:CurrentVersion", 1);
            }

            if (serverVersion > clientVersion)
            {
                App.BackgroundWorker.ReportProgress(0, ProgressBarNumber.One);
                App.BackgroundWorker.ReportProgress(0, ProgressBarNumber.Two);

                int progressMax = serverVersion - clientVersion;
                int progressValue = 1;

                while (clientVersion < serverVersion)
                {
                    var progressText = string.Format(Strings.UserState_Downloading, progressValue, progressMax);
                    App.BackgroundWorker.ReportProgress(0, progressText);

                    var patch = new Patch(clientVersion + 1);
                    App.HttpClient.DownloadFile(patch.Url, patch.FileName);

                    App.BackgroundWorker.ReportProgress(0, Strings.UserState_Extracting);

                    using (var archive = ZipFile.OpenRead(patch.FileName))
                        archive.ExtractToDirectory(Environment.CurrentDirectory, true);
                        
                    File.Delete(patch.FileName);

                    App.BackgroundWorker.ReportProgress(0, Strings.UserState_Updating);
                    DataRemover();
                    DataPatcher();

                    progressValue++;
                    clientVersion++;

                    clientSettings.SetOrAddValue("Version:CurrentVersion", clientVersion);
                    clientSettings.Write(ini.FullName);

                    int progressPercentage = (int)((double)clientVersion / serverVersion * 100);
                    App.BackgroundWorker.ReportProgress(progressPercentage, ProgressBarNumber.Two);
                }

                App.BackgroundWorker.ReportProgress(0, Strings.UserState_UpdateCompleted);
            }
        }

        private static void DataRemover()
        {
            var lst = new FileInfo("delete.lst");
            if (!lst.Exists)
                return;

            var fileCount = File.ReadAllLines(lst.FullName).Length;
            if (fileCount != 0)
            {
                var progress = new BackgroundWorkerProgress(App.BackgroundWorker, ProgressBarNumber.One, fileCount, 1);
                var data = new Data("data.sah", "data.saf");

                data.RemoveFilesFromLst(lst.FullName, progress.PerformStep);
                data.Sah.Write(data.Sah.Path);
            }

            File.Delete(lst.FullName);
        }

        private static void DataPatcher()
        {
            var sah = new FileInfo("update.sah");
            var saf = new FileInfo("update.saf");

            if (!sah.Exists || !saf.Exists)
                return;

            var patchData = new Data(sah.FullName, saf.FullName);
            var fileCount = patchData.FileIndex.Count;

            if (fileCount != 0)
            {
                var progress = new BackgroundWorkerProgress(App.BackgroundWorker, ProgressBarNumber.One, fileCount, 1);
                var data = new Data("data.sah", "data.saf");

                using (var dataPatcher = new DataPatcher())
                    dataPatcher.Patch(data, patchData, progress.PerformStep);

                data.Sah.Write(data.Sah.Path);
            }

            File.Delete(sah.FullName);
            File.Delete(saf.FullName);
        }

        private static void UpdaterPatcher()
        {
            // The game client should delete the old updater, rename the new updater, 
            // and then start the new updater.
            Process.Start("game.exe", "new updater");
            Environment.Exit(0);
        }
    }
}

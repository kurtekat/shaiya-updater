using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Windows;
using Parsec.Shaiya.Data;
using Updater.Common;
using Updater.Configuration;
using Updater.Core;
using Updater.Data;
using Updater.Extensions;
using Updater.Helpers;
using Updater.Resources;

namespace Updater
{
    public static class Program
    {
        public static void DoWork(HttpClient httpClient, BackgroundWorker backgroundWorker)
        {
            ClientConfiguration? clientConfiguration = null;
            ServerConfiguration? serverConfiguration = null;

            try
            {
                serverConfiguration = new ServerConfiguration(httpClient);
                clientConfiguration = new ClientConfiguration();

                if (serverConfiguration.UpdaterVersion > Constants.UpdaterVersion)
                {
                    backgroundWorker.ReportProgress(0, Strings.UserState1);
                    UpdaterPatcher(httpClient);
                    return;
                }

                if (serverConfiguration.PatchFileVersion > clientConfiguration.CurrentVersion)
                {
                    backgroundWorker.ReportProgress(0, new ProgressReport("ProgressBar1"));
                    backgroundWorker.ReportProgress(0, new ProgressReport("ProgressBar2"));

                    int progressMax = serverConfiguration.PatchFileVersion - clientConfiguration.CurrentVersion;
                    int progressValue = 1;

                    while (clientConfiguration.CurrentVersion < serverConfiguration.PatchFileVersion)
                    {
                        var progressText = string.Format(Strings.UserState2, progressValue, progressMax);
                        backgroundWorker.ReportProgress(0, progressText);

                        var patch = new Patch(clientConfiguration.CurrentVersion + 1);
                        httpClient.DownloadFile(patch.Url, patch.Path);

                        if (!File.Exists(patch.Path))
                        {
                            backgroundWorker.ReportProgress(0, Strings.UserState3);
                            return;
                        }

                        backgroundWorker.ReportProgress(0, Strings.UserState4);

                        if (!patch.ExtractToCurrentDirectory())
                        {
                            backgroundWorker.ReportProgress(0, Strings.UserState5);
                            return;
                        }

                        File.Delete(patch.Path);
                        backgroundWorker.ReportProgress(0, Strings.UserState6);
                        DataPatcher(backgroundWorker);

                        clientConfiguration.CurrentVersion++;
                        progressValue++;

                        var progressPercentage = MathHelper.Percentage(clientConfiguration.CurrentVersion, serverConfiguration.PatchFileVersion);
                        if (progressPercentage > 0)
                        {
                            backgroundWorker.ReportProgress(progressPercentage, new ProgressReport("ProgressBar2"));
                        }
                    }

                    backgroundWorker.ReportProgress(0, Strings.UserState7);
                    DataBuilder(backgroundWorker);
                    backgroundWorker.ReportProgress(0, Strings.UserState8);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                clientConfiguration?.Save();
                clientConfiguration?.Dispose();
                clientConfiguration = null;

                serverConfiguration?.Dispose();
                serverConfiguration = null;
            }
        }

        private static void DataBuilder(BackgroundWorker backgroundWorker)
        {
            if (!File.Exists("data.sah") || !File.Exists("data.saf"))
                return;

            var data = new Parsec.Shaiya.Data.Data("data.sah", "data.saf");
            File.Move("data.sah", "data.sah.bak", true);
            File.Move("data.saf", "data.saf.bak", true);
            data.Sah.Path += ".bak";
            data.Saf.Path += ".bak";

            var progressReport = new ProgressReport("ProgressBar1");
            var progress = new Progress(backgroundWorker, progressReport, data.FileIndex.Count, 1);
            Updater.Data.DataBuilder.Build(data, Directory.GetCurrentDirectory(), progress.PerformStep);

            File.Delete(data.Sah.Path);
            File.Delete(data.Saf.Path);
        }

        private static void DataPatcher(BackgroundWorker backgroundWorker)
        {
            if (File.Exists("delete.lst"))
            {
                var data = new Parsec.Shaiya.Data.Data("data.sah", "data.saf");
                var paths = File.ReadAllLines("delete.lst");

                var progressReport = new ProgressReport("ProgressBar1");
                var progress = new Progress(backgroundWorker, progressReport, paths.Length, 1);

                data.RemoveFilesFromLst("delete.lst", progress.PerformStep);
                data.Sah.Write(data.Sah.Path);
                File.Delete("delete.lst");
            }

            if (File.Exists("update.sah") && File.Exists("update.saf"))
            {
                var data = new Parsec.Shaiya.Data.Data("data.sah", "data.saf");
                var update = new Parsec.Shaiya.Data.Data("update.sah", "update.saf");

                var progressReport = new ProgressReport("ProgressBar1");
                var progress = new Progress(backgroundWorker, progressReport, update.FileIndex.Count, 1);

                using (var dataPatcher = new DataPatcher())
                    dataPatcher.Patch(data, update, progress.PerformStep);

                data.Sah.Write(data.Sah.Path);
                File.Delete("update.sah");
                File.Delete("update.saf");
            }
        }

        /// <summary>
        /// Downloads a new updater, starts a client process, passing "new updater" as the 
        /// command-line argument, and terminates the current process.
        /// 
        /// Expect the client to delete the old updater, rename the new updater and create 
        /// an updater process.
        /// </summary>
        private static void UpdaterPatcher(HttpClient httpClient)
        {
            var newUpdater = new NewUpdater();
            httpClient.DownloadFile(newUpdater.Url, newUpdater.Path);

            if (!File.Exists(newUpdater.Path))
                return;

            var fileName = Path.Combine(Directory.GetCurrentDirectory(), "game.exe");
            Process.Start(fileName, "new updater");

            var currentProcess = Process.GetCurrentProcess();
            currentProcess.Kill();
        }
    }
}

using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Windows;
using Parsec.Shaiya.Data;
using Updater.Common;
using Updater.Core;
using Updater.Imports;
using Updater.Resources;

namespace Updater
{
    public static class Program
    {
        public static void DoWork(HttpClient httpClient, BackgroundWorker worker)
        {
            try
            {
                var serverConfiguration = new ServerConfiguration(httpClient);
                var clientConfiguration = new ClientConfiguration();

                if (serverConfiguration.UpdaterVersion > Constants.UpdaterVersion)
                {
                    worker.ReportProgress(0, new ProgressReport(Strings.ProgressMessage1));
                    UpdaterPatcher(httpClient);
                    return;
                }

                if (serverConfiguration.PatchFileVersion > clientConfiguration.CurrentVersion)
                {
                    worker.ReportProgress(0, new ProgressReport(ByProgressBar: 1));
                    worker.ReportProgress(0, new ProgressReport(ByProgressBar: 2));

                    uint progressMax = serverConfiguration.PatchFileVersion - clientConfiguration.CurrentVersion;
                    uint progressValue = 1;

                    while (clientConfiguration.CurrentVersion < serverConfiguration.PatchFileVersion)
                    {
                        var progressMessage = string.Format(Strings.ProgressMessage2, progressValue, progressMax);
                        worker.ReportProgress(0, new ProgressReport(progressMessage));

                        var patch = new Patch(clientConfiguration.CurrentVersion + 1);
                        Util.DownloadToFile(httpClient, patch.Url, patch.Path);

                        if (!File.Exists(patch.Path))
                        {
                            worker.ReportProgress(0, new ProgressReport(Strings.ProgressMessage3));
                            break;
                        }

                        worker.ReportProgress(0, new ProgressReport(Strings.ProgressMessage4));
                        Kernel32.WritePrivateProfileStringW("Version", "StartUpdate", "EXTRACT_START", clientConfiguration.Path);

                        if (Util.ExtractZipFile(patch.Path, Directory.GetCurrentDirectory()) != 0)
                        {
                            worker.ReportProgress(0, new ProgressReport(Strings.ProgressMessage5));
                            break;
                        }

                        Kernel32.WritePrivateProfileStringW("Version", "StartUpdate", "EXTRACT_END", clientConfiguration.Path);
                        File.Delete(patch.Path);

                        worker.ReportProgress(0, new ProgressReport(Strings.ProgressMessage6));
                        Kernel32.WritePrivateProfileStringW("Version", "StartUpdate", "UPDATE_START", clientConfiguration.Path);

                        if (DataPatcher(worker) != 0)
                        {
                            worker.ReportProgress(0, new ProgressReport(Strings.ProgressMessage7));
                            break;
                        }

                        Kernel32.WritePrivateProfileStringW("Version", "StartUpdate", "UPDATE_END", clientConfiguration.Path);

                        ++clientConfiguration.CurrentVersion;
                        ++progressValue;

                        var percentProgress = ((double)clientConfiguration.CurrentVersion / serverConfiguration.PatchFileVersion) * 100;
                        if (percentProgress != 0)
                            worker.ReportProgress((int)percentProgress, new ProgressReport(Strings.ProgressMessage8, 2));

                        var currentVersion = clientConfiguration.CurrentVersion.ToString();
                        Kernel32.WritePrivateProfileStringW("Version", "CurrentVersion", currentVersion, clientConfiguration.Path);
                    }
                }
            }
            catch (Exception ex)
            {
                var caption = Application.ResourceAssembly.GetName().Name;
                MessageBox.Show(ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static int DataPatcher(BackgroundWorker worker)
        {
            try
            {
                var data = new Data("data.sah", "data.saf");
                var update = new Data("update.sah", "update.saf");

                if (File.Exists("delete.lst"))
                {
                    data.RemoveFilesFromLst("delete.lst");
                    data.Sah.Write(data.Sah.Path);
                    File.Delete("delete.lst");
                }

                var progressReport = new ProgressReport(ByProgressBar: 1);
                var progress = new PatchProgress(update.FileCount, worker, progressReport);
                using (var dataPatcher = new DataPatcher())
                    dataPatcher.Patch(data, update, progress.PerformStep);

                data.Sah.Write(data.Sah.Path);

                File.Delete("update.sah");
                File.Delete("update.saf");
                return 0;
            }
            catch (Exception ex)
            {
                var caption = Application.ResourceAssembly.GetName().Name;
                MessageBox.Show(ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

        /// <summary>
        /// Downloads a new updater, starts a client process, passing "new updater" as the 
        /// command-line argument, and terminates the current process.
        /// 
        /// Expect the client to rename the new updater, delete the old updater and create 
        /// an updater process.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <returns>Zero on failure. Otherwise, nonzero.</returns>
        private static void UpdaterPatcher(HttpClient httpClient)
        {
            try
            {
                var newUpdater = new NewUpdater();
                Util.DownloadToFile(httpClient, newUpdater.Url, newUpdater.Path);

                if (!File.Exists(newUpdater.Path))
                    return;

                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "game.exe");
                Process.Start(fileName, "new updater");
                Kernel32.TerminateProcess(Kernel32.GetCurrentProcess(), 0);
            }
            catch (Exception ex)
            {
                var caption = Application.ResourceAssembly.GetName().Name;
                MessageBox.Show(ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

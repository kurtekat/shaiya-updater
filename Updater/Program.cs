﻿using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using Parsec.Shaiya.Data;
using Updater.Common;
using Updater.Core;

namespace Updater
{
    public static class Program
    {
        public static void DoWork(HttpClient httpClient, BackgroundWorker worker)
        {
            try
            {
                var serverCfg = new ServerConfiguration(httpClient);
                var clientCfg = new ClientConfiguration();

                if (serverCfg.UpdaterVersion > Constants.UpdaterVersion)
                {
                    UpdaterPatcher(httpClient, worker);
                    return;
                }

                if (serverCfg.PatchFileVersion > clientCfg.CurrentVersion)
                {
                    worker.ReportProgress(0, new ProgressReport(ByProgressBar: 1));
                    worker.ReportProgress(0, new ProgressReport(ByProgressBar: 2));

                    uint progressMax = serverCfg.PatchFileVersion - clientCfg.CurrentVersion;
                    uint progressValue = 1;

                    while (clientCfg.CurrentVersion < serverCfg.PatchFileVersion)
                    {
                        var progressMessage = string.Format(Constants.ProgressMessage1, progressValue, progressMax);
                        worker.ReportProgress(0, new ProgressReport(progressMessage));

                        var patch = new Patch(clientCfg.CurrentVersion + 1);
                        Util.DownloadToFile(httpClient, patch.Url, patch.Path);

                        if (!File.Exists(patch.Path))
                        {
                            worker.ReportProgress(0, new ProgressReport(Constants.ProgressMessage2));
                            break;
                        }

                        worker.ReportProgress(0, new ProgressReport(Constants.ProgressMessage3));
                        Win32.WritePrivateProfileStringW("Version", "StartUpdate", "EXTRACT_START", clientCfg.Path);

                        if (Util.ExtractZipFile(patch.Path, Directory.GetCurrentDirectory()) != 0)
                        {
                            worker.ReportProgress(0, new ProgressReport(Constants.ProgressMessage4));
                            break;
                        }

                        Win32.WritePrivateProfileStringW("Version", "StartUpdate", "EXTRACT_END", clientCfg.Path);
                        File.Delete(patch.Path);

                        worker.ReportProgress(0, new ProgressReport(Constants.ProgressMessage5));
                        Win32.WritePrivateProfileStringW("Version", "StartUpdate", "UPDATE_START", clientCfg.Path);

                        if (DataPatcher(worker) != 0)
                        {
                            worker.ReportProgress(0, new ProgressReport(Constants.ProgressMessage6));
                            break;
                        }

                        Win32.WritePrivateProfileStringW("Version", "StartUpdate", "UPDATE_END", clientCfg.Path);

                        ++clientCfg.CurrentVersion;
                        ++progressValue;

                        var percentProgress = ((double)clientCfg.CurrentVersion / serverCfg.PatchFileVersion) * 100;
                        if (percentProgress != 0)
                            worker.ReportProgress((int)percentProgress, new ProgressReport(Constants.ProgressMessage7, 2));

                        var currentVersion = clientCfg.CurrentVersion.ToString();
                        Win32.WritePrivateProfileStringW("Version", "CurrentVersion", currentVersion, clientCfg.Path);
                    }
                }
            }
            catch (Exception ex)
            {
                var log = new Log();
                log.Write(ex.ToString());
            }
        }

        public static int DataPatcher(BackgroundWorker worker)
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

                var progress = new DataPatcherProgress(update.FileCount, worker);
                using (var dataPatcher = new DataPatcher())
                    dataPatcher.Patch(data, update, progress.FilePatchedCallback);

                data.Sah.Write(data.Sah.Path);

                File.Delete("update.sah");
                File.Delete("update.saf");
                return 0;
            }
            catch (Exception ex)
            {
                var log = new Log();
                log.Write(ex.ToString());
                return -1;
            }
        }

        public static void UpdaterPatcher(HttpClient httpClient, BackgroundWorker worker)
        {
            try
            {
                worker.ReportProgress(0, new ProgressReport(Constants.ProgressMessage0));

                var newUpdater = new NewUpdater();
                Util.DownloadToFile(httpClient, newUpdater.Url, newUpdater.Path);

                if (!File.Exists(newUpdater.Path))
                {
                    worker.ReportProgress(0, new ProgressReport(Constants.ProgressMessage2));
                    return;
                }

                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "game.exe");
                Process.Start(fileName, "new updater");
                Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {
                var log = new Log();
                log.Write(ex.ToString());
            }
        }
    }
}

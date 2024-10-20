using System.ComponentModel;
using Updater.Common;

namespace Updater.Core
{
    public class DataPatcherProgress
    {
        private readonly BackgroundWorker _backgroundWorker = new();
        public int Maximum { get; } = 0;
        public int Value { get; set; } = 0;

        public DataPatcherProgress(int fileCount, BackgroundWorker worker)
        {
            if (fileCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(fileCount));

            Maximum = fileCount;
            _backgroundWorker = worker;
            _backgroundWorker.WorkerReportsProgress = true;
        }

        public void FilePatchedCallback()
        {
            Value++;
            var percentProgress = (double)(Value / Maximum) * 100;
            _backgroundWorker.ReportProgress((int)percentProgress, new ProgressReport(ByProgressBar: 1));
        }
    }
}

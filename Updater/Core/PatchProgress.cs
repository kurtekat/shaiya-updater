using System.ComponentModel;

namespace Updater.Core
{
    public class PatchProgress
    {
        private readonly BackgroundWorker _backgroundWorker;
        private readonly ProgressReport _progressReport;
        public int Maximum { get; }
        public int Value { get; set; }

        public PatchProgress(int fileCount, BackgroundWorker worker, ProgressReport progressReport)
        {
            if (fileCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(fileCount));

            Maximum = fileCount;
            _backgroundWorker = worker;
            _backgroundWorker.WorkerReportsProgress = true;
            _progressReport = progressReport;
        }

        public void PerformStep()
        {
            Value++;
            var percentProgress = (double)(Value / Maximum) * 100;
            _backgroundWorker.ReportProgress((int)percentProgress, _progressReport);
        }
    }
}

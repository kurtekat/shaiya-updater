using System.ComponentModel;

namespace Updater.Core
{
    public class PatchProgress
    {
        private readonly BackgroundWorker _backgroundWorker = new();
        private readonly object? _userState = null;
        public int Maximum { get; } = 0;
        public int Value { get; set; } = 0;

        public PatchProgress(int fileCount, BackgroundWorker worker, object? userState = null)
        {
            if (fileCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(fileCount));

            Maximum = fileCount;
            _backgroundWorker = worker;
            _backgroundWorker.WorkerReportsProgress = true;
            _userState = userState;
        }

        public void PerformStep()
        {
            Value++;
            var percentProgress = (double)(Value / Maximum) * 100;
            _backgroundWorker.ReportProgress((int)percentProgress, _userState);
        }
    }
}

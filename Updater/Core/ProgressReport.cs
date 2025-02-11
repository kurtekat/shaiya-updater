namespace Updater.Core
{
    public sealed class ProgressReport
    {
        public string Message { get; set; } = string.Empty;
        public int ByProgressBar { get; set; } = 0;

        public ProgressReport()
        {
        }

        public ProgressReport(string message)
        {
            Message = message;
        }

        public ProgressReport(int byProgressBar)
        {
            ByProgressBar = byProgressBar;
        }

        public ProgressReport(string message, int byProgressBar)
        {
            Message = message;
            ByProgressBar = byProgressBar;
        }
    }
}

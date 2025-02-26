namespace Updater.Core
{
    /// <summary>
    /// Represents a user state object.
    /// </summary>
    public sealed class ProgressReport
    {
        public string Message { get; set; } = string.Empty;
        public int ByProgressBar { get; set; } = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressReport"/> class.
        /// </summary>
        public ProgressReport()
        {
        }

        /// <summary>
        /// <inheritdoc cref="ProgressReport.ProgressReport"/>
        /// </summary>
        /// <param name="message"></param>
        public ProgressReport(string message)
        {
            Message = message;
        }

        /// <summary>
        /// <inheritdoc cref="ProgressReport.ProgressReport"/>
        /// </summary>
        /// <param name="byProgressBar"></param>
        public ProgressReport(int byProgressBar)
        {
            ByProgressBar = byProgressBar;
        }

        /// <summary>
        /// <inheritdoc cref="ProgressReport.ProgressReport"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="byProgressBar"></param>
        public ProgressReport(string message, int byProgressBar)
        {
            Message = message;
            ByProgressBar = byProgressBar;
        }
    }
}

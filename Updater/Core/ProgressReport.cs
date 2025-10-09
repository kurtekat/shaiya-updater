namespace Updater.Core
{
    /// <summary>
    /// Represents a user state object.
    /// </summary>
    public sealed class ProgressReport
    {
        public object? Value { get; set; } = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressReport"/> class.
        /// </summary>
        public ProgressReport()
        {
        }

        /// <summary>
        /// <inheritdoc cref="ProgressReport.ProgressReport"/>
        /// </summary>
        public ProgressReport(object? value)
        {
            Value = value;
        }
    }
}

using System.ComponentModel;
using Updater.Helpers;

namespace Updater.Core
{
    /// <summary>
    /// Represents the progress of a <see cref="BackgroundWorker"/> operation.
    /// </summary>
    public sealed class Progress
    {
        private readonly BackgroundWorker _backgroundWorker;
        private readonly object? _userState = null;

        /// <summary>
        /// The maximum value of the range. The default is 100.
        /// </summary>
        public int Maximum { get; set; } = 100;

        /// <summary>
        /// The amount by which to increment the <see cref="Value"/> with each call to the <see cref="PerformStep"/> method. The default is 10.
        /// </summary>
        public int Step { get; set; } = 10;

        /// <summary>
        /// The position within the range. The default is 0.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Progress"/> class with the <paramref name="backgroundWorker"/> 
        /// and <paramref name="userState"/> parameters.
        /// </summary>
        /// <param name="backgroundWorker"></param>
        /// <param name="userState">A unique <see cref="object"/> indicating the user state.</param>
        public Progress(BackgroundWorker backgroundWorker, object? userState)
        {
            _backgroundWorker = backgroundWorker;
            _backgroundWorker.WorkerReportsProgress = true;
            _userState = userState;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Progress"/> class with the <paramref name="backgroundWorker"/>, 
        /// <paramref name="userState"/>, and <paramref name="maximum"/> parameters.
        /// </summary>
        /// <param name="backgroundWorker"></param>
        /// <param name="userState">A unique <see cref="object"/> indicating the user state.</param>
        /// <param name="maximum">The maximum value of the range.</param>
        /// <exception cref="ArgumentException">The specified maximum is less than 0.</exception>
        public Progress(BackgroundWorker backgroundWorker, object? userState, int maximum)
        {
            if (maximum < 0)
                throw new ArgumentException(null, nameof(maximum));

            Maximum = maximum;

            _backgroundWorker = backgroundWorker;
            _backgroundWorker.WorkerReportsProgress = true;
            _userState = userState;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Progress"/> class with the <paramref name="backgroundWorker"/>, 
        /// <paramref name="userState"/>, <paramref name="maximum"/>, and <paramref name="step"/> parameters.
        /// </summary>
        /// <param name="backgroundWorker"></param>
        /// <param name="userState">A unique <see cref="object"/> indicating the user state.</param>
        /// <param name="maximum">The maximum value of the range.</param>
        /// <param name="step">The amount by which to increment the <see cref="Value"/> with each call to the <see cref="PerformStep"/> method.</param>
        /// <exception cref="ArgumentException">The specified maximum is less than 0.</exception>
        public Progress(BackgroundWorker backgroundWorker, object? userState, int maximum, int step)
        {
            if (maximum < 0)
                throw new ArgumentException(null, nameof(maximum));

            Maximum = maximum;
            Step = step;

            _backgroundWorker = backgroundWorker;
            _backgroundWorker.WorkerReportsProgress = true;
            _userState = userState;
        }

        /// <summary>
        /// Advances the current <see cref="Value"/> by the specified amount and raises the <see cref="BackgroundWorker.ProgressChanged"/> event.
        /// </summary>
        /// <param name="value">The amount by which to increment the current <see cref="Value"/>.</param>
        public void Increment(int value)
        {
            Value += value;
            Value = Value > Maximum ? Maximum : Value;

            var percentProgress = MathHelper.Percentage(Value, Maximum);
            _backgroundWorker.ReportProgress(percentProgress, _userState);
        }

        /// <summary>
        /// Advances the current <see cref="Value"/> by the amount of the <see cref="Step"/> property and 
        /// raises the <see cref="BackgroundWorker.ProgressChanged"/> event.
        /// </summary>
        public void PerformStep()
        {
            Value += Step;
            Value = Value > Maximum ? Maximum : Value;

            var percentProgress = MathHelper.Percentage(Value, Maximum);
            _backgroundWorker.ReportProgress(percentProgress, _userState);
        }
    }
}

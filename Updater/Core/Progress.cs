using System.ComponentModel;
using Updater.Helpers;

namespace Updater.Core
{
    /// <summary>
    /// Represents the progress of a <see cref="BackgroundWorker"/> operation.
    /// </summary>
    public sealed class Progress : IProgress<int>
    {
        private readonly BackgroundWorker _backgroundWorker;
        private readonly object? _userState = null;
        private int _maximum = 100;
        private int _step = 10;
        private int _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Progress"/> class.
        /// </summary>
        /// <param name="backgroundWorker"></param>
        /// <param name="userState">A unique object indicating the user state.</param>
        /// <param name="maximum">The maximum value of the range. The default is 100.</param>
        /// <param name="step">The amount that a call to the <see cref='PerformStep'/> method increases the current value. The default is 10.</param>
        /// <exception cref="ArgumentException"></exception>
        public Progress(BackgroundWorker backgroundWorker, object? userState, int maximum = 100, int step = 10)
        {
            if (maximum < 0)
                throw new ArgumentException(null, nameof(maximum));

            _backgroundWorker = backgroundWorker;
            _backgroundWorker.WorkerReportsProgress = true;
            _userState = userState;
            _maximum = maximum;
            _step = step;
        }

        /// <summary>
        /// Advances the current value by the specified amount.
        /// </summary>
        /// <param name="value"></param>
        public void Increment(int value)
        {
            _value += value;

            if (_value < 0)
                _value = 0;

            if (_value > _maximum)
                _value = _maximum;

            Report(_value);
        }

        /// <summary>
        /// Advances the current value by the amount of the step property.
        /// </summary>
        public void PerformStep()
        {
            Increment(_step);
        }

        /// <summary>
        /// Reports a progress update.
        /// </summary>
        /// <param name="value">The value of the updated progress.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Report(int value)
        {
            if (value < 0 || value > _maximum)
                throw new ArgumentOutOfRangeException(nameof(value), value, null);

            var percentProgress = MathHelper.CalculatePercentage(value, _maximum);
            _backgroundWorker.ReportProgress(percentProgress, _userState);
        }

        /// <summary>
        /// Gets or sets the maximum value of the progress.
        /// </summary>
        public int Maximum
        {
            get => _maximum;
            set
            {
                if (value == _maximum)
                    return;

                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);

                _maximum = value;

                if (_value > _maximum)
                    _value = _maximum;
            }
        }

        /// <summary>
        /// Gets or sets the amount that a call to the <see cref='PerformStep'/> method increases the current value.
        /// </summary>
        public int Step
        {
            get => _step;
            set
            {
                _step = value;
            }
        }

        /// <summary>
        /// Gets or sets the current value of the progress.
        /// </summary>
        public int Value
        {
            get => _value;
            set
            {
                if (value == _value)
                    return;

                if (value < 0 || value > _maximum)
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);

                _value = value;
                Report(_value);
            }
        }
    }
}

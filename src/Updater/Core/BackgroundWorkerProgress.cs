using System;
using System.ComponentModel;

namespace Updater.Core
{
    /// <summary>
    /// Represents the progress of a <see cref="BackgroundWorker"/> operation.
    /// </summary>
    public sealed class BackgroundWorkerProgress : IProgress<int>
    {
        private readonly BackgroundWorker _backgroundWorker;
        private readonly object? _userState;
        private int _maximum = 100;
        private int _step = 10;
        private int _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundWorkerProgress"/> class.
        /// </summary>
        /// <param name="backgroundWorker">A <see cref="BackgroundWorker"/> instance.</param>
        /// <param name="userState">A unique object indicating the user state.</param>
        /// <param name="maximum">The maximum value of the range. The default is 100.</param>
        /// <param name="step">The amount to increment the value. The default is 10.</param>
        public BackgroundWorkerProgress(BackgroundWorker backgroundWorker, object? userState, int maximum = 100, int step = 10)
        {
            ArgumentNullException.ThrowIfNull(backgroundWorker, nameof(backgroundWorker));
            ArgumentOutOfRangeException.ThrowIfNegative(maximum, nameof(maximum));

            _backgroundWorker = backgroundWorker;
            _backgroundWorker.WorkerReportsProgress = true;
            _userState = userState;
            _maximum = maximum;
            _step = step;
        }

        /// <summary>
        /// Increments the current value by the specified amount.
        /// </summary>
        /// <param name="value">The amount to increment the current value.</param>
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
        /// Increments the current value by the amount of the step property.
        /// </summary>
        public void PerformStep()
        {
            Increment(_step);
        }

        /// <summary>
        /// Reports a progress update.
        /// </summary>
        /// <param name="value">The value of the updated progress.</param>
        public void Report(int value)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(value));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, _maximum, nameof(value));

            var percentage = (int)((double)value / _maximum * 100);
            _backgroundWorker.ReportProgress(percentage, _userState);
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        public int Maximum
        {
            get => _maximum;
            set
            {
                if (value == _maximum)
                    return;

                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(value));
                _maximum = value;

                if (_value > _maximum)
                    _value = _maximum;
            }
        }

        /// <summary>
        /// Gets or sets the amount to increment the current value.
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
        /// Gets or sets the current value.
        /// </summary>
        public int Value
        {
            get => _value;
            set
            {
                if (value == _value)
                    return;

                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(value));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value, _maximum, nameof(value));

                _value = value;
                Report(_value);
            }
        }
    }
}

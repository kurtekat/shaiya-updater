using System.ComponentModel;
using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class ProgressTests
    {
        private BackgroundWorker _backgroundWorker;
        private Progress _progress;

        [SetUp]
        public void SetUp()
        {
            _backgroundWorker = new BackgroundWorker();
            _progress = new Progress(_backgroundWorker, null);
        }

        [TearDown]
        public void TearDown()
        {
            _backgroundWorker.Dispose();
        }

        [Test]
        public void IncrementGreaterThanMaximumTest()
        {
            _progress.Increment(_progress.Maximum + 1);
            Assert.That(_progress.Value, Is.Not.GreaterThan(_progress.Maximum));
        }

        [Test]
        public void IncrementLessThanZeroTest()
        {
            _progress.Increment(-1);
            Assert.That(_progress.Value, Is.Not.LessThan(0));
        }

        [Test]
        public void IncrementTest()
        {
            var value = 1;
            _progress.Increment(value);
            Assert.That(_progress.Value, Is.EqualTo(value));
        }

        [Test]
        public void MaximumParameterTest()
        {
            Assert.Throws<ArgumentException>(() => new Progress(_backgroundWorker, null, -1));
        }

        [Test]
        public void MaximumSetLessThanZeroTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _progress.Maximum = -1);
        }

        [Test]
        public void PerformStepGreaterThanMaximumTest()
        {
            _progress.Value = _progress.Maximum;
            _progress.PerformStep();
            Assert.That(_progress.Value, Is.Not.GreaterThan(_progress.Maximum));
        }

        [Test]
        public void PerformStepTest()
        {
            _progress.PerformStep();
            Assert.That(_progress.Value, Is.EqualTo(_progress.Step));
        }

        [Test]
        public void ReportLessThanZeroTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _progress.Report(-1));
        }

        [Test]
        public void ReportGreaterThanMaximumTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _progress.Report(_progress.Maximum + 1));
        }

        [Test]
        public void ValueSetLessThanZeroTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _progress.Value = -1);
        }

        [Test]
        public void ValueSetGreaterThanMaximumTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _progress.Value = _progress.Maximum + 1);
        }
    }
}

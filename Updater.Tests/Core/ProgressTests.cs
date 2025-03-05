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
        public void IncrementTest()
        {
            var value = 1;
            _progress.Increment(value);
            Assert.That(_progress.Value, Is.EqualTo(value));

            _progress.Increment(-1);
            Assert.That(_progress.Value, Is.Not.LessThan(0));

            _progress.Increment(_progress.Maximum + 1);
            Assert.That(_progress.Value, Is.Not.GreaterThan(_progress.Maximum));
        }

        [Test]
        public void MaximumParameterTest()
        {
            Assert.Throws<ArgumentException>(() => new Progress(_backgroundWorker, null, -1));
        }

        [Test]
        public void PerformStepTest()
        {
            _progress.PerformStep();
            Assert.That(_progress.Value, Is.EqualTo(_progress.Step));

            _progress.Value = _progress.Maximum;
            _progress.PerformStep();
            Assert.That(_progress.Value, Is.Not.GreaterThan(_progress.Maximum));
        }

        [Test]
        public void ReportOutOfRangeTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _progress.Report(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _progress.Report(_progress.Maximum + 1));
        }

        [Test]
        public void SetMaximumOutOfRangeTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _progress.Maximum = -1);
        }

        [Test]
        public void SetValueOutOfRangeTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _progress.Value = -1);
            Assert.Throws<ArgumentOutOfRangeException>(() => _progress.Value = _progress.Maximum + 1);
        }
    }
}

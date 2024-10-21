using System.ComponentModel;
using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class PatchProgressTests
    {
        private const int FileCount = 10;
        private static readonly BackgroundWorker _backgroundWorker = new();
        private PatchProgress _progress;

        [SetUp]
        public void SetUp()
        {
            _backgroundWorker.WorkerReportsProgress = true;
            _progress = new PatchProgress(FileCount, _backgroundWorker);
        }

        [Test]
        public void MaximumShouldBeEqualToFileCount()
        {
            Assert.That(_progress.Maximum, Is.EqualTo(FileCount));
        }

        [Test]
        public void ShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PatchProgress(0, _backgroundWorker));
        }

        [Test]
        public void ValueShouldBeZero()
        {
            Assert.That(_progress.Value, Is.Zero);
        }

        [Test]
        public void ValueShouldBeEqualToMaximum()
        {
            for (int i = 0; i < _progress.Maximum; i++)
                _progress.PerformStep();

            Assert.That(_progress.Value, Is.EqualTo(_progress.Maximum));
        }
    }
}

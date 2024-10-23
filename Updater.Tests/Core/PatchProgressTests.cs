using System.ComponentModel;
using Updater.Common;
using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class PatchProgressTests
    {
        private const int FileCount = 10;
        private BackgroundWorker _backgroundWorker;
        private ProgressReport _progressReport;
        private PatchProgress _progress;

        [SetUp]
        public void SetUp()
        {
            _backgroundWorker = new BackgroundWorker();
            _progressReport = new ProgressReport();
            _progress = new PatchProgress(FileCount, _backgroundWorker, _progressReport);
        }

        [TearDown]
        public void TearDown()
        {
            _backgroundWorker.Dispose();
        }

        [Test]
        public void MaximumShouldBeEqualToFileCount()
        {
            Assert.That(_progress.Maximum, Is.EqualTo(FileCount));
        }

        [Test]
        public void ShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PatchProgress(0, _backgroundWorker, _progressReport));
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

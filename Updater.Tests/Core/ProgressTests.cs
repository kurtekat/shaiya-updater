using System.ComponentModel;
using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class ProgressTests
    {
        private BackgroundWorker _backgroundWorker;
        private ProgressReport _progressReport;

        [SetUp]
        public void SetUp()
        {
            _backgroundWorker = new BackgroundWorker();
            _progressReport = new ProgressReport();
        }

        [TearDown]
        public void TearDown()
        {
            _backgroundWorker.Dispose();
        }

        [Test]
        public void ValueShouldBeEqualToMaximum()
        {
            int maximum = 10;
            int step = 1;
            var progress = new Progress(_backgroundWorker, null, maximum, step);

            for (int i = 0; i < maximum; i++)
                progress.PerformStep();

            Assert.That(progress.Value, Is.EqualTo(progress.Maximum));
        }

        [Test]
        public void ValueShouldNotBeGreaterThanMaximum()
        {
            int maximum = 100;
            int value = 101;
            var progress = new Progress(_backgroundWorker, null, maximum);

            progress.Increment(value);
            Assert.That(progress.Value, Is.Not.GreaterThan(progress.Maximum));
        }
    }
}

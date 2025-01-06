using System.ComponentModel;
using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class ProgressTests
    {
        private BackgroundWorker _backgroundWorker;

        [SetUp]
        public void SetUp()
        {
            _backgroundWorker = new BackgroundWorker();
        }

        [TearDown]
        public void TearDown()
        {
            _backgroundWorker.Dispose();
        }

        [Test]
        public void ConstructorTest()
        {
            var progress = new Progress(_backgroundWorker, null);

            Assert.Multiple(() =>
            {
                Assert.That(progress.Maximum, Is.Not.Zero);
                Assert.That(progress.Step, Is.Not.Zero);
                Assert.That(progress.Value, Is.Zero);
            });
        }

        [Test]
        public void IncrementTest()
        {
            int maximum = 10;
            int value = 1;
            var progress = new Progress(_backgroundWorker, null, maximum);

            for (int i = 0; i < progress.Maximum; i++)
                progress.Increment(value);

            Assert.That(progress.Value, Is.EqualTo(progress.Maximum));

            progress.Increment(value);
            Assert.That(progress.Value, Is.Not.GreaterThan(progress.Maximum));
        }

        [Test]
        public void PerformStepTest()
        {
            int maximum = 10;
            int step = 1;
            var progress = new Progress(_backgroundWorker, null, maximum, step);

            for (int i = 0; i < maximum; i++)
                progress.PerformStep();

            Assert.That(progress.Value, Is.EqualTo(progress.Maximum));

            progress.PerformStep();
            Assert.That(progress.Value, Is.Not.GreaterThan(progress.Maximum));
        }
    }
}

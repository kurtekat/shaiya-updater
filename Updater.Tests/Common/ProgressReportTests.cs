using Updater.Common;

namespace Updater.Tests.Common
{
    [TestFixture]
    public class ProgressReportTests
    {
        private ProgressReport _progressReport;

        [SetUp]
        public void SetUp()
        {
            _progressReport = new ProgressReport();
        }

        [Test]
        public void MessageShouldBeEmpty()
        {
            Assert.That(_progressReport.Message, Is.Empty);
        }

        [Test]
        public void ProgressBarShouldBeZero()
        {
            Assert.That(_progressReport.ByProgressBar, Is.Zero);
        }
    }
}

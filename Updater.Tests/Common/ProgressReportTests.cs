using Updater.Core;

namespace Updater.Tests.Common
{
    [TestFixture]
    public class ProgressReportTests
    {
        [Test]
        public void ConstructorTest()
        {
            var progressReport = new ProgressReport();

            Assert.Multiple(() =>
            {
                Assert.That(progressReport.Message, Is.Empty);
                Assert.That(progressReport.ByProgressBar, Is.Zero);
            });
        }
    }
}

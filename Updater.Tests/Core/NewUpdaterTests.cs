using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class NewUpdaterTests
    {
        [Test]
        public void PathTest()
        {
            var newUpdater = new NewUpdater();
            Assert.That(newUpdater.Path, Does.EndWith(NewUpdater.FileName));
        }

        [Test]
        public void UrlTest()
        {
            var newUpdater = new NewUpdater();
            Assert.Multiple(() =>
            {
                Assert.That(newUpdater.Url, Does.EndWith(NewUpdater.FileName));
                Assert.That(Uri.IsWellFormedUriString(newUpdater.Url, UriKind.Absolute), Is.True);
            });
        }
    }
}

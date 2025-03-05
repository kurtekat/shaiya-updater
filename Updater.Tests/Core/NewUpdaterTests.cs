using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class NewUpdaterTests
    {
        private NewUpdater _newUpdater;

        [SetUp]
        public void SetUp()
        {
            _newUpdater = new NewUpdater();
        }

        [Test]
        public void PathTest()
        {
            Assert.That(_newUpdater.Path, Does.EndWith(NewUpdater.FileName));
        }

        [Test]
        public void UrlTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_newUpdater.Url, Does.EndWith(NewUpdater.FileName));
                Assert.That(Uri.IsWellFormedUriString(_newUpdater.Url, UriKind.Absolute), Is.True);
            });
        }
    }
}

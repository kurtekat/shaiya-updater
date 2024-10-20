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
        public void FileNameShouldNotBeEmpty()
        {
            Assert.That(NewUpdater.FileName, Is.Not.Empty);
        }

        [Test]
        public void PathShouldEndWithFileName()
        {
            Assert.That(_newUpdater.Path, Does.EndWith(NewUpdater.FileName));
        }

        [Test]
        public void UrlShouldEndWithFileName()
        {
            Assert.That(_newUpdater.Url, Does.EndWith(NewUpdater.FileName));
        }

        [Test]
        public void UrlShouldBeWellFormedUriString()
        {
            Assert.That(Uri.IsWellFormedUriString(_newUpdater.Url, UriKind.Absolute), Is.True);
        }
    }
}

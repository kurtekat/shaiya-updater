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
        public void FileName_IsNotEmpty()
        {
            Assert.That(NewUpdater.FileName, Is.Not.Empty);
        }

        [Test]
        public void Url_DoesEndWith_FileName()
        {
            Assert.That(_newUpdater.Url, Does.EndWith(NewUpdater.FileName));
        }

        [Test]
        public void Url_IsWellFormedUriString()
        {
            Assert.That(Uri.IsWellFormedUriString(_newUpdater.Url, UriKind.Absolute), Is.True);
        }
    }
}

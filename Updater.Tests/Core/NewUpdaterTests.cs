using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class NewUpdaterTests
    {
        [Test]
        public void ConstructorTest()
        {
            var newUpdater = new NewUpdater();

            Assert.Multiple(() =>
            {
                Assert.That(newUpdater.Path, Does.EndWith(NewUpdater.FileName));
                Assert.That(newUpdater.Url, Does.EndWith(NewUpdater.FileName));
                Assert.That(Uri.IsWellFormedUriString(newUpdater.Url, UriKind.Absolute), Is.True);
            });
        }

        [Test]
        public void FileNameIsNotEmpty()
        {
            Assert.That(NewUpdater.FileName, Is.Not.Empty);
        }
    }
}

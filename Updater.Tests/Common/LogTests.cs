using Updater.Common;

namespace Updater.Tests.Common
{
    [TestFixture]
    public class LogTests
    {
        private Log _log;

        [SetUp]
        public void SetUp()
        {
            _log = new Log();
        }

        [Test]
        public void FileNameShouldNotBeEmpty()
        {
            Assert.That(Log.FileName, Is.Not.Empty);
        }

        [Test]
        public void FileShouldContainContents()
        {
            const string contents = "1234";
            _log.Write(contents);

            string text = File.ReadAllText(_log.Path);
            Assert.That(text, Does.Contain(contents));
        }

        [Test]
        public void PathShouldEndWithFileName()
        {
            Assert.That(_log.Path, Does.EndWith(Log.FileName));
        }
    }
}

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
            var random = new Random();
            var number = random.Next(int.MaxValue);

            var contents = number.ToString();
            _log.Write(contents);

            var text = File.ReadAllText(_log.Path);
            Assert.That(text, Does.Contain(contents));
        }

        [Test]
        public void PathShouldEndWithFileName()
        {
            Assert.That(_log.Path, Does.EndWith(Log.FileName));
        }
    }
}

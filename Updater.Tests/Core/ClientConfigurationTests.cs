using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class ClientConfigurationTests
    {
        private ClientConfiguration _config;

        [SetUp]
        public void SetUp()
        {
            _config = new ClientConfiguration();
        }

        [Test]
        public void CheckVersionShouldBeZero()
        {
            Assume.That(_config.CheckVersion, Is.Zero);
        }

        [Test]
        public void CurrentVersionShouldNotBeZero()
        {
            Assert.That(_config.CurrentVersion, Is.Not.Zero);
        }

        [Test]
        public void FileNameShouldNotBeEmpty()
        {
            Assert.That(ClientConfiguration.FileName, Is.Not.Empty);
        }

        [Test]
        public void PathShouldEndWithFileName()
        {
            Assert.That(_config.Path, Does.EndWith(ClientConfiguration.FileName));
        }

        [Test]
        public void StartUpdateShouldBeEmpty()
        {
            Assume.That(_config.StartUpdate, Is.Empty);
        }
    }
}

using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class ClientConfigurationTests
    {
        private ClientConfiguration _clientCfg;

        [SetUp]
        public void SetUp()
        {
            _clientCfg = new ClientConfiguration();
        }

        [Test]
        public void CheckVersionShouldBeZero()
        {
            Assume.That(_clientCfg.CheckVersion, Is.Zero);
        }

        [Test]
        public void CurrentVersionShouldNotBeZero()
        {
            Assert.That(_clientCfg.CurrentVersion, Is.Not.Zero);
        }

        [Test]
        public void FileNameShouldNotBeEmpty()
        {
            Assert.That(ClientConfiguration.FileName, Is.Not.Empty);
        }

        [Test]
        public void PathShouldEndWithFileName()
        {
            Assert.That(_clientCfg.Path, Does.EndWith(ClientConfiguration.FileName));
        }

        [Test]
        public void StartUpdateShouldBeEmpty()
        {
            Assume.That(_clientCfg.StartUpdate, Is.Empty);
        }
    }
}

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
        public void CheckVersion_MaybeIsZero()
        {
            Assume.That(_clientCfg.CheckVersion, Is.Zero);
        }

        [Test]
        public void CurrentVersion_IsNotZero()
        {
            Assert.That(_clientCfg.CurrentVersion, Is.Not.Zero);
        }

        [Test]
        public void FileName_IsNotEmpty()
        {
            Assert.That(ClientConfiguration.FileName, Is.Not.Empty);
        }

        [Test]
        public void Path_DoesEndWithFileName()
        {
            Assert.That(_clientCfg.Path, Does.EndWith(ClientConfiguration.FileName));
        }

        [Test]
        public void StartUpdate_MaybeIsEmpty()
        {
            Assume.That(_clientCfg.StartUpdate, Is.Empty);
        }
    }
}

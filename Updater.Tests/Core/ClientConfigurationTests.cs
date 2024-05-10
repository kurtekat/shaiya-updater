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
        public void CheckVersion_Maybe_IsZero()
        {
            Assume.That(_clientCfg.CheckVersion, Is.Zero);
        }

        [Test]
        public void CurrentVersion_IsNotZero()
        {
            Assert.That(_clientCfg.CurrentVersion, Is.Not.Zero);
        }

        [Test]
        public void StartUpdate_Maybe_IsEmpty()
        {
            Assume.That(_clientCfg.StartUpdate, Is.Empty);
        }
    }
}

using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class ServerConfigurationTests
    {
        private static readonly HttpClient _httpClient = new();
        private ServerConfiguration _serverCfg;

        [SetUp]
        public void SetUp()
        {
            _serverCfg = new ServerConfiguration(_httpClient);
        }

        [Test]
        public void CheckVersion_Maybe_IsZero()
        {
            Assume.That(_serverCfg.CheckVersion, Is.Zero);
        }

        [Test]
        public void FileName_IsNotEmpty()
        {
            Assert.That(ServerConfiguration.FileName, Is.Not.Empty);
        }

        [Test]
        public void PatchFileVersion_IsNotZero()
        {
            Assert.That(_serverCfg.PatchFileVersion, Is.Not.Zero);
        }

        [Test]
        public void UpdateVersion_Maybe_IsZero()
        {
            Assume.That(_serverCfg.UpdaterVersion, Is.Zero);
        }
    }
}

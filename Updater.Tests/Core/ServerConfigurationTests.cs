using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class ServerConfigurationTests
    {
        private HttpClient _httpClient;
        private ServerConfiguration _config;

        [SetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient();
            _config = new ServerConfiguration(_httpClient);
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
        }

        [Test]
        public void CheckVersionShouldBeZero()
        {
            Assume.That(_config.CheckVersion, Is.Zero);
        }

        [Test]
        public void FileNameShouldNotBeEmpty()
        {
            Assert.That(ServerConfiguration.FileName, Is.Not.Empty);
        }

        [Test]
        public void PatchFileVersionShouldNotBeZero()
        {
            Assert.That(_config.PatchFileVersion, Is.Not.Zero);
        }

        [Test]
        public void UpdateVersionShouldNotBeZero()
        {
            Assert.That(_config.UpdaterVersion, Is.Not.Zero);
        }
    }
}

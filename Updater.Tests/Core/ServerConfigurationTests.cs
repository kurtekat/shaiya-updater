using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class ServerConfigurationTests
    {
        private HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient();
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
        }

        [Test]
        public void ConstructorTest()
        {
            var configuration = new ServerConfiguration(_httpClient);

            Assert.Multiple(() =>
            {
                Assert.That(configuration.PatchFileVersion, Is.Not.Zero);
                Assert.That(configuration.UpdaterVersion, Is.Not.Zero);
            });

            Assume.That(configuration.CheckVersion, Is.Zero);
        }

        [Test]
        public void FileNameIsNotEmpty()
        {
            Assert.That(ServerConfiguration.FileName, Is.Not.Empty);
        }
    }
}

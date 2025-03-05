using Updater.Configuration;

namespace Updater.Tests.Configuration
{
    [TestFixture]
    public class ServerConfigurationTests
    {
        private HttpClient _httpClient;
        private ServerConfiguration _configuration;

        [SetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient();
            _configuration = new ServerConfiguration(_httpClient);
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
            _configuration.Dispose();
        }

        [Test]
        public void PathTest()
        {
            Assert.That(_configuration.Source.Path, Does.EndWith(ClientConfiguration.FileName));
        }
    }
}

using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class ClientConfigurationTests
    {
        [Test]
        public void PathTest()
        {
            var configuration = new ClientConfiguration();
            Assert.That(configuration.Path, Does.EndWith(ClientConfiguration.FileName));
        }
    }
}

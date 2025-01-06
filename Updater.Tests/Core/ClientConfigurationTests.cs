using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class ClientConfigurationTests
    {
        [Test]
        public void ConstructorTest()
        {
            var configuration = new ClientConfiguration();

            Assert.Multiple(() =>
            {
                Assert.That(configuration.CurrentVersion, Is.Not.Zero);
                Assert.That(configuration.Path, Does.EndWith(ClientConfiguration.FileName));
            });

            Assume.That(configuration.CheckVersion, Is.Zero);
            Assume.That(configuration.StartUpdate, Is.Empty);
        }

        [Test]
        public void FileNameIsNotEmpty()
        {
            Assert.That(ClientConfiguration.FileName, Is.Not.Empty);
        }
    }
}

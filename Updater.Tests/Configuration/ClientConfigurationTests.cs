using Updater.Configuration;

namespace Updater.Tests.Configuration
{
    [TestFixture]
    public class ClientConfigurationTests
    {
        private ClientConfiguration _configuration;

        [SetUp]
        public void SetUp()
        {
            _configuration = new ClientConfiguration();
        }

        [TearDown]
        public void TearDown()
        {
            _configuration.Dispose();
        }

        [Test]
        public void PathTest()
        {
            Assert.That(_configuration.Source.Path, Does.EndWith(ClientConfiguration.FileName));
        }

        [TestCase(0, 0, "")]
        [TestCase(3, 32, "UPDATE_END")]
        public void SaveTest(int checkVersion, int currentVersion, string startUpdate)
        {
            _configuration.CheckVersion = checkVersion;
            _configuration.CurrentVersion = currentVersion;
            _configuration.StartUpdate = startUpdate;
            _configuration.Save();
            Assert.That(File.Exists(_configuration.Source.Path));

            _configuration.Load();

            _configuration.TryGet("Version:CheckVersion", out string? v1);
            Assert.That(Convert.ToInt32(v1), Is.EqualTo(checkVersion));

            _configuration.TryGet("Version:CurrentVersion", out string? v2);
            Assert.That(Convert.ToInt32(v2), Is.EqualTo(currentVersion));

            _configuration.TryGet("Version:StartUpdate", out string? v3);
            Assert.That(v3, Is.EqualTo(startUpdate));
        }

        [Test]
        public void SetCheckVersionTest()
        {
            _configuration.CheckVersion = 3;
            _configuration.TryGet("Version:CheckVersion", out var value);
            Assert.That(Convert.ToInt32(value), Is.EqualTo(_configuration.CheckVersion));
        }

        [Test]
        public void SetCurrentVersionTest()
        {
            _configuration.CurrentVersion = 32;
            _configuration.TryGet("Version:CurrentVersion", out var value);
            Assert.That(Convert.ToInt32(value), Is.EqualTo(_configuration.CurrentVersion));
        }

        [Test]
        public void SetStartUpdateTest()
        {
            _configuration.StartUpdate = "UPDATE_END";
            _configuration.TryGet("Version:StartUpdate", out var value);
            Assert.That(value, Is.EqualTo(_configuration.StartUpdate));
        }
    }
}

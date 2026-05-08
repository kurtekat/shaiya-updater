using System.IO;
using Updater.Configuration;

namespace Updater.Tests.Configuration
{
    [TestFixture]
    public class IniTests
    {
        private Ini _ini;

        [SetUp]
        public void SetUp()
        {
            _ini = new Ini();
            _ini.Parse(new FileInfo("Configuration/version.ini"));
        }

        [TestCase(3, 303, "UPDATE_END")]
        public void GetValueOrDefaultTest(int checkVersion, int currentVersion, string startUpdate)
        {
            var v1 = _ini.GetValueOrDefault("Version:CheckVersion", 0);
            Assert.That(v1, Is.EqualTo(checkVersion));

            var v2 = _ini.GetValueOrDefault("Version:CurrentVersion", 0);
            Assert.That(v2, Is.EqualTo(currentVersion));

            var v3 = _ini.GetValueOrDefault("Version:StartUpdate", "");
            Assert.That(v3, Is.EqualTo(startUpdate));
        }

        [Test]
        public void SetOrAddValueTest()
        {
            var v1 = 1;
            _ini.SetOrAddValue("Section:Key", v1);
            var v2 = _ini.GetValueOrDefault("Section:Key", 0);
            Assert.That(v2, Is.EqualTo(v1));

            var v3 = string.Empty;
            _ini.SetOrAddValue("Section:Key", v3);
            var v4 = _ini.GetValueOrDefault("Section:Key", "Value");
            Assert.That(v3, Is.EqualTo(v4));
        }

        [Test]
        public void WriteTest()
        {
            var ini = new Ini();
            ini.SetOrAddValue("Section1:Key1", "Value1");
            ini.SetOrAddValue("Section1:Key2", "Value2");
            ini.SetOrAddValue("Section2:Key1", "Value3");
            ini.SetOrAddValue("Section2:Key2", "Value4");

            Directory.CreateDirectory("Configuration");
            var fileInfo = new FileInfo("Configuration/WriteTest.ini");
            ini.Write(fileInfo);
            ini.Parse(fileInfo);

            var v1 = ini.GetValueOrDefault("Section1:Key1", string.Empty);
            Assert.That(v1, Is.EqualTo("Value1"));
            var v2 = ini.GetValueOrDefault("Section1:Key2", string.Empty);
            Assert.That(v2, Is.EqualTo("Value2"));
            var v3 = ini.GetValueOrDefault("Section2:Key1", string.Empty);
            Assert.That(v3, Is.EqualTo("Value3"));
            var v4 = ini.GetValueOrDefault("Section2:Key2", string.Empty);
            Assert.That(v4, Is.EqualTo("Value4"));
        }
    }
}

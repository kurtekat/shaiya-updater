using Updater.Helpers;

namespace Updater.Tests.Helpers
{
    [TestFixture]
    public class IniHelperTests
    {
        private const string Section1 = "section1";
        private const string Section2 = "section2";
        private const string Section3 = "section3";
        private const string Key1 = "key1";
        private const string Key2 = "key2";
        private const string Key3 = "key3";
        private const string Value1 = "value1";
        private const string Value2 = "value2";
        private const string Value3 = "value3";
        private string _iniPath;

        [SetUp]
        public void SetUp()
        {
            _iniPath = Path.Combine(Directory.GetCurrentDirectory(), "test.ini");
            string text1 = $"[{Section1}]\n{Key1}={Value1}\n{Key2}={Value2}\n";
            string text2 = $"[{Section2}]\n{Key1}={Value1}\n{Key2}={Value2}\n";
            File.WriteAllText(_iniPath, string.Concat(text1, text2));
        }

        [Test]
        public void ShouldCreateKeyName()
        {
            IniHelper.WritePrivateProfileString(Section1, Key3, Value3, _iniPath);
            var text = IniHelper.GetPrivateProfileString(Section1, Key3, "", _iniPath);
            Assert.That(text, Is.EqualTo(Value3));
        }

        [Test]
        public void ShouldCreateSectionName()
        {
            IniHelper.WritePrivateProfileString(Section3, Key1, Value1, _iniPath);
            var text = IniHelper.GetPrivateProfileString(Section3, Key1, "", _iniPath);
            Assert.That(text, Is.EqualTo(Value1));
        }

        [Test]
        public void ShouldDeleteKeyName()
        {
            IniHelper.WritePrivateProfileString(Section1, Key2, null, _iniPath);
            var text = IniHelper.GetPrivateProfileString(Section1, Key2, "", _iniPath);
            Assert.That(text, Is.Empty);
        }

        [Test]
        public void ShouldDeleteSectionName()
        {
            IniHelper.WritePrivateProfileString(Section2, null, "", _iniPath);
            var text = IniHelper.GetPrivateProfileString(Section2, Key1, "", _iniPath);
            Assert.That(text, Is.Empty);
        }

        [Test]
        public void ShouldReturnAllSectionKeyNames()
        {
            var text = IniHelper.GetPrivateProfileString(Section1, null, null, _iniPath);
            Assert.That(text, Does.Contain(Key1));
            Assert.That(text, Does.Contain(Key2));
        }

        [Test]
        public void ShouldReturnAllSectionNames()
        {
            var text = IniHelper.GetPrivateProfileString(null, null, null, _iniPath);
            Assert.That(text, Does.Contain(Section1));
            Assert.That(text, Does.Contain(Section2));
        }

        [Test]
        public void ShouldReturnDefaultValue()
        {
            const string defaultValue = "1234";
            var text = IniHelper.GetPrivateProfileString("", "", defaultValue, _iniPath);
            Assert.That(text, Is.EqualTo(defaultValue));
        }

        [Test]
        public void ShouldReturnEmptyString()
        {
            var text = IniHelper.GetPrivateProfileString("", "", null, _iniPath);
            Assert.That(text, Is.Empty);
        }

        [Test]
        public void ShouldReturnKeyNameValue()
        {
            var text = IniHelper.GetPrivateProfileString(Section1, Key1, "", _iniPath);
            Assert.That(text, Is.EqualTo(Value1));
        }
    }
}

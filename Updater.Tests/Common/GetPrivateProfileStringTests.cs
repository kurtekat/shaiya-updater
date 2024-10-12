using Updater.Common;

namespace Updater.Tests.Common
{
    [TestFixture]
    public class GetPrivateProfileStringTests
    {
        private const string Section1 = "section1";
        private const string Section2 = "section2";
        private const string Key1 = "key1";
        private const string Key2 = "key2";
        private const string Value1 = "value1";
        private const string Value2 = "value2";
        private string _iniPath;

        [SetUp]
        public void SetUp()
        {
            _iniPath = Path.Combine(Directory.GetCurrentDirectory(), "test.ini");
            string text1 = $"[{Section1}]\n{Key1}={Value1}\n{Key2}={Value2}\n\n";
            string text2 = $"[{Section2}]\n{Key1}={Value1}\n{Key2}={Value2}\n\n";
            File.WriteAllText(_iniPath, string.Concat(text1, text2));
        }

        [Test]
        public void ShouldReturnAllSectionKeyNames()
        {
            var text = Util.GetPrivateProfileString(Section1, null, null, _iniPath);
            Assert.That(text, Does.Contain(Key1));
            Assert.That(text, Does.Contain(Key2));
        }

        [Test]
        public void ShouldReturnAllSectionNames()
        {
            var text = Util.GetPrivateProfileString(null, null, null, _iniPath);
            Assert.That(text, Does.Contain(Section1));
            Assert.That(text, Does.Contain(Section2));
        }

        [Test]
        public void ShouldReturnDefaultValue()
        {
            const string defaultValue = "1234";
            var text = Util.GetPrivateProfileString("", "", defaultValue, _iniPath);
            Assert.That(text, Is.EqualTo(defaultValue));
        }

        [Test]
        public void ShouldReturnEmptyString()
        {
            var text = Util.GetPrivateProfileString("", "", null, _iniPath);
            Assert.That(text, Is.Empty);
        }

        [Test]
        public void ShouldReturnKeyNameValue()
        {
            var text = Util.GetPrivateProfileString(Section1, Key1, "", _iniPath);
            Assert.That(text, Is.EqualTo(Value1));
        }
    }
}

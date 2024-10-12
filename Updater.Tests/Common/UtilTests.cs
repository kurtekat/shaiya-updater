using Updater.Common;

namespace Updater.Tests.Common
{
    [TestFixture]
    public class UtilTests
    {
        private const string FileName = "Updater.Tests.Common.ini";
        private const string Section1 = "Section1";
        private const string Section2 = "Section2";
        private const string Key1 = "Key1";
        private const string Key2 = "Key2";
        private const string Value1 = "Value1";
        private const string Value2 = "Value2";
        private string _path;

        [SetUp]
        public void SetUp()
        {
            _path = Path.Combine(Directory.GetCurrentDirectory(), FileName);

            string text1 = $"[{Section1}]\n{Key1}={Value1}\n{Key2}={Value2}\n\n";
            string text2 = $"[{Section2}]\n{Key1}={Value1}\n{Key2}={Value2}\n\n";
            File.WriteAllText(_path, string.Concat(text1, text2));
        }

        [Test]
        public void GetPrivateProfileString_ReturnsAllSectionKeyNames()
        {
            var text = Util.GetPrivateProfileString(Section1, null, null, _path);
            Assert.That(text, Does.Contain(Key1));
            Assert.That(text, Does.Contain(Key2));
        }

        [Test]
        public void GetPrivateProfileString_ReturnsAllSectionNames()
        {
            var text = Util.GetPrivateProfileString(null, null, null, _path);
            Assert.That(text, Does.Contain(Section1));
            Assert.That(text, Does.Contain(Section2));
        }

        [Test]
        public void GetPrivateProfileString_ReturnsDefaultValue()
        {
            const string defaultValue = "1234";
            var text = Util.GetPrivateProfileString("", "", defaultValue, _path);
            Assert.That(text, Is.EqualTo(defaultValue));
        }

        [Test]
        public void GetPrivateProfileString_ReturnsEmptyString()
        {
            var text = Util.GetPrivateProfileString("", "", null, _path);
            Assert.That(text, Is.Empty);
        }

        [Test]
        public void GetPrivateProfileString_ReturnsKeyNameValue()
        {
            var text = Util.GetPrivateProfileString(Section1, Key1, "", _path);
            Assert.That(text, Is.EqualTo(Value1));
        }
    }
}

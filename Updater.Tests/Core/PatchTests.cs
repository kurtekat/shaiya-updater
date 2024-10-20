using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class PatchTests
    {
        private const string PatchFileName = "ps0002.patch";
        private const uint PatchFileVersion = 2;
        private Patch _patch;

        [SetUp]
        public void SetUp()
        {
            _patch = new Patch(PatchFileVersion);
        }

        [Test]
        public void FileNameShouldBeEqualToPatchFileName()
        {
            Assert.That(_patch.FileName, Is.EqualTo(PatchFileName));
        }

        [Test]
        public void PathShouldEndWithPatchFileName()
        {
            Assert.That(_patch.Path, Does.EndWith(PatchFileName));
        }

        [Test]
        public void ShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Patch(10000));
        }

        [Test]
        public void UrlShouldEndWithPatchFileName()
        {
            Assert.That(_patch.Url, Does.EndWith(PatchFileName));
        }

        [Test]
        public void UrlShouldBeWellFormedUriString()
        {
            Assert.That(Uri.IsWellFormedUriString(_patch.Url, UriKind.Absolute), Is.True);
        }
    }
}

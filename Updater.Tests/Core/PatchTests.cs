using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class PatchTests
    {
        private const string PatchFileName = "ps0002.patch";
        private const uint PatchFileVersion = 2;

        [Test]
        public void ConstructorTest()
        {
            var patch = new Patch(PatchFileVersion);

            Assert.Multiple(() =>
            {
                Assert.That(patch.Path, Does.EndWith(PatchFileName));
                Assert.That(patch.Url, Does.EndWith(PatchFileName));
                Assert.That(Uri.IsWellFormedUriString(patch.Url, UriKind.Absolute), Is.True);
            });
        }

        [Test]
        public void ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Patch(1));
            Assert.Throws<ArgumentException>(() => new Patch(10000));
        }
    }
}

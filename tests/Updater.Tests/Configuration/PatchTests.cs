using System;
using Updater.Configuration;

namespace Updater.Tests.Configuration
{
    [TestFixture]
    public class PatchTests
    {
        private const string PatchFileName = "ps0002.patch";
        private const int PatchFileVersion = 2;
        private Patch _patch;

        [SetUp]
        public void SetUp()
        {
            _patch = new Patch(PatchFileVersion);
        }

        [Test]
        public void UrlTest()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(_patch.Url, Does.EndWith(PatchFileName));
                Assert.That(Uri.IsWellFormedUriString(_patch.Url, UriKind.Absolute), Is.True);
            }
        }

        [Test]
        public void VersionOutOfRangeTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Patch(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Patch(10000));
        }
    }
}

using Updater.Common;
using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class PatchTests
    {
        private const string PatchFileName = "ps0002.patch";
        private const uint PatchFileVersion = 2;
        private static readonly HttpClient _httpClient = new();
        private Patch _patch;

        [SetUp]
        public void SetUp()
        {
            _patch = new Patch(PatchFileVersion);
        }

        [Test]
        public void FileName_IsEqualToPatchFileName()
        {
            Assert.That(_patch.FileName, Is.EqualTo(PatchFileName));
        }

        [Test]
        public void Path_DoesEndWithPatchFileName()
        {
            Assert.That(_patch.Path, Does.EndWith(PatchFileName));
        }

        [Test]
        public void Url_DoesEndWithPatchFileName()
        {
            Assert.That(_patch.Url, Does.EndWith(PatchFileName));
        }

        [Test]
        public void Url_IsWellFormedUriString()
        {
            Assert.That(Uri.IsWellFormedUriString(_patch.Url, UriKind.Absolute), Is.True);
        }

        [Test]
        public void Url_MaybeDownloadToFile()
        {
            Util.DownloadToFile(_httpClient, _patch.Url, _patch.Path);
            Assume.That(File.Exists(_patch.Path), Is.True);
        }
    }
}

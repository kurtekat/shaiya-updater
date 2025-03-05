using System.IO.Compression;
using Updater.Core;
using Updater.Extensions;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class PatchTests
    {
        private const string PatchFileName = "ps0002.patch";
        private const int PatchFileVersion = 2;
        private Patch _patch;
        private HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            _patch = new Patch(PatchFileVersion);
            _httpClient = new HttpClient();
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
        }

        [Test]
        public void ExtractToCurrentDirectoryTest()
        {
            _httpClient.DownloadFile(_patch.Url, _patch.FileName);

            if (File.Exists(_patch.Path))
            {
                Assert.That(_patch.ExtractToCurrentDirectory(), Is.True);

                using var zipArchive = ZipFile.OpenRead(_patch.Path);
                foreach (var entry in zipArchive.Entries)
                    Assert.That(File.Exists(entry.FullName), Is.True);
            }
        }

        [Test]
        public void PathTest()
        {
            Assert.That(_patch.Path, Does.EndWith(PatchFileName));
        }

        [Test]
        public void UrlTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_patch.Url, Does.EndWith(PatchFileName));
                Assert.That(Uri.IsWellFormedUriString(_patch.Url, UriKind.Absolute), Is.True);
            });
        }

        [Test]
        public void VersionParameterTest()
        {
            Assert.Throws<ArgumentException>(() => new Patch(1));
            Assert.Throws<ArgumentException>(() => new Patch(10000));
        }
    }
}

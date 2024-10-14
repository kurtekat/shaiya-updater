using System.IO.Compression;
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
        public void FileNameShouldBeEqualToPatchFileName()
        {
            Assert.That(_patch.FileName, Is.EqualTo(PatchFileName));
        }

        [Test]
        public void FileShouldExtract()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            Util.DownloadToFile(_httpClient, _patch.Url, _patch.Path);
            if (!File.Exists(_patch.Path))
                return;

            var source = ZipFile.OpenRead(_patch.Path);
            var entries = source.Entries;
            source.Dispose();

            _patch.Extract(currentDirectory);

            foreach (var entry in entries)
            {
                var path = Path.Combine(currentDirectory, entry.FullName);
                Assert.That(File.Exists(path), Is.True);
            }
        }

        [Test]
        public void PathShouldEndWithPatchFileName()
        {
            Assert.That(_patch.Path, Does.EndWith(PatchFileName));
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

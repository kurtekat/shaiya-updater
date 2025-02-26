using Updater.Core;
using Updater.Extensions;

namespace Updater.Tests.Extensions
{
    [TestFixture]
    public class HttpClientExtensionsTests
    {
        private const uint PatchFileVersion = 2;
        private HttpClient _httpClient;
        private Patch _patch;

        [SetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient();
            _patch = new Patch(PatchFileVersion);
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
        }

        [Test]
        public void DownloadFileTest()
        {
            _httpClient.DownloadFile(_patch.Url, _patch.FileName);
            Assume.That(File.Exists(_patch.Path), Is.True);
        }
    }
}

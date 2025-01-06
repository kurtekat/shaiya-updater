using Updater.Core;
using Updater.Extensions;

namespace Updater.Tests.Extensions
{
    public class HttpClientExtensionsTests
    {
        private const uint PatchFileVersion = 2;
        private HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient();
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
        }

        [Test]
        public void DownloadFileTest()
        {
            var patch = new Patch(PatchFileVersion);
            _httpClient.DownloadFile(patch.Url, patch.FileName);
            Assume.That(patch.Exists(), Is.True);
        }
    }
}

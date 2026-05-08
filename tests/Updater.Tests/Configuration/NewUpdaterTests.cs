using System;
using Updater.Configuration;

namespace Updater.Tests.Configuration
{
    [TestFixture]
    public class NewUpdaterTests
    {
        private NewUpdater _newUpdater;

        [SetUp]
        public void SetUp()
        {
            _newUpdater = new NewUpdater();
        }

        [Test]
        public void UrlTest()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(_newUpdater.Url, Does.EndWith(_newUpdater.FileName));
                Assert.That(Uri.IsWellFormedUriString(_newUpdater.Url, UriKind.Absolute), Is.True);
            }
        }
    }
}

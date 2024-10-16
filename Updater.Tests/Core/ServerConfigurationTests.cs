﻿using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class ServerConfigurationTests
    {
        private static readonly HttpClient _httpClient = new();
        private ServerConfiguration _serverCfg;

        [SetUp]
        public void SetUp()
        {
            _serverCfg = new ServerConfiguration(_httpClient);
        }

        [Test]
        public void CheckVersionShouldBeZero()
        {
            Assume.That(_serverCfg.CheckVersion, Is.Zero);
        }

        [Test]
        public void FileNameShouldNotBeEmpty()
        {
            Assert.That(ServerConfiguration.FileName, Is.Not.Empty);
        }

        [Test]
        public void PatchFileVersionShouldNotBeZero()
        {
            Assert.That(_serverCfg.PatchFileVersion, Is.Not.Zero);
        }

        [Test]
        public void UpdateVersionShouldNotBeZero()
        {
            Assert.That(_serverCfg.UpdaterVersion, Is.Not.Zero);
        }

        [Test]
        public void UrlShouldEndWithFileName()
        {
            Assert.That(_serverCfg.Url, Does.EndWith(ServerConfiguration.FileName));
        }

        [Test]
        public void UrlShouldBeWellFormedUriString()
        {
            Assert.That(Uri.IsWellFormedUriString(_serverCfg.Url, UriKind.Absolute), Is.True);
        }
    }
}

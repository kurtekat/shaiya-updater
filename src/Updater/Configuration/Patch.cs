using System;

namespace Updater.Configuration
{
    /// <summary>
    /// Represents a zip archive with a .patch extension.
    /// </summary>
    public sealed class Patch
    {
        public string FileName { get; }
        public string Url { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Patch"/> class.
        /// </summary>
        /// <param name="version">The patch version number.</param>
        public Patch(int version)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(version, nameof(version));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(version, 9999, nameof(version));
            // e.g., ps0002.patch
            FileName = $"ps{version:D4}.patch";
            Url = $"{Web.Scheme}://{Web.Root}/shaiya/patch/{FileName}";
        }
    }
}

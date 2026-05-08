namespace Updater.Configuration
{
    /// <summary>
    /// Represents a replacement updater application.
    /// </summary>
    public sealed class NewUpdater
    {
        public string FileName { get; }
        public string Url { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewUpdater"/> class.
        /// </summary>
        public NewUpdater()
        {
            FileName = "new_updater.exe";
            Url = $"{Web.Scheme}://{Web.Root}/shaiya/{FileName}";
        }
    }
}

namespace Updater.Core
{
    static class Constants
    {
        public const uint UpdaterVersion = 1;
        public const string Source = "https://kurtekat.github.io";
        public const string WebBrowserSource = "https://google.com";

        public const string Message0 = "The version does not match. Please install the client again from the homepage.";
        public const string Message1 = "Updater already in operation.";
        public const string Message2 = "Game is still in operation. Please try again after closing the game.";

        public const string ProgressMessage0 = "Downloading updater";
        public const string ProgressMessage1 = "Downloading ({0}/{1})";
        public const string ProgressMessage2 = "Download failed";
        public const string ProgressMessage3 = "Extracting";
        public const string ProgressMessage4 = "Extraction failed";
        public const string ProgressMessage5 = "Updating";
        public const string ProgressMessage6 = "Update error";
        public const string ProgressMessage7 = "Update completed";
        public const string ProgressMessage8 = "Save local version error";
    }
}

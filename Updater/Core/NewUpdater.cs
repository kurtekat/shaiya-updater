namespace Updater.Core
{
    public class NewUpdater
    {
        public const string FileName = "new_updater.exe";
        public readonly string Url = string.Empty;

        public NewUpdater()
        {
            Url = $"{Constants.Source}/shaiya/{FileName}";
        }
    }
}

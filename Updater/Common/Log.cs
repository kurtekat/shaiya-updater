using System.IO;

namespace Updater.Common
{
    public class Log
    {
        public const string FileName = "Updater.Log.txt";
        public readonly string Path = string.Empty;

        public Log()
        {
            Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), FileName);
        }

        public void Write(string contents)
        {
            File.AppendAllText(Path, $"{DateTime.Now}\n{contents}\n");
        }
    }
}

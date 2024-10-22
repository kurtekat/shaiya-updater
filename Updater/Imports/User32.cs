using System.Runtime.InteropServices;

namespace Updater.Imports
{
    static class User32
    {
        [DllImport("User32", EntryPoint = "FindWindowW", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowW(string? className, string? windowName);
    }
}

using System.Runtime.InteropServices;

namespace Updater.Core
{
    static class DllImport
    {
        [DllImport("User32", EntryPoint = "FindWindowW", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowW(string? className, string? windowName);
    }
}

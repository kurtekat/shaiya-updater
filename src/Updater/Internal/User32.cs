using System.Runtime.InteropServices;

namespace Updater.Internal
{
    internal static class User32
    {
        [DllImport("User32", EntryPoint = "FindWindowW", CharSet = CharSet.Unicode)]
        internal static extern nint FindWindowW(string? className, string? windowName);
    }
}

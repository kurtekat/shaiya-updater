using System.Runtime.InteropServices;

namespace Updater.Imports
{
    static class Kernel32
    {
        [DllImport("Kernel32", EntryPoint = "GetPrivateProfileIntW", CharSet = CharSet.Unicode)]
        public static extern uint GetPrivateProfileIntW(string section, string key, int defaultValue, string fileName);

        [DllImport("Kernel32", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode)]
        public static extern uint GetPrivateProfileStringW(string? section, string? key, string? defaultValue, char[] buffer, uint bufferSize, string fileName);

        [DllImport("Kernel32", EntryPoint = "WritePrivateProfileStringW", CharSet = CharSet.Unicode)]
        public static extern int WritePrivateProfileStringW(string section, string? key, string? value, string fileName);
    }
}

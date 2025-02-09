using System.Runtime.InteropServices;

namespace Updater.Core
{
    static class DllImport
    {
        [DllImport("Kernel32", EntryPoint = "GetCurrentProcess")]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("Kernel32", EntryPoint = "GetPrivateProfileIntW", CharSet = CharSet.Unicode)]
        public static extern uint GetPrivateProfileIntW(string section, string key, int defaultValue, string fileName);

        [DllImport("Kernel32", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode)]
        public static extern uint GetPrivateProfileStringW(string? section, string? key, string? defaultValue, char[] buffer, uint bufferSize, string fileName);

        [DllImport("Kernel32", EntryPoint = "TerminateProcess")]
        public static extern int TerminateProcess(IntPtr handle, int exitCode);

        [DllImport("Kernel32", EntryPoint = "WritePrivateProfileStringW", CharSet = CharSet.Unicode)]
        public static extern int WritePrivateProfileStringW(string section, string? key, string? value, string fileName);

        [DllImport("User32", EntryPoint = "FindWindowW", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowW(string? className, string? windowName);

        [DllImport("Updater.Interop", EntryPoint = "DataPatcher", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DataPatcher(Action? progressCallback);

        [DllImport("Updater.Interop", EntryPoint = "RemoveFiles", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RemoveFiles(Action? progressCallback);
    }
}

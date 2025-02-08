using Updater.Core;

namespace Updater.Helpers
{
    public static class IniHelper
    {
        /// <summary>
        /// Retrieves a string from the specified section in an initialization file.
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <remarks>https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-getprivateprofilestringw</remarks>
        public static string GetPrivateProfileString(string? section, string? key, string? defaultValue, string fileName)
        {
            var buffer = new char[short.MaxValue];
            var length = DllImport.GetPrivateProfileStringW(section, key, defaultValue, buffer, (uint)buffer.Length, fileName);
            return new string(buffer, 0, (int)length);
        }

        /// <summary>
        /// Copies a string into the specified section of an initialization file.
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <remarks>https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-writeprivateprofilestringw</remarks>
        public static int WritePrivateProfileString(string section, string? key, string? value, string fileName)
        {
            if (value is not null)
                value += '\0';

            return DllImport.WritePrivateProfileStringW(section, key, value, fileName);
        }
    }
}

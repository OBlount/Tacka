using System;
using System.Runtime.InteropServices;

namespace Tacka_CSGO_Radar_Cheat.Classes.Win32
{
    public static class Kernel
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, ref int lpBuffer, int dwSize, int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, IntPtr bInheritHandle, IntPtr dwProcessId);
    }
}
using System;
using System.Runtime.InteropServices;

namespace Tacka_CSGO_Radar_Cheat.Classes.Win32
{
    class User
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(bool lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowTextA(IntPtr hWnd, string lpString, int nMaxCount);
    }
}
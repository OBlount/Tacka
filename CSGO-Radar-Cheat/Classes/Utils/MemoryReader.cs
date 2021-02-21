using System;
using Tacka_CSGO_Radar_Cheat.Classes.Win32;

namespace Tacka_CSGO_Radar_Cheat.Classes.Utils
{
    class MemoryReader
    {
        public bool ReadInt32(IntPtr hProcess, int lpBaseAddress, ref int lpBuffer)
        {
            return Kernel.ReadProcessMemory(hProcess, lpBaseAddress, ref lpBuffer, sizeof(int), 0);
        }
    }
}
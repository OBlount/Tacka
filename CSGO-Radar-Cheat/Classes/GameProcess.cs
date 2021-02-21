using System;
using System.Diagnostics;
using Tacka_CSGO_Radar_Cheat.Classes.Win32;

namespace Tacka_CSGO_Radar_Cheat.Classes
{
    class GameProcess
    {
        private const string NAME_PROCESS = "csgo";
        private const string NAME_WINDOW = "Counter-Strike: Global Offensive";
        private const string NAME_MODULE_CLIENT = "client.dll";
        private const string NAME_MODULE_ENGINE = "engine.dll";

        private const int PROCESS_VM_READ = 0x0010;

        public string ProcessName { get { return NAME_PROCESS; } set { ProcessName = value; } }
        public string ModuleClientName { get { return NAME_MODULE_CLIENT; } set { ModuleClientName = value; } }
        public string ModuleEngineName { get { return NAME_MODULE_ENGINE; } set { ModuleEngineName = value; } }

        public int GetProccessID()
        {
            try
            {
                if (EnsureWindow())
                {
                    var PID = Process.GetProcessesByName(NAME_PROCESS)[0].Id;
                    return PID;
                }

                return 0;
            }
            catch
            {
                Console.WriteLine($"[ERROR] Could not obtain PID of {NAME_PROCESS}");
                return 0;
            }
        }

        public bool EnsureWindow()
        {
            IntPtr WindowHwnd = User.FindWindow(false, NAME_WINDOW);

            if (WindowHwnd == IntPtr.Zero)
            {
                return false;
            }
            if (WindowHwnd == User.GetForegroundWindow())
            {
                if (NAME_WINDOW.Length == User.GetWindowTextA(WindowHwnd, NAME_WINDOW, 1000))
                {
                    return true;
                }
            }

            return false;
        }

        public bool GetProcessHandle(int PID, ref IntPtr handle)
        {
            try
            {
                if (EnsureWindow())
                {
                    handle = Kernel.OpenProcess(PROCESS_VM_READ, IntPtr.Zero, (IntPtr)PID);
                    return true;
                }

                return false;
            }
            catch
            {
                Console.WriteLine($"[ERROR] Could not Obtain handle for PID: {PID}");
                return false;
            }
        }

        public IntPtr GetModuleBaseAddress(string processName, string moduleName)
        {
            if (EnsureWindow())
            {
                try
                {
                    Process[] processes = Process.GetProcessesByName(processName);

                    if (processes.Length > 0)
                    {
                        foreach (ProcessModule modules in processes[0].Modules)
                        {
                            if (modules.ModuleName == moduleName)
                            {
                                IntPtr baseClient = modules.BaseAddress;
                                return baseClient;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"[ERROR] Error obtaining process module {moduleName}");
                    return IntPtr.Zero;
                }
            }

            return IntPtr.Zero;
        }
    }
}
using System;
using System.Threading;
using Tacka_CSGO_Radar_Cheat.Classes;
using Tacka_CSGO_Radar_Cheat.Classes.Utils;
using Tacka_CSGO_Radar_Cheat.Server;

namespace Tacka_CSGO_Radar_Cheat
{
    class Program
    {
        public static string version = "v0.4.1";
        public static bool devMode = true;
        public static int HTTPSERVER_PORT = 8080;
        
        static void Main()
        {
            Console.Title = $"Tacka - CSGO Radar Cheat | {version}";

            WelcomeMessage();

            //First run the local HTTP server
            HTTPServer httpServer = new HTTPServer();
            httpServer.Port = HTTPSERVER_PORT;
            Thread _serverThread = new Thread(httpServer.RunLocalHTTPServer);
            _serverThread.Start();
            Thread.Sleep(2000);

            //Attempt to attach to CSGO
            GameProcess gameProcess = new GameProcess();
            bool attachedCheck = false;

            do
            {
                Console.WriteLine("\n\r[PROCESS] Attempting to attach to CSGO process... (Please open CSGO and switch to its window)");
                if (gameProcess.EnsureWindow())
                {
                    attachedCheck = !attachedCheck;
                }

                Thread.Sleep(2000);

            } while (!attachedCheck);

            //Get CSGO's PID
            int PID = gameProcess.GetProccessID();

            //OpenProcess to get CSGO handle
            IntPtr handle = IntPtr.Zero;
            bool processOpened = gameProcess.GetProcessHandle(PID, ref handle);

            //Get the base address client.dll and engine.dll from CSGO
            IntPtr clientBaseAddress = gameProcess.GetModuleBaseAddress(gameProcess.ProcessName, gameProcess.ModuleClientName);
            IntPtr engineBaseAddress = gameProcess.GetModuleBaseAddress(gameProcess.ProcessName, gameProcess.ModuleEngineName);

            //Update the EntityAddressList - This is to get the basic location of all entities
            EntityAddressList EntityAddressList = new EntityAddressList();
            EntityList entityList = new EntityList();
            MemoryReader memoryReader = new MemoryReader();

            //Loop to update console values
            for (; ; )
            {
                //Find the map name and update the property
                MapFinder.MapName = MapFinder.GetMapName(MapFinder.GetMapID(memoryReader, handle, engineBaseAddress));

                //First, Update the entityAddress list
                EntityAddressList.UpdateAddressList(memoryReader, handle, clientBaseAddress);
                //Now we know where all the enitity locations are in memory, create an entity obj for each and add it to the ENTITY_LIST list
                entityList.UpdateEntityList(memoryReader, handle, EntityAddressList.ENTITY_ADDRESS_LIST);

                //Print
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine($"PID: {PID}\n\r" +
                    $"ProcessOpened: {processOpened}\n\r" +
                    $"HandlePtr: {handle}\n\r" +
                    $"ClientBaseAddress: {clientBaseAddress}\n\r" +
                    $"EngineBaseAddress: {engineBaseAddress}\n\r" +
                    $"MapID: {MapFinder.MapName}\n\r" +
                    $"NumberOfEntityObjects: {EntityList.ENTITY_LIST.Count}\n\r");

                foreach (Entity entity in EntityList.ENTITY_LIST)
                {
                    Console.WriteLine($"{entity.entityAddess}, {entity.entityTeam}, {entity.entityHealth}, {entity.entityXPos}, {entity.entityYPos}, {entity.entityZPos}, {entity.isDormant}\n\r");
                }
            }
        }

        public static void WelcomeMessage()
        {
            // A welcome message:
            Console.WriteLine($"You are using: {Console.Title}! \n\r\n\r" +
                $"This is a program that reads player data within the CSGO process. With this data, it is streamed to a locally run\n\r" +
                $"HTTP server which you can access through your browser by going to 'http://localhost:{HTTPSERVER_PORT}/. From there you can see\n\r" +
                $"the radar in better detail in real-time.\n\r\n\r" +
                $"The current version you are running is: {version}\n\r\n\r" +
                $"Press ENTER to continue... ");
            Console.Read();
        }
    }
}
using System;
using System.Collections.Generic;

namespace Tacka_CSGO_Radar_Cheat.Classes.Utils
{
    class EntityAddressList
    {
        // Comp => 10
        // Casual => 20
        private static readonly int MAX_ENTITIES_IN_VAC_GAME = 20;

        public static List<int> ENTITY_ADDRESS_LIST = new List<int>();

        public bool UpdateAddressList(MemoryReader memoryReader, IntPtr handle, IntPtr clientBaseAddress)
        {
            ENTITY_ADDRESS_LIST.Clear();

            for (int i = 0; i <= MAX_ENTITIES_IN_VAC_GAME; i++)
            {
                int entityAddress = 0;
                bool read = memoryReader.ReadInt32(handle, (int)clientBaseAddress + Signatures.dwEntityList + (i * 0x10), ref entityAddress);

                if (entityAddress != 0)
                {
                    ENTITY_ADDRESS_LIST.Add(entityAddress);
                }
            }
            
            return true;
        }
    }
}
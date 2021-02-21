using System;
using Tacka_CSGO_Radar_Cheat.Classes.Utils;

namespace Tacka_CSGO_Radar_Cheat.Classes
{
    class MapFinder
    {
        public static string MapName { get; set; }

        public static string GetMapName(int mapID)
        {
            switch (mapID)
            {
                case (int)Maps.DE_MIRAGE:
                    return "de_mirage";

                case (int)Maps.DE_DUST2:
                    return "de_dust2";

                case (int)Maps.DE_INFERNO:
                    return "de_inferno";

                case (int)Maps.DE_NUKE:
                    return "de_nuke";

                case (int)Maps.DE_NUKE_SHORT:
                    return "de_nuke_short";

                case (int)Maps.DE_CACHE:
                    return "de_cache";

                case (int)Maps.DE_OVERPASS:
                    return "de_overpass";

                case (int)Maps.DE_TRAIN:
                    return "de_cache";

                case (int)Maps.DE_CHLORINE:
                    return "de_chlorine";

                case (int)Maps.DE_ANUBIS:
                    return "de_anubis";

                case (int)Maps.DE_VERTIGO:
                    return "de_vertigo";

                case (int)Maps.DE_DUST_SHORT:
                    return "de_dust_short";

                case (int)Maps.DE_RAILTO:
                    return "de_railto";

                case (int)Maps.DE_LAKE:
                    return "de_lake";

                default:
                    return "Unknown";
            }
        }

        public static int GetMapID(MemoryReader memoryReader, IntPtr handle, IntPtr engineBaseAddress)
        {
            int clientStateBuffer = 0;
            int mapIDBuffer = 0;
            memoryReader.ReadInt32(handle, (int)engineBaseAddress + Signatures.dwClientState, ref clientStateBuffer);
            memoryReader.ReadInt32(handle, clientStateBuffer + Signatures.dwClientState_Map, ref mapIDBuffer);

            return mapIDBuffer;
        }
    }
}
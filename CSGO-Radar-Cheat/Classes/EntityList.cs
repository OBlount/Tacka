using System;
using System.Collections.Generic;
using Tacka_CSGO_Radar_Cheat.Classes.Utils;

namespace Tacka_CSGO_Radar_Cheat.Classes
{
    class EntityList
    {
        public static List<Entity> ENTITY_LIST = new List<Entity>();

        public void UpdateEntityList(MemoryReader memoryReader, IntPtr handle, List<int> ENTITY_ADDRESS_LIST)
        {
            ENTITY_LIST.Clear();
            CreateEntityOjects(memoryReader, handle, ENTITY_ADDRESS_LIST);
        }

        private void CreateEntityOjects(MemoryReader memoryReader, IntPtr handle, List<int> ENTITY_ADDRESS_LIST)
        {
            foreach (int entityAddress in ENTITY_ADDRESS_LIST)
            {
                int entityHealth = 0;
                int entityXPosInt = 0;
                int entityYPosInt = 0;
                int entityZPosInt = 0;
                int entityTeam = 0;
                int isDormant = 0;
                memoryReader.ReadInt32(handle, entityAddress + Offsets.m_iHealth, ref entityHealth);
                memoryReader.ReadInt32(handle, entityAddress + 0x138, ref entityXPosInt);
                memoryReader.ReadInt32(handle, entityAddress + 0x13C, ref entityYPosInt);
                memoryReader.ReadInt32(handle, entityAddress + 0x140, ref entityZPosInt);
                memoryReader.ReadInt32(handle, entityAddress + Offsets.m_iTeamNum, ref entityTeam);
                memoryReader.ReadInt32(handle, entityAddress + Signatures.m_bDormant, ref isDormant);

                float entityXPos = ConvertIntPositions(entityXPosInt);
                float entityYPos = ConvertIntPositions(entityYPosInt);
                float entityZPos = ConvertIntPositions(entityZPosInt);

                Entity entity = new Entity
                {
                    entityAddess = entityAddress,
                    entityHealth = entityHealth,
                    entityXPos = entityXPos,
                    entityYPos = entityYPos,
                    entityZPos = entityZPos,
                    entityTeam = entityTeam,
                    isDormant = isDormant
                };

                ENTITY_LIST.Add(entity);
            }
        }

        private float ConvertIntPositions(int positionInt)
        {
            byte[] bytesOfTheNumber = BitConverter.GetBytes(positionInt);
            float floatConversion = BitConverter.ToSingle(bytesOfTheNumber, 0);

            return floatConversion;
        }
    }
}
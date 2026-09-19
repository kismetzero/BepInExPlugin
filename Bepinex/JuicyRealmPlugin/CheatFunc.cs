
using SpaceCan;
using UnityEngine;

namespace JuicyRealmPlugin
{
    public class CheatFunc
    {
        public static void SpawnBossChest(ChestType chestType = ChestType.Nice, int offsetLevelMin = 0, int offsetLevelMax = 0, int coreCount = 0, int addID = 0)
        {
            LootManager lm = GameManager.GetManager<LootManager>();
            GameObject obj = lm.GenerateChest(chestType, offsetLevelMin, offsetLevelMax, coreCount, addID);
            lm.ShowChest(obj, null);
        }
    }
}

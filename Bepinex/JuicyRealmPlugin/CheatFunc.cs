using Mirror;
using SpaceCan;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JuicyRealmPlugin
{
    public class CheatFunc
    {
        public static int A_PlayerNum { get; set; } = 0;
        public static DataManager C_DataManager => GameManager.GetManager<DataManager>();
        public static LootManager C_LootManager => GameManager.GetManager<LootManager>();
        public static LogicManager C_LogicManager => GameManager.GetManager<LogicManager>();
        public static PlayerObject C_PlayerObj
        {
            get
            {
                LogicManager logic = C_LogicManager;
                if (logic == null) { return null; }
                List<SCPlayer> players = logic.GetPlayers();
                if (players == null) { return null; }
                if (players.Count < 1) { return null; }
                SCPlayer player = players[A_PlayerNum];
                if (player == null) { return null; }
                return player.creature;
            }
        }
        public static void SpawnBossChest(ChestType chestType = ChestType.Nice, int offsetLevelMin = 0, int offsetLevelMax = 0, int coreCount = 0, int addID = 0)
        {
            LootManager lm = GameManager.GetManager<LootManager>();
            GameObject obj = lm.GenerateChest(chestType, offsetLevelMin, offsetLevelMax, coreCount, addID);
            lm.ShowChest(obj, null);
        }
        public static GameObject A_GenerateWeapon(int weaponId, int starLevel = 0)
        {
            LootManager lootManager = GameManager.GetManager<LootManager>();
            DataManager dataManager = GameManager.GetManager<DataManager>();

            int trueWeaponId = dataManager.config.Weapon[weaponId].ID;
            GameObject weaponResObj = dataManager.GetPrefabByID(trueWeaponId);
            if (weaponResObj == null) { return null; }
            GameObject weaponObj = UnityEngine.Object.Instantiate<GameObject>(weaponResObj);
            NetworkServer.Spawn(weaponObj);
            weaponObj.GetComponent<Weapon>().InitalStarRate = starLevel;
            lootManager.AddLootObject(weaponObj);
            weaponObj.GetComponent<LootObject>().enabled = true;
            return weaponObj;
        }
        public static void SpawnDiyChest(int weaponId, int starLevel = 0, ChestType chestType = ChestType.Nice, int coinCount = 0, int coreCount = 0)
        {
            LootManager lootManager = GameManager.GetManager<LootManager>();
            string chestResPath = "Prefab/LootChest_Normal";
            switch (chestType)
            {
                case ChestType.Normal:
                case ChestType.Guide:
                    chestResPath = "Prefab/LootChest_Normal";
                    break;
                case ChestType.Nice:
                    chestResPath = "Prefab/LootChest_Nice";
                    break;
                case ChestType.Pro:
                case ChestType.EndPro:
                    chestResPath = "Prefab/LootChest_Pro";
                    break;
                case ChestType.Trap:
                    chestResPath = "Prefab/LootChest_Nice";
                    break;
                case ChestType.Hurt:
                    chestResPath = "Prefab/LootChest_Hurt";
                    break;
            }
            GameObject chestObj = PoolManager.SharedInstance.TakeOutEntity(Resources.Load<GameObject>(chestResPath), 1, false);
            NetworkServer.Spawn(chestObj);
            TreasureChest chest = chestObj.GetComponent<TreasureChest>();
            List<GameObject> chestList = new List<GameObject>
            {
                A_GenerateWeapon(weaponId, starLevel),
                lootManager.GenerateItem("HealShieldItem"),
                lootManager.GenerateItem("Candy")
            };
            chest.SetLoot(chestList, coinCount, coreCount);
            lootManager.AddLootObject(chestObj);
            ClientManager.Instance.RpcSetActive(chestObj, false);
            lootManager.ShowChest(chestObj, null);
        }
    }
}

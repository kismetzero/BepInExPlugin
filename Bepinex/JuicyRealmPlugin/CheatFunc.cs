using HarmonyLib;
using Mirror;
using SpaceCan;
using System.Collections.Generic;
using UnityEngine;

namespace JuicyRealmPlugin
{
    public class CheatFunc
    {
        public static LogicManager LM => GameManager.GetManager<LogicManager>();

        public static int PlayerNum { get; set; } = 0;
        public static PlayerObject PlayerObj
        {
            get
            {
                LogicManager logic = LM;
                if (logic == null) { return default; }
                List<SCPlayer> players = logic.GetPlayers();
                if (players == null) { return default; }
                if (players.Count < 1) { return default; }
                if (PlayerNum < 0 || PlayerNum >= players.Count) { return default; }
                SCPlayer player = players[PlayerNum];
                if (player == null) { return default; }
                return player.creature;
            }
        }
        public static float Health
        {
            get => (PlayerObj != null) ? PlayerObj.CurrentHp : default;
            set { if (PlayerObj != null) { PlayerObj.CurrentHp = value; } }
        }
        public static float HealthMax
        {
            get => (PlayerObj != null) ? PlayerObj.HpMax : default;
            set { if (PlayerObj != null) { PlayerObj.HpMax = value; } }
        }


        public static float Shield
        {
            get => (PlayerObj != null) ? PlayerObj.CurrentShield : default;
            set { if (PlayerObj != null) { PlayerObj.CurrentShield = value; } }
        }
        public static float ShieldMax => (PlayerObj != null) ? PlayerObj.ShieldMax : default;


        public static float Energy
        {
            get => (PlayerObj != null) ? PlayerObj.currentEnergy : default;
            set { if (PlayerObj != null) { PlayerObj.currentEnergy = value; } }
        }
        public static float EnergyMax
        {
            get => (PlayerObj != null) ? PlayerObj.energyMax : default;
            set { if (PlayerObj != null) { PlayerObj.energyMax = value; } }
        }
        public static bool InfEnergy { get; set; } = false;

        [HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), "UpdateView")]
        public static void PlayerObject_UpdateView_Prefix(PlayerObject __instance)
        {
            if (InfEnergy)
            {
                if (__instance.currentEnergy < __instance.energyMax)
                {
                    __instance.currentEnergy = __instance.energyMax;
                }
            }
        }


        public static int Coin
        {
            get => (PlayerObj != null) ? PlayerObj.Coin : default;
            set { if (PlayerObj != null) { PlayerObj.Coin = value; } }
        }


        public static bool CanNotHurt => (PlayerObj != null) ? PlayerObj.CanNotBeHurt : default;
        public static void A_GodModeOn()
        {
            PlayerObject player = PlayerObj;
            if (player == null) { return; }
            Traverse.Create(player).Field("canNotHurt").GetValue<MultiBool>().SetTrue("debug_no_hurt");
        }
        public static void A_GodModeOff()
        {
            PlayerObject player = PlayerObj;
            if (player == null) { return; }
            Traverse.Create(player).Field("canNotHurt").GetValue<MultiBool>().SetFalse("debug_no_hurt");
        }

        public enum GodModeType { Off, SkipOnDamage, SkipHurt }
        public static GodModeType A_GodMode { get; set; } = GodModeType.Off;

        [HarmonyPrefix, HarmonyPatch(typeof(CreatureObject), nameof(CreatureObject.OnDamage))]
        public static bool CreatureObject_OnDamage_Prefix(CreatureObject __instance)
        {
            if (A_GodMode == GodModeType.SkipOnDamage && __instance is PlayerObject) { return false; }
            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), "Hurt")]
        public static bool PlayerObject_Hurt_Prefix()
        {
            if (A_GodMode == GodModeType.SkipHurt) { return false; }
            return true;
        }

        //[HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), nameof(PlayerObject.CurrentHp), MethodType.Setter)]
        //public static bool PlayerObject_CurrentHp_Setter_Prefix(PlayerObject __instance, ref float value)
        //{
        //    return true;
        //}

        //[HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), nameof(PlayerObject.CurrentShield), MethodType.Setter)]
        //public static bool PlayerObject_CurrentShield_Setter_Prefix(PlayerObject __instance, ref float value)
        //{
        //    return true;
        //}


        public static bool A_InfSkill { get; set; } = false;

        [HarmonyPrefix, HarmonyPatch(typeof(SkillItem), nameof(SkillItem.CanUse))]
        public static bool SkillItem_CanUse_Prefix(ref bool __result)
        {
            if (A_InfSkill)
            {
                __result = true;
                return false;
            }
            return true;
        }


        public static GameObject A_GenerateWeapon(int weaponId, int starLevel = 0)
        {
            LootManager lootManager = GameManager.GetManager<LootManager>();
            DataManager dataManager = GameManager.GetManager<DataManager>();

            starLevel = Mathf.Clamp(starLevel, 0, 3);
            GameObject weaponRes = dataManager.GetPrefabByID(weaponId);
            if (weaponRes == null) { return null; }
            GameObject weaponObj = Object.Instantiate<GameObject>(weaponRes);
            NetworkServer.Spawn(weaponObj);
            weaponObj.GetComponent<Weapon>().InitalStarRate = starLevel;
            lootManager.AddLootObject(weaponObj);
            weaponObj.GetComponent<LootObject>().enabled = true;
            return weaponObj;
        }
        public static void A_SpawnDiyChest(List<GameObject> chestList, ChestType chestType = ChestType.Nice, int coinCount = 0, int coreCount = 0)
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
            chest.SetLoot(chestList, coinCount, coreCount);
            lootManager.AddLootObject(chestObj);
            ClientManager.Instance.RpcSetActive(chestObj, false);
            lootManager.ShowChest(chestObj, null);
        }
    }   
}

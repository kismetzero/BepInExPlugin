using HarmonyLib;
using Mirror;
using SpaceCan;
using System.Collections.Generic;
using UnityEngine;

namespace JuicyRealmPlugin
{
    public class CheatFunc
    {
        public static DataManager C_DataManager => GameManager.GetManager<DataManager>();
        public static LootManager C_LootManager => GameManager.GetManager<LootManager>();
        public static LogicManager C_LogicManager => GameManager.GetManager<LogicManager>();

        public static int A_PlayerNum { get; set; } = 0;
        public static PlayerObject C_PlayerObj
        {
            get
            {
                LogicManager logic = C_LogicManager;
                if (logic == null) { return null; }
                List<SCPlayer> players = logic.GetPlayers();
                if (players == null) { return null; }
                if (players.Count < 1) { return null; }
                if (A_PlayerNum < 0 || A_PlayerNum >= players.Count) { return null; }
                SCPlayer player = players[A_PlayerNum];
                if (player == null) { return null; }
                return player.creature;
            }
        }
        public static float A_CurrentHp
        {
            get => (C_PlayerObj != null) ? C_PlayerObj.CurrentHp : 0f;
            set { if (C_PlayerObj != null) { C_PlayerObj.CurrentHp = value; } }
        }
        public static float A_HpMax
        {
            get => (C_PlayerObj != null) ? C_PlayerObj.HpMax : 0f;
            set { if (C_PlayerObj != null) { C_PlayerObj.HpMax = value; } }
        }


        public static float A_CurrentShield
        {
            get => (C_PlayerObj != null) ? C_PlayerObj.CurrentShield : 0f;
            set { if (C_PlayerObj != null) { C_PlayerObj.CurrentShield = value; } }
        }
        public static float A_ShieldMax => (C_PlayerObj != null) ? C_PlayerObj.ShieldMax : 0f;


        public static float A_CurrentEnergy
        {
            get => (C_PlayerObj != null) ? C_PlayerObj.currentEnergy : 0f;
            set { if (C_PlayerObj != null) { C_PlayerObj.currentEnergy = value; } }
        }
        public static float A_EnergyMax
        {
            get => (C_PlayerObj != null) ? C_PlayerObj.energyMax : 0f;
            set { if (C_PlayerObj != null) { C_PlayerObj.energyMax = value; } }
        }
        public static bool A_InfEnergy { get; set; } = false;

        [HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), "UpdateView")]
        public static void PlayerObject_UpdateView_Prefix(PlayerObject __instance)
        {
            if (A_InfEnergy)
            {
                if (__instance.currentEnergy < __instance.energyMax)
                {
                    __instance.currentEnergy = __instance.energyMax;
                }
            }
        }


        public static int A_Coin
        {
            get => (C_PlayerObj != null) ? C_PlayerObj.Coin : 0;
            set { if (C_PlayerObj != null) { C_PlayerObj.Coin = value; } }
        }


        public static bool A_CanNotHurt => (C_PlayerObj != null) ? C_PlayerObj.CanNotBeHurt : false;
        public static void A_GodModeOn()
        {
            PlayerObject player = C_PlayerObj;
            if (player == null) { return; }
            Traverse.Create(player).Field("canNotHurt").GetValue<MultiBool>().SetTrue("debug_no_hurt");
        }
        public static void A_GodModeOff()
        {
            PlayerObject player = C_PlayerObj;
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

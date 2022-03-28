using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace JuicyRealmPlugin.PatchPack
{
    class PlayerObjectPatch
    {
        public static PlayerObject POinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), "Awake")]
        public static void getPOinstance(PlayerObject __instance)
        {
            POinstance = __instance;
            Debug.Log("PlayerObject的实例获取方法已调用");
        }

        //使用金币
        [HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), "UseCoin")]
        public static void UseCoinPrefix(PlayerObject __instance)
        {
            if (FlagControl.infiniteGold.value)
            {
                if (__instance.Coin < 233) { __instance.Coin = 350; }
            }
        }

        //掉落金币
        [HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), "LootCoin")]
        public static void LootCoinPrefix(PlayerObject __instance)
        {
            if (FlagControl.infiniteGold.value)
            {
                if (__instance.Coin < 233) { __instance.Coin = 350; }
            }
        }

        //受伤
        [HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), "Hurt")]
        public static bool HurtPrefix(PlayerObject __instance)
        {
            if (FlagControl.GodMod.value) { return false; }
            else { return true; }

        }

        //使用生命
        [HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), "UseHp")]
        public static void UseHpPrefix(PlayerObject __instance)
        {
            if (FlagControl.GodMod.value)
            {
                if (__instance.CurrentHp < 3) { __instance.CurrentHp = 5; }
            }
        }
    }
}

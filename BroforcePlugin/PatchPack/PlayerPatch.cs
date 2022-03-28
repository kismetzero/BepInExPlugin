using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace BroforcePlugin.PatchPack
{
    class PlayerPatch
    {
        public static Player Pinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(Player), "Awake")]
        public static void getPinstance(Player __instance)
        {
            Pinstance = __instance;
            Debug.Log("Player的实例获取方法已调用");
        }

        //移除生命
        [HarmonyPrefix, HarmonyPatch(typeof(Player), "RemoveLife")]
        public static bool RemoveLifePrefix(Player __instance)
        {
            if (Pinstance != __instance)
            {
                Pinstance = __instance;
            }

            if (FlagControl.LockLive.value) { return false; }
            else { return true; }
        }
    }
}

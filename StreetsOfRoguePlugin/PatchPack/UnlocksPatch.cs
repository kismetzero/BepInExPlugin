using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace StreetsOfRoguePlugin.PatchPack
{
    class UnlocksPatch
    {
        public static Unlocks Uinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(Unlocks), "Awake")]
        public static void getUinstance(Unlocks __instance)
        {
            Uinstance = __instance;
            Debug.Log("Unlocks的实例获取方法已调用");
        }
    }
}

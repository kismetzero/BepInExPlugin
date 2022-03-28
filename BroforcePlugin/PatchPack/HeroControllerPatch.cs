using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace BroforcePlugin.PatchPack
{
    class HeroControllerPatch
    {
        public static HeroController HCinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(HeroController), "Awake")]
        public static void getHCinstance(HeroController __instance)
        {
            HCinstance = __instance;
            Debug.Log("HeroController的实例获取方法已调用");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(HeroController), "Start")]
        public static void getBBinstanceRPC(HeroController __instance)
        {
            if (HCinstance != __instance)
            {
                HCinstance = __instance;
                Debug.Log("HeroController的实例update");
            }
        }
    }
}

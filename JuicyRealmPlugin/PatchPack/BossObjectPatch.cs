using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace JuicyRealmPlugin.PatchPack
{
    class BossObjectPatch
    {
        public static BossObject BOinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(BossObject), "Awake")]
        public static void getBOinstance(BossObject __instance)
        {
            BOinstance = __instance;
            Debug.Log("BossObject的实例获取方法已调用");
        }
    }
}

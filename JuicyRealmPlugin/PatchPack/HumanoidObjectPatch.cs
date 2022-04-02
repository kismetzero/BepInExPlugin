using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace JuicyRealmPlugin.PatchPack
{
    class HumanoidObjectPatch
    {
        public static HumanoidObject HOinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(HumanoidObject), "Awake")]
        public static void getHOinstance(HumanoidObject __instance)
        {
            HOinstance = __instance;
            Debug.Log("HumanoidObject的实例获取方法已调用");
        }
    }
}

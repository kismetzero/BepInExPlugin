using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace BroforcePlugin.PatchPack
{
    class BroBasePatch
    {
        public static BroBase BBinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(BroBase), "Awake")]
        public static void getBBinstance(BroBase __instance)
        {
            BBinstance = __instance;
            Debug.Log("BroBase的实例获取方法已调用");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(BroBase), "Start")]
        public static void getBBinstanceRPC(BroBase __instance)
        {
            if (BBinstance != __instance)
            {
                BBinstance = __instance;
                Debug.Log("BroBase的实例update");
            }
        }

        //特殊弹药
        [HarmonyPrefix, HarmonyPatch(typeof(BroBase), "SpecialAmmo", MethodType.Setter)]
        public static void SpecialAmmoPrefix(ref int value)
        {
            //Debug.Log("value的值为：" + value);
            if (FlagControl.infiniteAmmo.value)
            {
                if (value < 2) { value = 5; }
            }

            //return true;
        }
    }
}

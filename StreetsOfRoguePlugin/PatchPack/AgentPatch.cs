using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace StreetsOfRoguePlugin.PatchPack
{
    class AgentPatch
    {
        public static Agent Ainstance;

        [HarmonyPrefix, HarmonyPatch(typeof(Agent), "Awake")]
        public static void getUinstance(Agent __instance)
        {
            Ainstance = __instance;
            Debug.Log("Agent的实例获取方法已调用");
        }

        /*[HarmonyPrefix, HarmonyPatch(typeof(Agent), "currentHealth", MethodType.Setter)]
        public static void currentHealthPrefix(ref int value)
        {
            Debug.Log("currentHealth" + value);
        }*/
    }
}

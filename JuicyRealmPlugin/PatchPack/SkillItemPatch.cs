using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace JuicyRealmPlugin.PatchPack
{
    class SkillItemPatch
    {
        public static SkillItem SIinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(SkillItem), "Awake")]
        public static void getSIinstance(SkillItem __instance)
        {
            SIinstance = __instance;
            Debug.Log("SkillItem的实例获取方法已调用");
        }

        //技能能否用
        [HarmonyPrefix, HarmonyPatch(typeof(SkillItem), "CanUse")]
        public static void CanUsePrefix(SkillItem __instance)
        {
            if (FlagControl.infiniteSkill.value)
            {
                __instance.timer = __instance.cd;
            }
        }
    }
}

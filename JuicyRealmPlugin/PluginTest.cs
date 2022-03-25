using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace JuicyRealmPlugin
{
    [BepInPlugin("com.kisme.BepInEx.JuicyRealm.PluginTest", "MyFirstJuicyRealmBepInExMod", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        public static PluginTest PTinstance;
        private bool UpdateFirstOn;

        public bool GodModOn;
        public bool infiniteGoldOn;
        public bool infiniteSkillOn;

        void Awake()
        {
            Logger.LogInfo("Hello World！！！");
            Logger.LogInfo("插件的Awake()方法被调用了");
            PTinstance = this;
            this.UpdateFirstOn = true;

            this.GodModOn = false;
            this.infiniteGoldOn = false;
            this.infiniteSkillOn = false;
        }

        void Start()
        {
            Logger.LogInfo("插件的Start()方法被调用了");
            Harmony.CreateAndPatchAll(typeof(PlayerObjectPatch));
            Harmony.CreateAndPatchAll(typeof(SkillItemPatch));
            Logger.LogInfo("Harmony补丁已开启");
        }

        void Update()
        {
            if (this.UpdateFirstOn)
            {
                Logger.LogInfo("插件的Update()方法被调用了");
                this.UpdateFirstOn = false;
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                //F5：上帝模式
                this.GodModOn = !this.GodModOn;
                if (this.GodModOn) { Logger.LogInfo("上帝模式已开启"); }
                else { Logger.LogInfo("上帝模式已关闭"); }
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                //F6：无限金币
                this.infiniteGoldOn = !this.infiniteGoldOn;
                if (this.infiniteGoldOn) { Logger.LogInfo("无限金钱已开启"); }
                else { Logger.LogInfo("无限金钱已关闭"); }
            }

            if (Input.GetKeyDown(KeyCode.F7))
            {
                //F7：无限技能
                this.infiniteSkillOn = !this.infiniteSkillOn;
                if (this.infiniteSkillOn) { Logger.LogInfo("无限技能已开启"); }
                else { Logger.LogInfo("无限技能已关闭"); }
            }

            if (Input.GetKeyDown(KeyCode.F8))
            {
                //F8：增加500金币
                Logger.LogInfo("增加500金币");
                PlayerObjectPatch.POinstance.Coin += 500;
            }
        }


        class PlayerObjectPatch
        {
            public static PlayerObject POinstance;

            [HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), "Awake")]
            public static void getPOinstance(PlayerObject __instance)
            {
                POinstance = __instance;
                Debug.Log("PlayerObject的实例获取方法已调用");
            }

            [HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), "UseCoin")]
            public static void UseCoinPrefix(PlayerObject __instance)
            {
                if (PluginTest.PTinstance.infiniteGoldOn)
                {
                    if (__instance.Coin < 233) { __instance.Coin = 350; }
                }
            }

            [HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), "LootCoin")]
            public static void LootCoinPrefix(PlayerObject __instance)
            {
                if (PluginTest.PTinstance.infiniteGoldOn)
                {
                    if (__instance.Coin < 233) { __instance.Coin = 350; }
                }
            }

            [HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), "Hurt")]
            public static bool HurtPrefix(PlayerObject __instance)
            {
                if (PluginTest.PTinstance.GodModOn) { return false; }
                else { return true; }

            }

            [HarmonyPrefix, HarmonyPatch(typeof(PlayerObject), "UseHp")]
            public static void UseHpPrefix(PlayerObject __instance)
            {
                if (PluginTest.PTinstance.GodModOn)
                {
                    if (__instance.CurrentHp < 3) { __instance.CurrentHp = 5; }
                }
            }
        }

        class SkillItemPatch
        {
            public static SkillItem SIinstance;

            [HarmonyPrefix, HarmonyPatch(typeof(SkillItem), "Awake")]
            public static void getSIinstance(SkillItem __instance)
            {
                SIinstance = __instance;
                Debug.Log("SkillItem的实例获取方法已调用");
            }

            [HarmonyPrefix, HarmonyPatch(typeof(SkillItem), "CanUse")]
            public static void CanUsePrefix(SkillItem __instance)
            {
                __instance.timer = __instance.cd;
            }
        }
    }
}

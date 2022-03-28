using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;
using JuicyRealmPlugin.PatchPack;

namespace JuicyRealmPlugin
{
    [BepInPlugin("com.kisme.BepInEx.JuicyRealm.PluginTest", "MyFirstJuicyRealmBepInExMod", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        public static PluginTest PTinstance;
        private bool UpdateFirstOn;

        void Awake()
        {
            Logger.LogInfo("Hello World！！！");
            Logger.LogInfo("插件的Awake()方法被调用了");
            PTinstance = this;
            this.UpdateFirstOn = true;
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
                FlagControl.GodMod.SwitchFlag();
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                //F6：无限金币
                FlagControl.infiniteGold.SwitchFlag();
            }

            if (Input.GetKeyDown(KeyCode.F7))
            {
                //F7：无限技能
                FlagControl.infiniteSkill.SwitchFlag();
            }

            if (Input.GetKeyDown(KeyCode.F8))
            {
                //F8：增加500金币
                Logger.LogInfo("增加500金币");
                PlayerObjectPatch.POinstance.Coin += 500;
            }
        }
    }
}

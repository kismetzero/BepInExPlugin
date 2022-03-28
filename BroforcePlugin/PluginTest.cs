using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;
using BroforcePlugin.PatchPack;

namespace BroforcePlugin
{
    [BepInPlugin("com.kisme.BepInEx.Broforce.PluginTest", "MyFirstBroforceBepInExMod", "1.0")]
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
            Harmony.CreateAndPatchAll(typeof(PlayerPatch));
            Harmony.CreateAndPatchAll(typeof(BroBasePatch));
            Logger.LogInfo("Harmony补丁已开启");
        }

        void Update()
        {
            if (this.UpdateFirstOn)
            {
                Logger.LogInfo("插件的Update()方法被调用了");
                this.UpdateFirstOn = false;
            }

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                //num1：锁定生命
                FlagControl.LockLive.SwitchFlag();
            }

            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                //num4：增加生命
                Logger.LogInfo("增加生命");
                PlayerPatch.Pinstance.AddLife();
            }

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                //num2：无限弹药
                FlagControl.infiniteAmmo.SwitchFlag();
            }

            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                //num5：增加弹药
                Logger.LogInfo("增加弹药");
                BroBasePatch.BBinstance.SpecialAmmo += 1;
            }
        }
    }
}

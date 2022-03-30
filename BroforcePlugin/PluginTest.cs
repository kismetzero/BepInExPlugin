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
        public int HeroINT;

        void Awake()
        {
            Logger.LogInfo("Hello World！！！");
            Logger.LogInfo("插件的Awake()方法被调用了");
            PTinstance = this;
            this.UpdateFirstOn = true;
            this.HeroINT = 0;
        }

        void Start()
        {
            Logger.LogInfo("插件的Start()方法被调用了");
            Harmony.CreateAndPatchAll(typeof(PlayerPatch));
            Harmony.CreateAndPatchAll(typeof(BroBasePatch));
            Harmony.CreateAndPatchAll(typeof(HeroControllerPatch));
            Logger.LogInfo("Harmony补丁已开启");
        }

        void Update()
        {
            if (this.UpdateFirstOn)
            {
                Logger.LogInfo("插件的Update()方法被调用了");
                this.UpdateFirstOn = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //1：锁定生命
                FlagControl.LockLive.SwitchFlag();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //2：增加生命
                Logger.LogInfo("增加生命");
                PlayerPatch.Pinstance.AddLife();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //3：无限弹药
                FlagControl.infiniteAmmo.SwitchFlag();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                //4：增加弹药
                Logger.LogInfo("增加弹药");
                BroBasePatch.BBinstance.SpecialAmmo += 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                //5：增加
                Logger.LogInfo("增加");
                this.HeroINT++;
                if (HeroINT > 46) { HeroINT = 0; }
                Logger.LogInfo(HeroINT);
            }
        }

        
    }
}

using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace BroforcePlugin
{
    [BepInPlugin("com.kisme.BepInEx.Broforce.PluginTest", "MyFirstBroforceBepInExMod", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        public static PluginTest PTinstance;
        private bool UpdateFirstOn;

        public bool LockLiveOn;
        public bool LockAmmoOn;

        void Awake()
        {
            Logger.LogInfo("Hello World！！！");
            Logger.LogInfo("插件的Awake()方法被调用了");
            PTinstance = this;
            this.UpdateFirstOn = true;

            this.LockLiveOn = false;
            this.LockAmmoOn = false;
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

            if (Input.GetKeyDown(KeyCode.F5))
            {
                //F5：锁定生命
                this.LockLiveOn = !this.LockLiveOn;
                if (this.LockLiveOn) { Logger.LogInfo("锁定生命已开启"); }
                else { Logger.LogInfo("锁定生命已关闭"); }
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                //F6：增加生命
                Logger.LogInfo("增加生命");
                PlayerPatch.Pinstance.AddLife();
            }

            if (Input.GetKeyDown(KeyCode.F7))
            {
                //F7：锁定弹药
                this.LockAmmoOn = !this.LockAmmoOn;
                if (this.LockAmmoOn) { Logger.LogInfo("锁定弹药已开启"); }
                else { Logger.LogInfo("锁定弹药已关闭"); }
            }

            if (Input.GetKeyDown(KeyCode.F8))
            {
                //F8：增加弹药
                Logger.LogInfo("增加弹药");
                BroBasePatch.BBinstance.SpecialAmmo += 1;
            }
        }
    }

    class PlayerPatch
    {
        public static Player Pinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(Player), "Awake")]
        public static void getPinstance(Player __instance)
        {
            Pinstance = __instance;
            Debug.Log("Player的实例获取方法已调用");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Player), "RemoveLife")]
        public static bool RemoveLifePrefix(Player __instance)
        {
            if (PluginTest.PTinstance.LockLiveOn) { return false; }
            else { return true; }
        }
    }

    class BroBasePatch
    {
        public static BroBase BBinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(BroBase), "Awake")]
        public static void getPinstance(BroBase __instance)
        {
            BBinstance = __instance;
            Debug.Log("BroBase的实例获取方法已调用");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(BroBase), "SpecialAmmo", MethodType.Setter)]
        public static bool SpecialAmmoPrefix(BroBase __instance)
        {
            if (PluginTest.PTinstance.LockAmmoOn) { return false; }
            else { return true; }
        }
    }
}

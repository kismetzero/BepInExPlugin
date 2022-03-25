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

        public bool infiniteLiveOn;
        public bool GodModOn;
        public bool LockAmmoOn;

        void Awake()
        {
            Logger.LogInfo("Hello World！！！");
            Logger.LogInfo("插件的Awake()方法被调用了");
            PTinstance = this;
            this.UpdateFirstOn = true;

            this.infiniteLiveOn = false;
            this.GodModOn = false;
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
                //F5：无限生命
                this.infiniteLiveOn = !this.infiniteLiveOn;
                if (this.infiniteLiveOn) { Logger.LogInfo("无限生命已开启"); }
                else { Logger.LogInfo("无限生命已关闭"); }
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                //F6：上帝模式
                this.GodModOn = !this.GodModOn;
                if (this.GodModOn) { Logger.LogInfo("上帝模式已开启"); }
                else { Logger.LogInfo("上帝模式已关闭"); }
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
            if (PluginTest.PTinstance.infiniteLiveOn)
            {
                __instance.AddLife();
                if (__instance.Lives < 3) { __instance.AddLife(); }
            }

            if (PluginTest.PTinstance.GodModOn) { return false; }
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

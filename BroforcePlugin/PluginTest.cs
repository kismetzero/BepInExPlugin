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

        public bool GodModOn;
        public bool infiniteLiveOn;
        public bool infiniteAmmoOn;

        void Awake()
        {
            Logger.LogInfo("Hello World！！！");
            Logger.LogInfo("插件的Awake()方法被调用了");

            PTinstance = this;
            this.UpdateFirstOn = true;

            this.GodModOn = false;
            this.infiniteLiveOn = false;
            this.infiniteAmmoOn = false;
        }

        void Start()
        {
            Logger.LogInfo("插件的Start()方法被调用了");
            Harmony.CreateAndPatchAll(typeof(PlayerPatch));
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

            if (Input.GetKeyDown(KeyCode.F5))
            {
                //F5：无限生命
                this.infiniteLiveOn = !this.infiniteLiveOn;
                if (this.infiniteLiveOn)
                {
                    Logger.LogInfo("无限生命已开启");
                }
                else
                {
                    Logger.LogInfo("无限生命已关闭");
                }
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                //F6：上帝模式
                this.GodModOn = !this.GodModOn;
                if (this.GodModOn)
                {
                    Logger.LogInfo("上帝模式已开启");
                }
                else
                {
                    Logger.LogInfo("上帝模式已关闭");
                }
            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                //F7：无限弹药
                this.infiniteAmmoOn = !this.infiniteAmmoOn;
                if (this.infiniteAmmoOn)
                {
                    Logger.LogInfo("无限弹药已开启");
                }
                else
                {
                    Logger.LogInfo("无限弹药已关闭");
                }
            }
         /*   if (Input.GetKeyDown(KeyCode.F8))
            {
                //F8：增加500金币
                Logger.LogInfo("增加500金币");
                //PlayerObjectPatch.POinstance.Coin += 500;
            }*/
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

            if (PluginTest.PTinstance.GodModOn)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    class HeroControllerPatch
    {
        public static HeroController HCinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(HeroController), "Awake")]
        public static void getPinstance(HeroController __instance)
        {
            HCinstance = __instance;
            Debug.Log("HeroController的实例获取方法已调用");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(HeroController), "SetSpecialAmmo")]
        public static bool SetSpecialAmmoPrefix(HeroController __instance)
        {
            if (PluginTest.PTinstance.infiniteAmmoOn)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

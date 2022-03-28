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

    class PlayerPatch
    {
        public static Player Pinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(Player), "Awake")]
        public static void getPinstance(Player __instance)
        {
            Pinstance = __instance;
            Debug.Log("Player的实例获取方法已调用");
        }

        //移除生命
        [HarmonyPrefix, HarmonyPatch(typeof(Player), "RemoveLife")]
        public static bool RemoveLifePrefix(Player __instance)
        {
            if (FlagControl.LockLive.value) { return false; }
            else { return true; }
        }
    }

    class BroBasePatch
    {
        public static BroBase BBinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(BroBase), "Awake")]
        public static void getBBinstance(BroBase __instance)
        {
            BBinstance = __instance;
            Debug.Log("BroBase的实例获取方法已调用");
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

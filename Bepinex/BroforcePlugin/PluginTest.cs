using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace BroforcePlugin
{
    [BepInPlugin("com.kisme.BepInEx.Broforce.PluginTest", "MyFirstBroforceBepInExMod", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        public static PluginTest _instance;
        public bool UpdateFirstOn;

        public static HeroController i_hc_awake;
        public static HeroController i_hc_start;

        void Awake()
        {
            Logger.LogInfo("Hello World！！！");
            Logger.LogInfo("BroforcePluginTest: Awake()");
            //初始化参数
            _instance = this;
            Harmony.CreateAndPatchAll(typeof(PluginTest));
        }

        void Start()
        {
            Logger.LogInfo("BroforcePluginTest: Start()");
        }

        void Update()
        {
            if (this.UpdateFirstOn)
            {
                Logger.LogInfo("BroforcePluginTest: First Update()");
                this.UpdateFirstOn = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //1：增加生命
                HeroController.AddLife(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //2：切换角色
                HeroController.ChangeBro(0, HeroType.IndianaBrones);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //3：增加特殊弹药
                var broBase = HeroController.players[0].character as BroBase;
                broBase.SpecialAmmo += 1;
            }
        }

        void OnGUI()
        {
            if (WindowsDisplayOn)
            {
                Cursor.visible = true;
                // 定义窗口位置 x y 宽 高
                Rect windowRect = new Rect(100, 100, 200, 200);
                /* 创建一个新窗口
                       注意：第一个参数(114514)为窗口ID，ID尽量设置的与众不同，
                       若与其他Mod的窗口ID相同，将会导致窗口冲突  */
                windowRect = GUI.Window(114514, windowRect, DoMyWindow, "修改器窗口");
            }
        }

        //[HarmonyPrefix, HarmonyPatch(typeof(HeroController), "Awake")]
        //public static void getHcAwake(HeroController __instance)
        //{
        //    i_hc_awake = __instance;
        //    Debug.Log("BroforcePluginTest: getHcAwake()");
        //}

        //[HarmonyPrefix, HarmonyPatch(typeof(HeroController), "Start")]
        //public static void getHcStart(HeroController __instance)
        //{
        //    i_hc_start = __instance;
        //    Debug.Log("BroforcePluginTest: getHcStart()");
        //}
    }
}

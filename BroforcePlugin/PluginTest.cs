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
        private bool UpdateFirstOn;             //首次启动Update标记
        public int HeroINT;                     //英雄序号
        private String TmpHeroIntStr;           //临时英雄序号字符串
        private bool WindowsDisplayOn;          //窗口显示标记
        private int TmpHeroINT;                 //临时英雄序号

        void Awake()
        {
            Logger.LogInfo("Hello World！！！");
            Logger.LogInfo("插件的Awake()方法被调用了");
            //初始化参数
            PTinstance = this;
            this.UpdateFirstOn = true;
            this.HeroINT = 0;
            this.WindowsDisplayOn = false;
            this.TmpHeroIntStr = "0";
            this.TmpHeroINT = HeroINT;
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
                //5：选角色
                Logger.LogInfo("选择");
                WindowsDisplayOn = !WindowsDisplayOn;
                if(WindowsDisplayOn)
                {
                    Logger.LogInfo("选择窗口已打开");
                    //Cursor.visible = true;
                    //ShowMouseController.ShowMouse = true;
                }
                else
                {
                    Logger.LogInfo("选择窗口已关闭");
                    HeroINT = int.Parse(TmpHeroIntStr);
                    //Cursor.visible = false;
                    //ShowMouseController.ShowMouse = true;
                }
                Logger.LogInfo(HeroINT);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                //6：切换回随机英雄
                if (HeroINT == 0)
                {
                    HeroINT = TmpHeroINT;
                }
                else
                {
                    TmpHeroINT = HeroINT;
                    HeroINT = 0;
                }
            }
        }

        void OnGUI()
        {
            if (WindowsDisplayOn)
            {
                Cursor.visible = true;
                // 定义窗口位置 x y 宽 高
                Rect windowRect = new Rect(100, 100, 200, 200);
                // 创建一个新窗口
                // 注意：第一个参数(20210218)为窗口ID，ID尽量设置的与众不同，若与其他Mod的窗口ID相同，将会导致窗口冲突
                windowRect = GUI.Window(114514, windowRect, DoMyWindow, "修改器窗口");
            }
        }

        void DoMyWindow(int winId)
        {
            GUILayout.BeginArea(new Rect(10, 20, 180, 160));
            // 这里的大括号是可选的，我个人为了代码的阅读性,习惯性的进行了添加
            // 建议大家也使用大括号这样包裹起来，让代码看起来不那么的乱
            {
                GUILayout.Label("请输入0-45选择角色");
                TmpHeroIntStr = GUILayout.TextField(TmpHeroIntStr);
                GUILayout.Label(FlagControl.LockLive.Status());
                GUILayout.Label(FlagControl.infiniteAmmo.Status());
            }
            GUILayout.EndArea();
        }

    }
}

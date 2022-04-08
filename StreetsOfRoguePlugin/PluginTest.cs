using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;
using StreetsOfRoguePlugin.PatchPack;

namespace StreetsOfRoguePlugin
{
    [BepInPlugin("com.kisme.BepInEx.StreetsOfRogue.PluginTest", "MyFirstStreetsOfRogueBepInExMod", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        public static PluginTest PTinstance;
        private bool UpdateFirstOn;

        // 定义窗口位置 x y 宽 高
        public Rect windowRect = new Rect(100, 100, 300, 200);

        private String StrTmpHP;

        void Awake()
        {
            Logger.LogInfo("Hello World！！！");
            Logger.LogInfo("插件的Awake()方法被调用了");
            PTinstance = this;
            this.UpdateFirstOn = true;

            this.StrTmpHP = "0";
        }

        void Start()
        {
            Logger.LogInfo("插件的Start()方法被调用了");
            Harmony.CreateAndPatchAll(typeof(UnlocksPatch));
            //Harmony.CreateAndPatchAll(typeof(AgentPatch));
            Logger.LogInfo("Harmony补丁已开启");
        }

        void Update()
        {
            if (this.UpdateFirstOn)
            {
                Logger.LogInfo("插件的Update()方法被调用了");
                this.UpdateFirstOn = false;
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                //Del: 修改器窗口
                FlagControl.WindowsDisplay.SwitchFlag();
            }

            if(FlagControl.LockLive.value)
            {
                SetHealth(100f);
            }

/*            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                //num1: 增加50鸡块
                UnlocksPatch.Uinstance.AddNuggets(50);
                Logger.LogInfo("增加50鸡块");
            }*/

        }

        void OnGUI()
        {
            if (FlagControl.WindowsDisplay.value)
            {
                /*  创建一个新窗口
                        注意：第一个参数(114514)为窗口ID，ID尽量设置的与众不同，
                        若与其他Mod的窗口ID相同，将会导致窗口冲突  */
                windowRect = GUI.Window(114514, windowRect, DoMyWindow, "修改器窗口");
            }
        }

        void DoMyWindow(int winId)  //绘制修改器窗口
        {
            GUILayout.BeginArea(new Rect(10, 20, 280, 160));
            /* 这里的大括号是可选的，我个人为了代码的阅读性,习惯性的进行了添加
               建议大家也使用大括号这样包裹起来，让代码看起来不那么的乱 */
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("HP：" + CurrHealth());
                    GUILayout.Label("Lock: " + FlagControl.LockLive.value);
                    if (GUILayout.Button("锁定HP")) { FlagControl.LockLive.SwitchFlag(); }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("加50HP")) { AddHealth(50f); }
                    StrTmpHP = GUILayout.TextField(StrTmpHP);
                    if (GUILayout.Button("设置HP")) { SetHealth(float.Parse(StrTmpHP)); }
                }
                GUILayout.EndHorizontal();

                if (GUILayout.Button("加50鸡块")) { UnlocksPatch.Uinstance.AddNuggets(50); }

            }
            GUILayout.EndArea();
        }

        void AddHealth(float value)
        {
            GameController.gameController.playerAgent.health += value;
            Logger.LogInfo("增加" + value + "血");
        }

        void SetHealth(float value)
        {
            GameController.gameController.playerAgent.health = value;
            Logger.LogInfo("设置" + value + "血");
        }

        float CurrHealth()
        {
            return GameController.gameController.playerAgent.health;
        }

    }
}
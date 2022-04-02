using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;
using SpaceCan;
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
            //Harmony.CreateAndPatchAll(typeof(HumanoidObjectPatch));
            //Harmony.CreateAndPatchAll(typeof(BossObjectPatch));
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
                //1：上帝模式
                FlagControl.GodMod.SwitchFlag();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //2：无限金币
                FlagControl.infiniteGold.SwitchFlag();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //3：无限技能
                FlagControl.infiniteSkill.SwitchFlag();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                //4：增加500金币
                this.AddCoin();
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                //5：功能窗口
                FlagControl.WindowsDisplay.SwitchFlag();
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                //6：生成Boss箱
                this.SpawnBossChest();
            }
        }

        void OnGUI()
        {
            if (FlagControl.WindowsDisplay.value)
            {
                // 定义窗口位置 x y 宽 高
                Rect windowRect = new Rect(100, 100, 200, 200);
                /*  创建一个新窗口
                        注意：第一个参数(114514)为窗口ID，ID尽量设置的与众不同，
                        若与其他Mod的窗口ID相同，将会导致窗口冲突  */
                windowRect = GUI.Window(114514, windowRect, DoMyWindow, "修改器窗口");
            }
        }

        void DoMyWindow(int winId)  //绘制修改器窗口
        {
            GUILayout.BeginArea(new Rect(10, 20, 180, 160));
            /* 这里的大括号是可选的，我个人为了代码的阅读性,习惯性的进行了添加
               建议大家也使用大括号这样包裹起来，让代码看起来不那么的乱 */
            {
                GUILayout.Label(FlagControl.GodMod.Status());
                GUILayout.Label(FlagControl.infiniteGold.Status());
                GUILayout.Label(FlagControl.infiniteSkill.Status());

                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("上帝模式(1)")) { FlagControl.GodMod.SwitchFlag(); }

                    if (GUILayout.Button("无限金币(2)")) { FlagControl.infiniteGold.SwitchFlag(); }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("无限技能(3)")) { FlagControl.infiniteSkill.SwitchFlag(); }

                    if (GUILayout.Button("+500金币(4)")) { this.AddCoin(); }
                }
                GUILayout.EndHorizontal();

                if (GUILayout.Button("生成Boss箱(6)")) { this.SpawnBossChest(); }

            }
            GUILayout.EndArea();
        }

        void SpawnBossChest()   //生成Boss箱方法
        {
            LootManager manager = GameManager.GetManager<LootManager>();
            GameObject gameObject = manager.GenerateChest(ChestType.Pro, 0, 0, 0);
            manager.ShowChest(gameObject, null);
            Logger.LogInfo("生成Boss箱");
        }

        void AddCoin()      //增加500金币方法
        {
            PlayerObjectPatch.POinstance.Coin += 500;
            Logger.LogInfo("增加500金币");
        }
    }
}

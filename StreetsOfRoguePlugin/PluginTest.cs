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
            Harmony.CreateAndPatchAll(typeof(UnlocksPatch));
            Harmony.CreateAndPatchAll(typeof(AgentPatch));
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
                //num1: 增加50鸡块
                UnlocksPatch.Uinstance.AddNuggets(50);
                Logger.LogInfo(AgentPatch.Ainstance.playfieldObjectAgent.health);
            }
        }
    }
}
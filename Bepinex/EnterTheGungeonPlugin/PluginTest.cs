using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace EnterTheGungeonPlugin
{
    [BepInPlugin("com.kisme.BepInEx.EnterTheGungeon.PluginTest", "EnterTheGungeonPluginTest", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        public static PluginTest _instance;
        public bool UpdateFirstOn = true;
        void Awake()
        {
            Logger.LogInfo("Awake()");
            _instance = this;
        }
        void OnDestroy()
        {
            Logger.LogInfo("OnDestroy()");
        }
        void OnEnable()
        {
            Logger.LogInfo("OnEnable()");
        }
        void OnDisable()
        {
            Logger.LogInfo("OnDisable()");
        }

        void Start()
        {
            Logger.LogInfo("Start()");
        }

        void Update()
        {
            if (this.UpdateFirstOn)
            {
                Logger.LogInfo("First Update()");
                this.UpdateFirstOn = false;
            }
        }
    }
}

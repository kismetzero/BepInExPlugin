using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace StreetsOfRoguePlugin
{
    [BepInPlugin("com.kisme.BepInEx.StreetsOfRogue.PluginTest", "StreetsOfRoguePluginTest", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        public static PluginTest _instance;
        public bool UpdateFirstOn = true;

        void Awake()
        {
            Logger.LogInfo("Awake()");
            _instance = this;
        }
        void OnDestroy() { Logger.LogInfo("OnDestroy()"); }
        void OnEnable() { Logger.LogInfo("OnEnable()"); }
        void OnDisable() { Logger.LogInfo("OnDisable()"); }
        void Start() { Logger.LogInfo("Start()"); }
        void Update()
        {
            if (this.UpdateFirstOn)
            {
                Logger.LogInfo("First Update()");
                this.UpdateFirstOn = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Logger.LogInfo("Alpha0");
            }
        }
    }
}

using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace WizardOfLegendPlugin
{
    [BepInPlugin("com.kisme.BepInEx.WizardOfLegend.PluginTest", "WizardOfLegendPluginTest", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        public static PluginTest Instance { get; set; }
        public bool UpdateFirstOn { get; set; } = true;

        void Awake()
        {
            Logger.LogInfo("Awake()");
            Instance = this;
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

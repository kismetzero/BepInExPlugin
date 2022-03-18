using System;
using BepInEx;
using UnityEngine;

namespace WizardOfLegendPlugin
{
    [BepInPlugin("com.kisme.BepInEx.WizardOfLegend.PluginTest", "MyFirstWizardOfLegendBepInExMod", "1.0")]
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
                DebugMenu.Instance.Toggle();
                Logger.LogInfo("DeBug菜单调用");
            }
        }
    }
}

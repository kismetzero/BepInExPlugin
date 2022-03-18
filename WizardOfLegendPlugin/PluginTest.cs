using System;
using BepInEx;
using UnityEngine;

namespace WizardOfLegendPlugin
{
    [BepInPlugin("com.kisme.BepInEx.WizardOfLegend.PluginTest", "MyFirstWizardOfLegendBepInExMod", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        private bool isUpdateFirstOn;
        void Awake()
        {
            Logger.LogInfo("Hello World！！！");
            Logger.LogInfo("插件的Awake()方法被调用了");
            this.isUpdateFirstOn = true;
        }

        void Start()
        {
            Logger.LogInfo("插件的Start()方法被调用了");
        }

        void Update()
        {
            if (this.isUpdateFirstOn)
            {
                Logger.LogInfo("插件的Update()方法被调用了");
                this.isUpdateFirstOn = false;
            }
            
            if (Input.GetKeyDown(KeyCode.F5))
            {
                DebugMenu.Instance.Toggle();
            }
        }
    }
}

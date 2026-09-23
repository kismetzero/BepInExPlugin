using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace JuicyRealmPlugin
{
    [BepInPlugin("com.kisme.BepInEx.JuicyRealm.PluginTest", "JuicyRealmPluginTest", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        public static PluginTest Instance { get; set; }

        void Awake()
        {
            Logger.LogInfo("Awake()");
            Instance = this;
            Harmony.CreateAndPatchAll(typeof(CheatFunc));
        }
        void OnDestroy() { Logger.LogInfo("OnDestroy()"); }
        void OnEnable() { Logger.LogInfo("OnEnable()"); }
        void OnDisable() { Logger.LogInfo("OnDisable()"); }
        void Start() { Logger.LogInfo("Start()"); }

        public bool UpdateFirst { get; set; } = true;
        void Update()
        {
            if (this.UpdateFirst)
            {
                Logger.LogInfo("First Update()");
                this.UpdateFirst = false;
            }

            if (Input.GetKeyDown(KeyCode.Equals))
            {
                Logger.LogInfo("KeyCode.Equals");
                WindowsDisplay = !WindowsDisplay;
            }
        }

        public bool WindowsDisplay { get; set; }
        public Rect windowRect = new Rect(100, 100, 200, 200); // 定义窗口位置 x y 宽 高
        void OnGUI()
        {
            if (WindowsDisplay)
            {
                Cursor.visible = true;
                /* 创建一个新窗口
                    注意：第一个参数(114514)为窗口ID，ID尽量设置的与众不同，
                    若与其他Mod的窗口ID相同，将会导致窗口冲突  */
                windowRect = GUI.Window(114514, windowRect, WindowFunc, "窗口");
            }
        }
        public void WindowFunc(int id)
        {
            GUILayout.Label($"窗口id = {id}");
        }
    }
}

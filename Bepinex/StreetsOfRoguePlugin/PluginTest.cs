using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace StreetsOfRoguePlugin
{
    [BepInPlugin("com.kismetzero.BepInEx.StreetsOfRoguePlugin", "StreetsOfRoguePluginTest", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        public static PluginTest Instance { get; set; }
        public ConfigEntry<KeyCode> windowsHotkey;

        void Awake()
        {
            Logger.LogInfo("Awake()");
            Instance = this;
            windowsHotkey = Config.Bind<KeyCode>("Config", "windowsHotkey", KeyCode.Equals, "窗口快捷键");
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

            if (Input.GetKeyDown(windowsHotkey.Value))
            {
                Logger.LogInfo("Windows Toggle");
                WindowsDisplay = !WindowsDisplay;
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                CheatFunc.Invisible = !CheatFunc.Invisible;
            }
        }

        public bool WindowsDisplay { get; set; }
        public Rect windowRect = new Rect(100f, 100f, 250f, 200f); // 定义窗口位置 x y 宽 高
        void OnGUI()
        {
            if (WindowsDisplay)
            {
                // if (!Cursor.visible) { Cursor.visible = true; }
                /* 创建一个新窗口
                    注意：第一个参数(114514)为窗口ID，ID尽量设置的与众不同，
                    若与其他Mod的窗口ID相同，将会导致窗口冲突  */
                windowRect = GUI.Window(114514, windowRect, WindowFunc, "窗口");
            }
        }
        public int tmp_int = 0;
        public float tmp_float = 0;
        public string tmp_curr_str;
        public string tmp_new_str;
        public static readonly string[] GodModeLabels = { "关", "零伤害", "跳伤害", "无敌" };
        public void WindowFunc(int id)
        {
            GUILayout.Label($"窗口id = {id}");

            GUILayout.BeginHorizontal();
            GUILayout.Label($"无敌模式:");
            GUILayout.Label($"HP:");
            tmp_curr_str = CheatFunc.Health.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.Health = tmp_float; }
            GUILayout.Label($"/");
            tmp_curr_str = CheatFunc.HealthMax.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.HealthMax = tmp_float; }
            GUILayout.EndHorizontal();

            CheatFunc.GodMode = (CheatFunc.GodModeType)GUILayout.Toolbar((int)CheatFunc.GodMode, GodModeLabels);

            GUILayout.BeginHorizontal();
            CheatFunc.Invisible = GUILayout.Toggle(CheatFunc.Invisible, "隐身");
            CheatFunc.AboveTheLaw = GUILayout.Toggle(CheatFunc.AboveTheLaw, "法外狂徒");
            if (GUILayout.Button("效果 复活")) { CheatFunc.AddStatusEffect("Resurrection"); }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("获得道具传送器")) { CheatFunc.AddItem("ItemTeleporter"); }
            if (GUILayout.Button("获得500")) { CheatFunc.AddMoney(500); }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("效果 践踏法律")) { CheatFunc.AddStatusEffect("AboveTheLaw"); }
            if (GUILayout.Button("技能 用药指南")) { CheatFunc.AddTrait("IdentifyScience"); }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("技能 全球定位")) { CheatFunc.AddTrait("MapFilled"); }
            if (GUILayout.Button("技能 随意传送")) { CheatFunc.AddTrait("TeleportAnytime"); }
            GUILayout.EndHorizontal();

            GUI.DragWindow();
        }
    }
}

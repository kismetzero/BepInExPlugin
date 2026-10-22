using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace EnterTheGungeonPlugin
{
    [BepInPlugin("com.kismetzero.BepInEx.EnterTheGungeonPlugin", "EnterTheGungeonPluginTest", "1.0")]
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
        }

        public bool WindowsDisplay { get; set; }
        public Rect windowRect = new Rect(100f, 100f, 200f, 300f); // 定义窗口位置 x y 宽 高
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
        public static readonly string[] PunchGodModeLabels = { "关", "零伤害", "无敌"};
        public void WindowFunc(int id)
        {
            GUILayout.Label($"窗口id = {id}");

            GUILayout.BeginHorizontal();
            CheatFunc.A_GodMode = GUILayout.Toggle(CheatFunc.A_GodMode, "无敌");
            GUILayout.Label($"金币:");
            tmp_curr_str = CheatFunc.Coin.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.Coin = tmp_int; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
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
            GUILayout.Label($"护盾:");
            tmp_curr_str = CheatFunc.Armor.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.Armor = tmp_float; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"空饷弹:");
            tmp_curr_str = CheatFunc.Blanks.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.Blanks = tmp_int; }
            CheatFunc.BlanksInf = GUILayout.Toggle(CheatFunc.BlanksInf, "无限");
            CheatFunc.BlanksLock = GUILayout.Toggle(CheatFunc.BlanksLock, "锁定");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"钥匙:");
            tmp_curr_str = CheatFunc.Keys.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.Keys = tmp_int; }
            GUILayout.Label($"老鼠宝箱钥匙:");
            tmp_curr_str = CheatFunc.RatResKeys.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.RatResKeys = tmp_int; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"老鼠关卡:");
            GUILayout.Label($"必杀星:");
            tmp_curr_str = CheatFunc.PunchStars.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.PunchStars = tmp_int; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"无敌模式:");
            GUILayout.Label($"HP:");
            tmp_curr_str = CheatFunc.PunchHealth.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.PunchHealth = tmp_float; }
            GUILayout.EndHorizontal();
            CheatFunc.PunchGodMode = (CheatFunc.PunchGodModeType)GUILayout.Toolbar((int)CheatFunc.PunchGodMode, PunchGodModeLabels);

            GUILayout.BeginHorizontal();
            GUILayout.Label($"时间:");
            tmp_curr_str = CheatFunc.PunchTime.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.PunchTime = tmp_float; }
            CheatFunc.PunchTimerFreeze = GUILayout.Toggle(CheatFunc.PunchTimerFreeze, "锁定");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"疲劳:");
            tmp_curr_str = CheatFunc.PunchFat.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.PunchFat = tmp_float; }
            CheatFunc.PunchNoFat = GUILayout.Toggle(CheatFunc.PunchNoFat, "锁定零");
            GUILayout.EndHorizontal();

            GUI.DragWindow();
        }
    }
}

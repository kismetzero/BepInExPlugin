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
            tmp_curr_str = CheatFunc.B_Currency.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.B_Currency = tmp_int; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"HP:");
            tmp_curr_str = CheatFunc.A_Health.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.A_Health = tmp_float; }
            GUILayout.Label($"/");
            tmp_curr_str = CheatFunc.A_HealthMax.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.A_HealthMax = tmp_float; }
            GUILayout.Label($"护盾:");
            tmp_curr_str = CheatFunc.A_Armor.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.A_Armor = tmp_float; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"空饷弹:");
            tmp_curr_str = CheatFunc.A_Blanks.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.A_Blanks = tmp_int; }
            CheatFunc.A_BlanksInf = GUILayout.Toggle(CheatFunc.A_BlanksInf, "无限");
            CheatFunc.A_BlanksLock = GUILayout.Toggle(CheatFunc.A_BlanksLock, "锁定");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"钥匙:");
            tmp_curr_str = CheatFunc.B_KeyBullets.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.B_KeyBullets = tmp_int; }
            GUILayout.Label($"老鼠宝箱钥匙:");
            tmp_curr_str = CheatFunc.B_RatKeys.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.B_RatKeys = tmp_int; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"老鼠关卡:");
            GUILayout.Label($"必杀星:");
            tmp_curr_str = CheatFunc.A_PunchStars.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.A_PunchStars = tmp_int; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"无敌模式:");
            GUILayout.Label($"HP:");
            tmp_curr_str = CheatFunc.A_PunchHealth.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.A_PunchHealth = tmp_float; }
            GUILayout.EndHorizontal();
            CheatFunc.A_PunchGodMode = (CheatFunc.PunchGodModeType)GUILayout.Toolbar((int)CheatFunc.A_PunchGodMode, PunchGodModeLabels);

            GUILayout.BeginHorizontal();
            GUILayout.Label($"时间:");
            tmp_curr_str = CheatFunc.A_PunchTime.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.A_PunchTime = tmp_float; }
            CheatFunc.A_PunchTimerFreeze = GUILayout.Toggle(CheatFunc.A_PunchTimerFreeze, "锁定");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"疲劳:");
            tmp_curr_str = CheatFunc.A_PunchFat.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.A_PunchFat = tmp_float; }
            CheatFunc.A_PunchNoFat = GUILayout.Toggle(CheatFunc.A_PunchNoFat, "锁定零");
            GUILayout.EndHorizontal();

            GUI.DragWindow();
        }
    }
}

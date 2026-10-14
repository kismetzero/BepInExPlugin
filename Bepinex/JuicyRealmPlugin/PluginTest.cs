using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace JuicyRealmPlugin
{
    [BepInPlugin("com.kismet.BepInEx.JuicyRealmPlugin", "JuicyRealmPluginTest", "1.0")]
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
        public Rect windowRect = new Rect(100f, 100f, 200f, 200f); // 定义窗口位置 x y 宽 高
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
        public string tmp_id_str;
        public string tmp_level_str;
        public static readonly string[] GodModeLabels = { "关", "跳Dmg", "跳Hurt" };
        public void WindowFunc(int id)
        {
            GUILayout.Label($"窗口id = {id}");

            GUILayout.BeginHorizontal();
            GUILayout.Label($"玩家:");
            tmp_curr_str = CheatFunc.A_PlayerNum.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.A_PlayerNum = tmp_int; }
            GUILayout.Label($"护盾:");
            tmp_curr_str = CheatFunc.A_CurrentShield.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.A_CurrentShield = tmp_float; }
            GUILayout.Label($"/{CheatFunc.A_ShieldMax}");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"无敌模式:");
            GUILayout.Label($"HP:");
            tmp_curr_str = CheatFunc.A_CurrentHp.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.A_CurrentHp = tmp_float; }
            GUILayout.Label($"/");
            tmp_curr_str = CheatFunc.A_HpMax.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.A_HpMax = tmp_float; }
            GUILayout.EndHorizontal();

            CheatFunc.A_GodMode = (CheatFunc.GodModeType)GUILayout.Toolbar((int)CheatFunc.A_GodMode, GodModeLabels);

            GUILayout.BeginHorizontal();
            GUILayout.Label($"金币:");
            tmp_curr_str = CheatFunc.A_Coin.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.A_Coin = tmp_int; }
            CheatFunc.A_InfSkill = GUILayout.Toggle(CheatFunc.A_InfSkill, "无限技能");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"能量:");
            tmp_curr_str = CheatFunc.A_CurrentEnergy.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.A_CurrentEnergy = tmp_float; }
            GUILayout.Label($"/");
            tmp_curr_str = CheatFunc.A_EnergyMax.ToString("F1");
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && float.TryParse(tmp_new_str, out tmp_float))
            { CheatFunc.A_EnergyMax = tmp_float; }
            CheatFunc.A_InfEnergy = GUILayout.Toggle(CheatFunc.A_InfEnergy, "无限能量");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"武器ID/lv:");
            tmp_id_str = GUILayout.TextField(tmp_id_str);
            tmp_level_str = GUILayout.TextField(tmp_level_str);
            if (GUILayout.Button("刷取") && int.TryParse(tmp_id_str, out int tmp_id) && int.TryParse(tmp_id_str, out int tmp_level))
            {
                List<GameObject> chestList = new List<GameObject>();
                chestList.Add(CheatFunc.A_GenerateWeapon(tmp_id, tmp_level));
                CheatFunc.A_SpawnDiyChest(chestList);
            }
            GUILayout.EndHorizontal();

            GUI.DragWindow();
        }
    }
}

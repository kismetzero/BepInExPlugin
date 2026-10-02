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
                CheatFunc.A_invisible = !CheatFunc.A_invisible;
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
        public void WindowFunc(int id)
        {
            GUILayout.Label($"窗口id = {id}");

            GUILayout.BeginHorizontal();
            CheatFunc.C_Player.ghost = GUILayout.Toggle(CheatFunc.C_Player.ghost, "幽灵");
            CheatFunc.C_Player.invisible = GUILayout.Toggle(CheatFunc.C_Player.invisible, "隐身");
            CheatFunc.C_Player.dontHate = GUILayout.Toggle(CheatFunc.C_Player.dontHate, "不恨");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            CheatFunc.C_Player.copsDontCare = GUILayout.Toggle(CheatFunc.C_Player.copsDontCare, "警察无视");
            CheatFunc.C_Player.aboveTheLaw = GUILayout.Toggle(CheatFunc.C_Player.aboveTheLaw, "法外狂徒");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            CheatFunc.C_Player.resurrect = GUILayout.Toggle(CheatFunc.C_Player.resurrect, "复活");
            CheatFunc.C_Player.quickResurrect = GUILayout.Toggle(CheatFunc.C_Player.quickResurrect, "快速复活");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("获得道具传送器")) { CheatFunc.A_AddItem("ItemTeleporter"); }
            if (GUILayout.Button("获得500")) { CheatFunc.A_AddMoney(500); }
            GUILayout.EndHorizontal();

            GUI.DragWindow();
        }
    }
}

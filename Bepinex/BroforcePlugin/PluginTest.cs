using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BroforcePlugin
{
    [BepInPlugin("com.kismetzero.BepInEx.BroforcePlugin", "BroforcePluginTest", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        public static PluginTest Instance { get; set; }
        public ConfigEntry<KeyCode> windowsHotkey;

        void Awake()
        {
            Logger.LogInfo("Awake()");
            Instance = this;
            windowsHotkey = Config.Bind<KeyCode>("Config", "windowsHotkey", KeyCode.Equals, "窗口快捷键");
            Harmony.CreateAndPatchAll(typeof(PluginTest));
            Harmony.CreateAndPatchAll(typeof(CheatFunc));
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (!this.gameObject.activeSelf)
            {
                Logger.LogInfo("OnSceneLoaded()");
                this.gameObject.SetActive(true);
            }
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
        public Rect windowRect = new Rect(100f, 100f, 200f, 500f); // 定义窗口位置 x y 宽 高
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
        public static readonly string[] PlayerNumStrings = { "0", "1", "2", "3" };
        public int tmp_int = 0;
        public string tmp_curr_str;
        public string tmp_new_str;
        public static readonly string[] HeroLabels = Enum.GetNames(typeof(HeroType));
        public Vector2 heroScrollPos;
        public void WindowFunc(int id)
        {
            GUILayout.Label($"窗口id = {id}");

            GUILayout.BeginHorizontal();
            GUILayout.Label($"玩家:");
            CheatFunc.A_PlayerNum = GUILayout.Toolbar(CheatFunc.A_PlayerNum, PlayerNumStrings);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            CheatFunc.A_GodMode = GUILayout.Toggle(CheatFunc.A_GodMode, "无敌");
            GUILayout.Label($"生命数:");
            tmp_curr_str = CheatFunc.A_Lives.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.A_Lives = tmp_int; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            CheatFunc.A_InfAmmo = GUILayout.Toggle(CheatFunc.A_InfAmmo, "无限弹药");
            GUILayout.Label($"弹药:");
            tmp_curr_str = CheatFunc.A_Ammo.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.A_Ammo = tmp_int; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"InfMin:");
            tmp_curr_str = CheatFunc.A_InfAmmoMin.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.A_InfAmmoMin = tmp_int; }

            GUILayout.Label($"InfMax:");
            tmp_curr_str = CheatFunc.A_InfAmmoMax.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.A_InfAmmoMax = tmp_int; }
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical();
            heroScrollPos = GUILayout.BeginScrollView(heroScrollPos);
            int currentIndex = Array.IndexOf(HeroLabels, CheatFunc.A_HeroType.ToString());
            int heroIndex = GUILayout.SelectionGrid(currentIndex, HeroLabels, 1);
            if (heroIndex != currentIndex) { CheatFunc.A_HeroType = (HeroType)Enum.Parse(typeof(HeroType), HeroLabels[heroIndex]); }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Cursor), nameof(Cursor.visible), MethodType.Setter)]
        public static void Cursor_visible_Setter_Prefix(ref bool value)
        {
            if (PluginTest.Instance.WindowsDisplay) { value = true; }
        }
    }
}

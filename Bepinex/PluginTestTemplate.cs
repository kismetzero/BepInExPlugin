using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlugin
{
    [BepInPlugin("com.kisme.BepInEx.Game.PluginTest", "GamePluginTest", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        public static PluginTest Instance { get; set; }

        void Awake()
        {
            Logger.LogInfo("Awake()");
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            Harmony.CreateAndPatchAll(typeof(PluginTest));
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

        [HarmonyPrefix, HarmonyPatch(typeof(PluginTest), "WindowFunc", new Type[] { typeof(int) })]
        public static bool PluginTest_WindowFunc_Prefix(PluginTest __instance, ref int id)
        {
            if (id != 114514) { return false; }
            return true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PluginTest), nameof(PluginTest.WindowsDisplay), MethodType.Getter)]
        public static void PluginTest_WindowsDisplay_Getter_Postfix(PluginTest __instance, ref bool __result)
        {
            Logger.LogInfo($"WindowsDisplay Getter __result = {__result}");
        }
    }
}

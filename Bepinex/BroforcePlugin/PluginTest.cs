using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BroforcePlugin
{
    [BepInPlugin("com.kisme.BepInEx.Broforce.PluginTest", "BroforcePluginTest", "1.0")]
    public class PluginTest : BaseUnityPlugin
    {
        public static PluginTest Instance { get; set; }
        public bool UpdateFirstOn { get; set; } = true;

        void Awake()
        {
            Logger.LogInfo("Awake()");
            Instance = this;
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
        void Update()
        {
            if (this.UpdateFirstOn)
            {
                Logger.LogInfo("First Update()");
                this.UpdateFirstOn = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CheatFunc.A_Lives += 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CheatFunc.A_Ammo += 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CheatFunc.A_InfAmmo = !CheatFunc.A_InfAmmo;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                CheatFunc.A_GodMode = !CheatFunc.A_GodMode;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                CheatFunc.A_HeroType = HeroType.IndianaBrones;
            }
        }

        //void OnGUI()
        //{
        //    if (WindowsDisplayOn)
        //    {
        //        Cursor.visible = true;
        //        // 定义窗口位置 x y 宽 高
        //        Rect windowRect = new Rect(100, 100, 200, 200);
        //        /* 创建一个新窗口
        //               注意：第一个参数(114514)为窗口ID，ID尽量设置的与众不同，
        //               若与其他Mod的窗口ID相同，将会导致窗口冲突  */
        //        windowRect = GUI.Window(114514, windowRect, DoMyWindow, "修改器窗口");
        //    }
        //}

    }
}

using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PunTeams;

namespace RwbyGePlugin
{
    public class PluginTest : MonoBehaviour
    {
        public static PluginTest Instance { get; set; }
        void Awake()
        {
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }
        }
        void OnDestroy() { }
        void OnEnable() { }
        void OnDisable() { }
        void Start() { }
        public bool UpdateFirst { get; set; } = true;
        void Update()
        {
            if (this.UpdateFirst)
            {
                this.UpdateFirst = false;
            }

            if (Input.GetKeyDown(KeyCode.Equals))
            {
                WindowsDisplay = !WindowsDisplay;
            }
        }
        public bool WindowsDisplay { get; set; }
        public Rect windowRect = new Rect(100f, 100f, 500f, 600f); // 定义窗口位置 x y 宽 高
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
        public Vector2 scrollPos;

        public void WindowFunc(int id)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"窗口id = {id}");
            GUILayout.Label($"Cursor.visible = {Cursor.visible}");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Toggle(UeLoader.test1, "test1");
            GUILayout.Toggle(UeLoader.test2, "test2");
            GUILayout.Toggle(UeLoader.test3, "test3");
            GUILayout.Toggle(UeLoader.test4, "test4");
            GUILayout.Toggle(UeLoader.test5, "test5");
            GUILayout.EndHorizontal();

            if (GUILayout.Button("UeInit")) { UeInit(); }
            GUILayout.Label($"test_str = {test_str}");

            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            // foreach (var obj in GameObject.FindObjectsOfType<GameObject>()) { GUILayout.Label($"{obj.name}"); }

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                GUILayout.Label($"{asm.GetName().Name}");
            }

            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        public string test_str = "null";
        public void UeInit()
        {
            if (UeLoader.UeAsm == null)
            {
                test_str = $"fail: UeLoader.UeAsm == null";
                return;
            }
            Type type = UeLoader.UeAsm.GetType("UnityExplorer.ExplorerStandalone");
            if (type == null)
            {
                test_str = $"fail: type == null";
                return;
            }
            MethodInfo methodinfo = type.GetMethod("CreateInstance", BindingFlags.Public | BindingFlags.Static, null, Type.EmptyTypes, null);
            if (methodinfo == null)
            {
                test_str = $"fail: methodinfo == null";
                return;
            }
            try
            {
                var res = methodinfo.Invoke(null, null);
                test_str = $"sccess: {res}";
            }
            catch (Exception e)
            {
                test_str = $"fail: {e.Message}";
            }
        }
    }
}

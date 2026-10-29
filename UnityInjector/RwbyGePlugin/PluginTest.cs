using UnityEngine;
using UnityEngine.SceneManagement;

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
        public Rect windowRect = new Rect(100f, 100f, 300f, 600f); // 定义窗口位置 x y 宽 高
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
        public int tmp_int = 0;
        public float tmp_float = 0;
        public string tmp_curr_str;
        public string tmp_new_str;

        public void WindowFunc(int id)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"窗口id = {id}");
            GUILayout.Label($"Cursor.visible = {Cursor.visible}");
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            GlobalSettings.GodMode = GUILayout.Toggle(GlobalSettings.GodMode, "GodMode");

            GUILayout.Label($"HP:");
            tmp_curr_str = CheatFunc.Health.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.Health = tmp_int; }
            GUILayout.Label($"/");
            tmp_curr_str = CheatFunc.HealthMax.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.HealthMax = tmp_int; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"S:");
            tmp_curr_str = CheatFunc.Shields.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.Shields = tmp_int; }
            GUILayout.Label($"/");
            tmp_curr_str = CheatFunc.ShieldsMax.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.ShieldsMax = tmp_int; }

            GUILayout.Label($"SP:");
            tmp_curr_str = CheatFunc.Special.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.Special = tmp_int; }
            GUILayout.Label($"/");
            tmp_curr_str = CheatFunc.SpecialMax.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.SpecialMax = tmp_int; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("AddExp")) { CheatFunc.AddExp(999); }
            GUILayout.Label($"经验: {CheatFunc.Experience}");

            GUILayout.Label($"技能点:");
            tmp_curr_str = CheatFunc.SkillPoints.ToString();
            tmp_new_str = GUILayout.TextField(tmp_curr_str);
            if (tmp_new_str != tmp_curr_str && int.TryParse(tmp_new_str, out tmp_int))
            { CheatFunc.SkillPoints = tmp_int; }
            GUILayout.EndHorizontal();

            GlobalSettings.IgnorePrestigeRequirements = GUILayout.Toggle(GlobalSettings.IgnorePrestigeRequirements, "无条件");

            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            foreach (var obj in GameObject.FindObjectsOfType<GameObject>()) { GUILayout.Label($"{obj.name}"); }

            //foreach (var asm in AppDomain.CurrentDomain.GetAssemblies()) { GUILayout.Label($"{asm.GetName().Name}"); }

            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUI.DragWindow();
        }
    }
}

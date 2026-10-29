using UnityEngine;

namespace RwbyGePlugin
{
    public class PluginLoader
    {
        public static GameObject PluginInstance;
        public static void Load()
        {
            PluginInstance = new GameObject();
            PluginInstance.AddComponent<PluginTest>();
            UnityEngine.GameObject.DontDestroyOnLoad(PluginInstance);
            UeLoader.LoadAsm();
        }
    }
}

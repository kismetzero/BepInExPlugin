using UnityEngine;

namespace NeonAbyssPlugin
{
    public class PluginLoader
    {
        public static GameObject NeonAbyssPluginInstance;
        public static void Load()
        {
            NeonAbyssPluginInstance = new GameObject();
            NeonAbyssPluginInstance.AddComponent<PluginTest>();
            UnityEngine.GameObject.DontDestroyOnLoad(NeonAbyssPluginInstance);
        }
    }
}

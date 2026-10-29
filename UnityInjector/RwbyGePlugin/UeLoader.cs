using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace RwbyGePlugin
{
    public class UeLoader
    {
        public static bool Load_All;
        public static bool Load_0Harmony;
        public static bool Load_MonoModUtils;
        public static bool Load_MonoModRuntime;
        public static bool Load_UniverseLib;
        public static bool Load_UnityExplorer;

        public static Assembly UeAsm;
        public static bool UeInitFlag;

        public static Assembly LoadAsmByName(string name)
        {
            string path = Application.dataPath + "/Managed/UnityExplorer/" + name;
            if (!File.Exists(path)) { return null; }
            byte[] data = File.ReadAllBytes(path);
            return Assembly.Load(data); ;
        }

        public static void LoadAsm()
        {
            CheckAsm();
            if (!Load_0Harmony) { LoadAsmByName("0Harmony.dll"); }
            if (!Load_MonoModUtils) { LoadAsmByName("MonoMod.Utils.dll"); }
            if (!Load_MonoModRuntime) { LoadAsmByName("MonoMod.RuntimeDetour.dll"); }
            if (!Load_UniverseLib) { LoadAsmByName("UniverseLib.Mono.dll"); }
            if (!Load_UnityExplorer) { LoadAsmByName("UnityExplorer.STANDALONE.Mono.dll"); }
            CheckAsm();
        }

        public static bool CheckAsm()
        {
            Load_All = Load_0Harmony = false;
            Load_MonoModUtils = Load_MonoModRuntime = false;
            Load_UniverseLib = Load_UnityExplorer = false;

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.GetName().Name == "0Harmony") { Load_0Harmony = true; }
                if (asm.GetName().Name == "MonoMod.Utils") { Load_MonoModUtils = true; }
                if (asm.GetName().Name == "MonoMod.RuntimeDetour") { Load_MonoModRuntime = true; }
                if (asm.GetName().Name == "UniverseLib.Mono") { Load_UniverseLib = true; }
                if (asm.GetName().Name == "UnityExplorer.Standalone.Mono")
                {
                    UeAsm = asm;
                    Load_UnityExplorer = true;
                }
            }

            if (Load_0Harmony && Load_MonoModUtils && Load_MonoModRuntime &&
                Load_UniverseLib && Load_UnityExplorer) { Load_All = true; }

            return Load_All;
        }

        public static bool test1, test2, test3, test4, test5;

        public static void Init()
        {
            test1 = true;
            if (UeInitFlag) { return; }

            if (!Load_All) { LoadAsm(); }

            if (!Load_All) { return; }
            test2 = true;

            Type type = UeAsm.GetType("UnityExplorer.ExplorerStandalone");

            if (type == null) { return; }
            test3 = true;

            MethodInfo methodinfo = type.GetMethod("CreateInstance", BindingFlags.Public | BindingFlags.Static, null, Type.EmptyTypes, null);

            if (methodinfo == null) { return; }
            test4 = true;

            methodinfo.Invoke(null, null);
            test5 = true;

            UeInitFlag = true;
        }
    }
}

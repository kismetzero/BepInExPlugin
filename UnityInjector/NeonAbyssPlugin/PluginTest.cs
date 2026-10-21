using System;
using System.IO;
using UnityEngine;

namespace NeonAbyssPlugin
{
    public class Injector
    {
        public static void Inject()
        {
            File.WriteAllText("C:/tmp/tmp.txt", "你好！Neon Abyss!");
        }
    }
}

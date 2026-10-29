using NEON.Framework;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeonAbyssPlugin
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
        void OnDestroy() {  }
        void OnEnable() {  }
        void OnDisable() {  }
        void Start() {  }
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
        public bool bossCoins, finalCoins, abyssCoins, exchangeCoins;
        public bool health, healthMax, shields, shieldsMax, mana, manaMax;
        public bool coin, coinMax, key, keyMax, bomb, bombMax;
        public bool fireBomb, iceBomb, thunderBomb, liquorBomb;
        public Vector2 scrollPos;
        public void WindowFunc(int id)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"窗口id = {id}");
            GUILayout.Label($"Cursor.visible = {Cursor.visible}");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Coins");
            if (GUILayout.Button("+1")) { addCoins(1); }
            if (GUILayout.Button("-1")) { addCoins(-1); }
            if (GUILayout.Button("+10")) { addCoins(10); }
            if (GUILayout.Button("-10")) { addCoins(-10); }
            if (GUILayout.Button("+100")) { addCoins(100); }
            if (GUILayout.Button("-100")) { addCoins(-100); }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            bossCoins = GUILayout.Toggle(bossCoins, $"Boss:[{CheatFunc.BossCoins}]");
            finalCoins = GUILayout.Toggle(finalCoins, $"Final:[{CheatFunc.FinalCoins}]");
            abyssCoins = GUILayout.Toggle(abyssCoins, $"Abyss:[{CheatFunc.AbyssCoins}]");
            exchangeCoins = GUILayout.Toggle(exchangeCoins, $"Exc:[{CheatFunc.ExchangeCoins}]");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Health:[{CheatFunc.Health}/{CheatFunc.HealthMax}]");
            health = GUILayout.Toggle(health, $"Health");
            healthMax = GUILayout.Toggle(healthMax, $"HealthMax");
            CheatFunc.GodMode = GUILayout.Toggle(CheatFunc.GodMode, $"GodMode");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Shields:[{CheatFunc.Shields}/{CheatFunc.ShieldsMax}]");
            shields = GUILayout.Toggle(shields, $"Shields");
            shieldsMax = GUILayout.Toggle(shieldsMax, $"ShieldsMax");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Mana:[{CheatFunc.Mana}/{CheatFunc.ManaMax}]");
            mana = GUILayout.Toggle(mana, $"Mana");
            manaMax = GUILayout.Toggle(manaMax, $"ManaMax");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Coin:[{CheatFunc.Coin}/{CheatFunc.CoinMax}]");
            coin = GUILayout.Toggle(coin, $"Coin");
            coinMax = GUILayout.Toggle(coinMax, $"CoinMax");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Key:[{CheatFunc.Key}/{CheatFunc.KeyMax}]");
            key = GUILayout.Toggle(key, $"Key");
            keyMax = GUILayout.Toggle(keyMax, $"KeyMax");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("+1")) { add(1); }
            if (GUILayout.Button("-1")) { add(-1); }
            if (GUILayout.Button("+10")) { add(10); }
            if (GUILayout.Button("-10")) { add(-10); }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Bomb:[{CheatFunc.Bomb}/{CheatFunc.BombMax}]");
            bomb = GUILayout.Toggle(bomb, $"Bomb");
            if (GUILayout.Button("+1")) { addBomb(1); }
            if (GUILayout.Button("-1")) { addBomb(-1); }
            if (GUILayout.Button("+10")) { addBomb(10); }
            if (GUILayout.Button("-10")) { addBomb(-10); }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            fireBomb = GUILayout.Toggle(fireBomb, $"Fire:[{CheatFunc.FireBomb}]");
            iceBomb = GUILayout.Toggle(iceBomb, $"Ice:[{CheatFunc.IceBomb}]");
            thunderBomb = GUILayout.Toggle(thunderBomb, $"Thunder:[{CheatFunc.ThunderBomb}]");
            liquorBomb = GUILayout.Toggle(liquorBomb, $"Liquor:[{CheatFunc.LiquorBomb}]");
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            Attri playerAttri = CheatFunc.PlayerAttri;
            if (playerAttri != null)
            {
                foreach (var kv in playerAttri.FixedValues.DictInstance)
                {
                    GUILayout.Label($"[FixedValues] {kv.Key} = {kv.Value}");
                }

                foreach (var kv in playerAttri.Formulas.DictInstance)
                {
                    GUILayout.Label($"[Formulas] {kv.Key} = {kv.Value}");
                }

                foreach (var kv in playerAttri.ClampList.DictInstance)
                {
                    GUILayout.Label($"[ClampList] {kv.Key} = [{kv.Value.x}, {kv.Value.y}]");
                }
            }

            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        public void addCoins(int num)
        {
            if (bossCoins) { CheatFunc.BossCoins += num; }
            if (finalCoins) { CheatFunc.FinalCoins += num; }
            if (abyssCoins) { CheatFunc.AbyssCoins += num; }
            if (exchangeCoins) { CheatFunc.ExchangeCoins += num; }
        }
        public void add(int num)
        {
            if (health) { CheatFunc.Health += num; }
            if (healthMax) { CheatFunc.HealthMax += num; }
            if (shields) { CheatFunc.Shields += num; }
            if (shieldsMax) { CheatFunc.ShieldsMax += num; }
            if (mana) { CheatFunc.Mana += num; }
            if (manaMax) { CheatFunc.ManaMax += num; }

            if (coin) { CheatFunc.Coin += num; }
            if (coinMax) { CheatFunc.CoinMax += num; }
            if (key) { CheatFunc.Key += num; }
            if (keyMax) { CheatFunc.KeyMax += num; }
        }
        public void addBomb(int num)
        {
            if (bomb) { CheatFunc.Bomb += num; }
            if (bombMax) { CheatFunc.BombMax += num; }

            if (fireBomb) { CheatFunc.FireBomb += num; }
            if (iceBomb) { CheatFunc.IceBomb += num; }
            if (thunderBomb) { CheatFunc.ThunderBomb += num; }
            if (liquorBomb) { CheatFunc.LiquorBomb += num; }
        }

        public void dumpAttri()
        {
            StringBuilder sb = new StringBuilder();
            Attri playerAttri = CheatFunc.PlayerAttri;
            if (playerAttri != null)
            {
                foreach (var kv in playerAttri.FixedValues.DictInstance)
                {
                    sb.AppendLine($"[FixedValues] {kv.Key} = {kv.Value}");
                }

                foreach (var kv in playerAttri.Formulas.DictInstance)
                {
                    sb.AppendLine($"[Formulas] {kv.Key} = {kv.Value}");
                }

                foreach (var kv in playerAttri.ClampList.DictInstance)
                {
                    sb.AppendLine($"[ClampList] {kv.Key} = [{kv.Value.x}, {kv.Value.y}]");
                }
                string tmp_path = Path.Combine(Application.dataPath, "../DumpAttri.txt");
                File.WriteAllText(tmp_path, sb.ToString(), Encoding.UTF8);
            }
        }
    }
}

using NEON.Framework;
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
        public Rect windowRect = new Rect(100f, 100f, 300f, 500f); // 定义窗口位置 x y 宽 高
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
        public void WindowFunc(int id)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"窗口id = {id}");
            GUILayout.Label($"Cursor.visible = {Cursor.visible}");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Coins");
            bossCoins = GUILayout.Toggle(bossCoins, $"Boss:[{CheatFunc.BossCoins}]");
            finalCoins = GUILayout.Toggle(finalCoins, $"Final:[{CheatFunc.FinalCoins}]");
            abyssCoins = GUILayout.Toggle(abyssCoins, $"Abyss:[{CheatFunc.AbyssCoins}]");
            GUILayout.EndHorizontal();
            exchangeCoins = GUILayout.Toggle(exchangeCoins, $"Exchange:[{CheatFunc.ExchangeCoins}]");

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Health:[{CheatFunc.Health}/{CheatFunc.HealthMax}]");
            health = GUILayout.Toggle(health, $"Health");
            healthMax = GUILayout.Toggle(healthMax, $"HealthMax");
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
            GUILayout.Label($"Bomb:[{CheatFunc.Bomb}/{CheatFunc.BombMax}]");
            bomb = GUILayout.Toggle(bomb, $"Bomb");
            bombMax = GUILayout.Toggle(bombMax, $"BombMax");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            fireBomb = GUILayout.Toggle(fireBomb, $"Fire:[{CheatFunc.FireBomb}]");
            iceBomb = GUILayout.Toggle(iceBomb, $"Ice:[{CheatFunc.IceBomb}]");
            thunderBomb = GUILayout.Toggle(thunderBomb, $"Thunder:[{CheatFunc.ThunderBomb}]");
            liquorBomb = GUILayout.Toggle(liquorBomb, $"Liquor:[{CheatFunc.LiquorBomb}]");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("+1")) { add(1); }
            if (GUILayout.Button("-1")) { add(-1); }
            if (GUILayout.Button("+10")) { add(10); }
            if (GUILayout.Button("-10")) { add(-10); }
            if (GUILayout.Button("+100")) { add(100); }
            if (GUILayout.Button("-100")) { add(-100); }
            GUILayout.EndHorizontal();

            GUI.DragWindow();
        }

        public void add(int num)
        {
            if (bossCoins) { CheatFunc.UnlockService?.IncBossCoins(num); }
            if (finalCoins) { CheatFunc.UnlockService?.IncFinalCoins(num); }
            if (abyssCoins) { CheatFunc.UnlockService?.IncAbyssCoins(num); }
            if (exchangeCoins) { CheatFunc.UnlockService?.IncExchangeCoins(num); }

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
            if (bomb) { CheatFunc.Bomb += num; }
            if (bombMax) { CheatFunc.BombMax += num; }

            if (fireBomb) { CheatFunc.FireBomb += num; }
            if (iceBomb) { CheatFunc.IceBomb += num; }
            if (thunderBomb) { CheatFunc.ThunderBomb += num; }
            if (liquorBomb) { CheatFunc.LiquorBomb += num; }
        }
    }
}

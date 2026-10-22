using NEON.Framework;

namespace NeonAbyssPlugin
{
    public class CheatFunc
    {
        public static NEON.UI.UnlockSystem.Service.UnlockService UnlockService => Global.GetService<NEON.UI.UnlockSystem.Service.UnlockService>(true);
        public static NEON.Framework.SaveData.SaveDataService SaveDataService => Global.GetService<NEON.Framework.SaveData.SaveDataService>(true);
        public static NEON.Framework.SaveData.SaveData SaveData => (SaveDataService != null) ? SaveDataService.Data : default;
        public static NEON.UI.UnlockSystem.Data.UnlockSystemData UnlockSystemData => (SaveData != null) ? SaveData.UnlockSystemData : default;
        public static int BossCoins
        {
            get => (UnlockSystemData != null) ? UnlockSystemData.BossCoins : default;
            set { if (UnlockSystemData != null) { UnlockSystemData.BossCoins = value; } }
        }
        public static int FinalCoins
        {
            get => (UnlockSystemData != null) ? UnlockSystemData.FinalCoins : default;
            set { if (UnlockSystemData != null) { UnlockSystemData.FinalCoins = value; } }
        }
        public static int AbyssCoins
        {
            get => (UnlockSystemData != null) ? UnlockSystemData.AbyssCoins : default;
            set { if (UnlockSystemData != null) { UnlockSystemData.AbyssCoins = value; } }
        }
        public static int ExchangeCoins
        {
            get => (UnlockSystemData != null) ? UnlockSystemData.ExchangeCoins : default;
            set { if (UnlockSystemData != null) { UnlockSystemData.ExchangeCoins = value; } }
        }

        public static PlayerState PS => Global.PlayerState;
        public static Attri PlayerAttri => (PS != null) ? PS.attrs : null;
        public static int Health
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("CurrentHearts") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("CurrentHearts", value); } }
        }
        public static int HealthMax
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("MaxHearts") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("MaxHearts", value); } }
        }
        public static int Shields
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("CurrentShields") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("CurrentShields", value); } }
        }
        public static int ShieldsMax
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("MaxShields") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("MaxShields", value); } }
        }
        public static int Mana
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("CurrentMana") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("CurrentMana", value); } }
        }
        public static int ManaMax
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("MaxMana") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("MaxMana", value); } }
        }
        public static int Coin
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("Coin") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("Coin", value); } }
        }
        public static int CoinMax
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("MaxCoin") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("MaxCoin", value); } }
        }

        public static int Key
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("Key") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("InfiniteKey", value); } }
        }
        public static int KeyMax
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("Key") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("MaxKey", value); } }
        }

        public static int BombMax
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("MaxBomb") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("MaxBomb", value); } }
        }
        public static int Bomb
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("DefaultBomb") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("DefaultBomb", value); } }
        }
        public static int FireBomb
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("FireBomb") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("FireBomb", value); } }
        }
        public static int IceBomb
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("IceBomb") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("IceBomb", value); } }
        }
        public static int ThunderBomb
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("ThunderBomb") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("ThunderBomb", value); } }
        }
        public static int LiquorBomb
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("Liquor_Bomb") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("Liquor_Bomb", value); } }
        }
    }
}

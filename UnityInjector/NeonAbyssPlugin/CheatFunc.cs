using NEON.Framework;

namespace NeonAbyssPlugin
{
    public class CheatFunc
    {
        public static NEON.Framework.SaveData.SaveDataService SaveDataService => Global.GetService<NEON.Framework.SaveData.SaveDataService>(true);
        public static NEON.Framework.SaveData.SaveData SaveData => (SaveDataService != null) ? SaveDataService.Data : default;
        public static NEON.UI.UnlockSystem.Data.UnlockSystemData UnlockSystemData => (SaveData != null) ? SaveData.UnlockSystemData : default;
        public static NEON.UI.UnlockSystem.Service.UnlockService UnlockService => Global.GetService<NEON.UI.UnlockSystem.Service.UnlockService>(true);
        public static int BossCoins
        {
            get => (UnlockSystemData != null) ? UnlockSystemData.BossCoins : default;
            set
            {
                if (UnlockService == null || UnlockSystemData == null) { return; }
                int delta = value - UnlockSystemData.BossCoins;
                if (delta != 0) { UnlockService.IncBossCoins(delta); }
            }
        }
        public static int FinalCoins
        {
            get => (UnlockSystemData != null) ? UnlockSystemData.FinalCoins : default;
            set
            {
                if (UnlockService == null || UnlockSystemData == null) { return; }
                int delta = value - UnlockSystemData.BossCoins;
                if (delta != 0) { UnlockService.IncFinalCoins(delta); }
            }
        }
        public static int AbyssCoins
        {
            get => (UnlockSystemData != null) ? UnlockSystemData.AbyssCoins : default;
            set
            {
                if (UnlockService == null || UnlockSystemData == null) { return; }
                int delta = value - UnlockSystemData.BossCoins;
                if (delta != 0) { UnlockService.IncAbyssCoins(delta); }
            }
        }
        public static int ExchangeCoins
        {
            get => (UnlockSystemData != null) ? UnlockSystemData.ExchangeCoins : default;
            set
            {
                if (UnlockService == null || UnlockSystemData == null) { return; }
                int delta = value - UnlockSystemData.BossCoins;
                if (delta != 0) { UnlockService.IncExchangeCoins(delta); }
            }
        }

        public static PlayerState PS => Global.PlayerState;
        public static Attri PlayerAttri => (PS != null) ? PS.attrs : null;
        public static NEON.Game.Managers.NEONPlayerState NPS => Global.PlayerState as NEON.Game.Managers.NEONPlayerState;
        public static NEON.Game.GameModes.ControlPlayerMode CPM => Global.GetGameMode<NEON.Game.GameModes.ControlPlayerMode>();
        public static NEON.Game.Actors.PlayerActor PA => (CPM != null) ? CPM.Player : default;
        public static bool GodMode
        {
            get => (PA != null) ? PA._hearts.isInvincible : default;
            set { if (PA != null) { PA._hearts.isInvincible = value; } }
        }
        public static int Health
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("CurrentHearts") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("CurrentHearts", value); NPS.RefreshTopLeftUi(); } }
        }
        public static int HealthMax
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("MaxHearts") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("MaxHearts", value); NPS.RefreshTopLeftUi(); } }
        }
        public static int Shields
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("CurrentShields") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("CurrentShields", value); NPS.RefreshTopLeftUi(); } }
        }
        public static int ShieldsMax
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("MaxShields") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("MaxShields", value); NPS.RefreshTopLeftUi(); } }
        }
        public static int Mana
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("CurrentMana") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("CurrentMana", value); NPS.RefreshTopLeftUi(); } }
        }
        public static int ManaMax
        {
            get => (PlayerAttri != null) ? PlayerAttri.i("MaxMana") : default;
            set { if (PlayerAttri != null) { PlayerAttri.Set("MaxMana", value); NPS.RefreshTopLeftUi(); } }
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

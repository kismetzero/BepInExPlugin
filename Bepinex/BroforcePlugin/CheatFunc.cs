using HarmonyLib;

namespace BroforcePlugin
{
    public class CheatFunc
    {
        public static int PlayerNum { get; set; } = 0;

        public static Player Player
        {
            get
            {
                if (HeroController.Instance == null) { return default; }
                if (PlayerNum < 0 || PlayerNum > 4) { return default; }
                return HeroController.players[PlayerNum];
            }
        }
        public static int HP
        {
            get => (Player != null) ? Player.Lives : default;
            set { if (Player != null) { Player.Lives = value; } }
        }
        public static bool GodMode
        {
            get => (Player != null) ? Player.character.invulnerable : default;
            set { if (Player != null) { Player.character.invulnerable = value; } }
        }
        public static int Ammo
        {
            get
            {
                Player player = Player;
                if (player == null) { return default; }
                var broBase = player.character as BroBase;
                return broBase.SpecialAmmo;
            }
            set
            {
                Player player = Player;
                if (player == null) { return; }
                var broBase = player.character as BroBase;
                broBase.SpecialAmmo = value;
            }
        }
        public static bool InfAmmo { get; set; } = false;
        public static int InfAmmoMin { get; set; } = 2;
        public static int InfAmmoMax { get; set; } = 5;

        [HarmonyPrefix, HarmonyPatch(typeof(BroBase), nameof(BroBase.SpecialAmmo), MethodType.Setter)]
        public static void BroBase_SpecialAmmo_Setter_Prefix(ref int value)
        {
            if (InfAmmo)
            {
                if (value < InfAmmoMin) { value = InfAmmoMax; }
            }
        }

        public static HeroType A_HeroType
        {
            get => (Player != null) ? Player.heroType : HeroType.None;
            set
            {
                Player player = Player;
                if (player == null) { return; }
                if (PlayerNum < 0 || PlayerNum > 4) { return; }
                HeroController.ChangeBro(PlayerNum, value);
            }
        }
    }
}

using HarmonyLib;
using static Text3D;

namespace BroforcePlugin
{
    public class CheatFunc
    {
        public static int A_PlayerNum { get; set; } = 0;

        public static HeroController B_HeroCtrl => HeroController.Instance;
        public static Player B_Player
        {
            get
            {
                if (HeroController.Instance == null) { return null; }
                if (A_PlayerNum < 0 || A_PlayerNum > 4) { return null; }
                return HeroController.players[A_PlayerNum];
            }
        }
        public static int A_Lives
        {
            get => (B_Player != null) ? B_Player.Lives : 0;
            set { if (B_Player != null) { B_Player.Lives = value; } }
        }
        public static bool A_GodMode
        {
            get => (B_Player != null) ? B_Player.character.invulnerable : false;
            set { if (B_Player != null) { B_Player.character.invulnerable = value; } }
        }
        public static int A_Ammo
        {
            get
            {
                Player player = B_Player;
                if (player == null) { return 0; }
                var broBase = player.character as BroBase;
                return broBase.SpecialAmmo;
            }
            set
            {
                Player player = B_Player;
                if (player == null) { return; }
                var broBase = player.character as BroBase;
                broBase.SpecialAmmo = value;
            }
        }
        public static bool A_InfAmmo { get; set; } = false;
        public static int A_InfAmmoMin { get; set; } = 2;
        public static int A_InfAmmoMax { get; set; } = 5;

        [HarmonyPrefix, HarmonyPatch(typeof(BroBase), nameof(BroBase.SpecialAmmo), MethodType.Setter)]
        public static void BroBase_SpecialAmmo_Setter_Prefix(ref int value)
        {
            if (A_InfAmmo)
            {
                if (value < A_InfAmmoMin) { value = A_InfAmmoMax; }
            }
        }

        public static HeroType A_HeroType
        {
            get => (B_Player != null) ? B_Player.heroType : HeroType.None;
            set
            {
                Player player = B_Player;
                if (player == null) { return; }
                if (A_PlayerNum < 0 || A_PlayerNum > 4) { return; }
                HeroController.ChangeBro(A_PlayerNum, value);
            }
        }
    }
}

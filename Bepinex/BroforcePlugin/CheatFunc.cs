using HarmonyLib;

namespace BroforcePlugin
{
    public class CheatFunc
    {
        public static int A_PlayerNum = 0;

        public static bool A_InfAmmo = false;
        public static int A_InfAmmoMin = 2;
        public static int A_InfAmmoMax = 5;

        // public static HeroController C_HeroCtrl { get { return HeroController.Instance; } }
        public static HeroController B_HeroCtrl => HeroController.Instance;
        public static Player B_Player
        {
            get
            {
                if (HeroController.Instance == null) { return null; }
                return HeroController.players[A_PlayerNum];
            }
        }
        public static int A_Life
        {
            get
            {
                if (HeroController.Instance == null) { return 0; }
                Player player = B_Player; if (player == null) { return 0; }
                return player.Lives;
            }
        }
        public static bool A_GodMode
        {
            get
            {
                if (HeroController.Instance == null) { return false; }
                Player player = B_Player; if (player == null) { return false; }
                return player.character.invulnerable;
            }
            set
            {
                if (HeroController.Instance == null) { return; }
                Player player = B_Player; if (player == null) { return; }
                player.character.invulnerable = value;
            }
        }
        public static int A_Ammo
        {
            get
            {
                if (HeroController.Instance == null) { return 0; }
                Player player = B_Player; if (player == null) { return 0; }
                var broBase = player.character as BroBase;
                return broBase.SpecialAmmo;
            }
            set
            {
                if (HeroController.Instance == null) { return; }
                Player player = B_Player; if (player == null) { return; }
                var broBase = player.character as BroBase;
                broBase.SpecialAmmo = value;
            }
        }
        public static HeroType A_HeroType
        {
            get
            {
                if (HeroController.Instance == null) { return HeroType.None; }
                Player player = B_Player; if (player == null) { return HeroType.None; }
                return player.heroType;
            }
            set
            {
                if (HeroController.Instance == null) { return; }
                HeroController.ChangeBro(A_PlayerNum, value);
            }
        }
        public static void A_AddLife()
        {
            if (HeroController.Instance == null) { return; }
            HeroController.AddLife(A_PlayerNum);
        }

        [HarmonyPrefix, HarmonyPatch(typeof(BroBase), nameof(BroBase.SpecialAmmo), MethodType.Setter)]
        public static void BroBase_SpecialAmmo_Setter_Prefix(ref int value)
        {
            if (A_InfAmmo)
            {
                if (value < A_InfAmmoMin) { value = A_InfAmmoMax; }
            }
        }
    }
}

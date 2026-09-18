using HarmonyLib;

namespace BroforcePlugin
{
    public class CheatFunc
    {
        public static int pNum = 0;
        public static HeroType hType = HeroType.IndianaBrones;

        public static bool infiniteAmmo = false;
        public static int sAmmoMin = 2;
        public static int sAmmoMax = 5;

        public static void A_AddLife(int playerNum)
        {
            if (HeroController.Instance == null) { return; }
            HeroController.AddLife(playerNum);
        }
        public static void A_ChangeBro(int playerNum, HeroType newHeroType)
        {
            if (HeroController.Instance == null) { return; }
            HeroController.ChangeBro(playerNum, newHeroType);
        }
        public static void A_AddAmmo(int playerNum)
        {
            if (HeroController.Instance == null) { return; }
            var broBase = HeroController.players[playerNum].character as BroBase;
            broBase.SpecialAmmo += 1;
        }
        public static void A_GodMode(int playerNum)
        {
            if (HeroController.Instance == null) { return; }
            bool isGod = HeroController.players[playerNum].character.invulnerable;
            if (isGod)
            {
                HeroController.players[playerNum].character.invulnerable = false;
            }
            else
            {
                HeroController.players[playerNum].character.invulnerable = true;
            }
        }
        public static void A_InfAmmo()
        {
            if (HeroController.Instance == null) { return; }
            if (infiniteAmmo) { infiniteAmmo = false; }
            else { infiniteAmmo = true;  }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(BroBase), nameof(BroBase.SpecialAmmo), MethodType.Setter)]
        public static void BroBase_SpecialAmmo_Setter_Prefix(ref int value)
        {
            if (infiniteAmmo)
            {
                if (value < sAmmoMin) { value = sAmmoMax; }
            }
        }
    }
}

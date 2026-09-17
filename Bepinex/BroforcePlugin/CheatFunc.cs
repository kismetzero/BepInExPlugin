using UnityEngine;
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

        public static void a_addLife(int playerNum)
        {
            if (HeroController.Instance == null) { return; }
            Debug.Log("CheatFunc: Add Life");
            HeroController.AddLife(playerNum);
        }
        public static void a_changeBro(int playerNum, HeroType newHeroType)
        {
            if (HeroController.Instance == null) { return; }
            Debug.Log("CheatFunc: Change Bro");
            HeroController.ChangeBro(playerNum, newHeroType);
        }
        public static void a_addAmmo(int playerNum)
        {
            if (HeroController.Instance == null) { return; }
            Debug.Log("CheatFunc: Add Ammo");
            var broBase = HeroController.players[playerNum].character as BroBase;
            broBase.SpecialAmmo += 1;
        }
        public static void a_godMode(int playerNum)
        {
            Debug.Log("CheatFunc: God Switch");
            if (HeroController.Instance == null) { return; }
            bool isGod = HeroController.players[playerNum].character.invulnerable;
            if (isGod)
            {
                HeroController.players[playerNum].character.invulnerable = false;
                Debug.Log("CheatFunc: God off");
            }
            else
            {
                HeroController.players[playerNum].character.invulnerable = true;
                Debug.Log("CheatFunc: God on");
            }
        }
        public static void a_infAmmo()
        {
            Debug.Log("CheatFunc: infinite Ammo Switch");
            if (HeroController.Instance == null) { return; }
            if (infiniteAmmo)
            {
                infiniteAmmo = false;
                Debug.Log("CheatFunc: infinite Ammo off");
            }
            else
            {
                infiniteAmmo = true;
                Debug.Log("CheatFunc: infinite Ammo on");
            }
        }

        // [HarmonyPrefix, HarmonyPatch(typeof(BroBase), "SpecialAmmo", MethodType.Setter)]
        [HarmonyPrefix, HarmonyPatch(typeof(BroBase), nameof(BroBase.SpecialAmmo), MethodType.Setter)]
        public static void SpecialAmmoPrefix(ref int value)
        {
            if (infiniteAmmo)
            {
                if (value < sAmmoMin) { value = sAmmoMax; }
            }
        }
    }
}

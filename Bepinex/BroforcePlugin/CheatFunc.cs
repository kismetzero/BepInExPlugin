using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace BroforcePlugin
{
    public class CheatFunc
    {
        public static bool infiniteAmmo = false;
        public static int pNum = 0;
        public static HeroType hType = HeroType.IndianaBrones;
        public static int sAmmoMin = 2;
        public static int sAmmoMax = 5;

        public static void a_addLife(int playerNum)
        {
            Debug.Log("Add Life");
            HeroController.AddLife(playerNum);
        }
        public static void a_changeBro(int playerNum, HeroType newHeroType)
        {
            Debug.Log("Change Bro");
            HeroController.ChangeBro(playerNum, newHeroType);
        }
        public static void a_addAmmo(int playerNum)
        {
            Debug.Log("Add Ammo");
            var broBase = HeroController.players[playerNum].character as BroBase;
            broBase.SpecialAmmo += 1;
        }
        public static void a_godMode(int playerNum)
        {
            Debug.Log("God Switch");
            bool isGod = HeroController.players[playerNum].character.invulnerable;
            if (isGod)
            {
                HeroController.players[playerNum].character.invulnerable = false;
                Debug.Log("God off");
            }
            else
            {
                HeroController.players[playerNum].character.invulnerable = true;
                Debug.Log("God on");
            }
        }
        public static void a_infAmmo()
        {
            Debug.Log("infinite Ammo Switch");
            if (infiniteAmmo)
            {
                infiniteAmmo = false;
                Debug.Log("infinite Ammo off");
            }
            else
            {
                infiniteAmmo = true;
                Debug.Log("infinite Ammo on");
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

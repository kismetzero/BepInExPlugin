using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace BroforcePlugin.PatchPack
{
    class PlayerPatch
    {
        public static Player Pinstance;

        [HarmonyPrefix, HarmonyPatch(typeof(Player), "Awake")]
        public static void getPinstance(Player __instance)
        {
            Pinstance = __instance;
            Debug.Log("Player的实例获取方法已调用");
        }

        //移除生命
        [HarmonyPrefix, HarmonyPatch(typeof(Player), "RemoveLife")]
        public static bool RemoveLifePrefix(Player __instance)
        {
            if (Pinstance != __instance)
            {
                Pinstance = __instance;
            }

            if (FlagControl.LockLive.value) { return false; }
            else { return true; }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Player), "SpawnHero")]
        public static void SpawnHeroPrefix(ref HeroType nextHeroType)
        {
            Debug.Log(nextHeroType);
            switch (PluginTest.PTinstance.HeroINT)
            {
                case 0:
                    break;
                case 1:
                    nextHeroType = HeroType.AshBrolliams;
                    break;
                case 2:
                    nextHeroType = HeroType.TimeBroVanDamme;
                    break;
                case 3:
                    nextHeroType = HeroType.EllenRipbro;
                    break;
                case 4:
                    nextHeroType = HeroType.Brobocop;
                    break;
                case 5:
                    nextHeroType = HeroType.TheBrode;
                    break;
                case 6:
                    nextHeroType = HeroType.CherryBroling;
                    break;
                case 7:
                    nextHeroType = HeroType.BroHard;
                    break;
                case 8:
                    nextHeroType = HeroType.BronanTheBrobarian;
                    break;
                case 9:
                    nextHeroType = HeroType.Predabro;
                    break;
                case 10:
                    nextHeroType = HeroType.TheBrofessional;
                    break;
                case 11:
                    nextHeroType = HeroType.BroDredd;
                    break;
                case 12:
                    nextHeroType = HeroType.BrodellWalker;
                    break;
                case 13:
                    nextHeroType = HeroType.TheBrocketeer;
                    break;
                case 14:
                    nextHeroType = HeroType.BroMax;
                    break;
                case 15:
                    nextHeroType = HeroType.CherryBroling;
                    break;
                case 16:
                    nextHeroType = HeroType.BroveHeart;
                    break;
                case 17:
                    nextHeroType = HeroType.Brochete;
                    break;
                case 18:
                    nextHeroType = HeroType.DoubleBroSeven;
                    break;
                case 19:
                    nextHeroType = HeroType.Blade;
                    break;
                case 20:
                    nextHeroType = HeroType.Broden;
                    break;
                case 21:
                    nextHeroType = HeroType.Brommando;
                    break;
                case 22:
                    nextHeroType = HeroType.BaBroracus;
                    break;
                case 23:
                    nextHeroType = HeroType.McBrover;
                    break;
                case 24:
                    nextHeroType = HeroType.Brononymous;
                    break;
                case 25:
                    nextHeroType = HeroType.MadMaxBrotansky;//nonull
                    break;
                case 26:
                    nextHeroType = HeroType.SnakeBroSkin;
                    break;
                case 27:
                    nextHeroType = HeroType.Brominator;
                    break;
                case 28:
                    nextHeroType = HeroType.IndianaBrones;
                    break;
                case 29:
                    nextHeroType = HeroType.Nebro;
                    break;
                case 30:
                    nextHeroType = HeroType.BoondockBros;
                    break;
                case 31:
                    nextHeroType = HeroType.ColJamesBroddock;
                    break;
                case 32:
                    nextHeroType = HeroType.BroniversalSoldier;
                    break;
                case 33:
                    nextHeroType = HeroType.BroneyRoss;
                    break;
                case 34:
                    nextHeroType = HeroType.LeeBroxmas;
                    break;
                case 35:
                    nextHeroType = HeroType.BronnarJensen;
                    break;
                case 36:
                    nextHeroType = HeroType.HaleTheBro;
                    break;
                case 37:
                    nextHeroType = HeroType.TrentBroser;
                    break;
                case 38:
                    nextHeroType = HeroType.Broc;
                    break;
                case 39:
                    nextHeroType = HeroType.TollBroad;
                    break;
                case 40:
                    nextHeroType = HeroType.BrondleFly;
                    break;
                case 41:
                    nextHeroType = HeroType.TankBro;
                    break;
                case 42:
                    nextHeroType = HeroType.TheBrolander;
                    break;
                case 43:
                    nextHeroType = HeroType.DirtyHarry;
                    break;
                case 44:
                    nextHeroType = HeroType.BroLee;
                    break;
                case 45:
                    nextHeroType = HeroType.Rambro;
                    break;
            }
            
            Debug.Log(nextHeroType);

        }
    }
}

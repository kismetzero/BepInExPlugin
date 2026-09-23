
using HarmonyLib;
using System;

namespace StreetsOfRoguePlugin
{
    public class CheatFunc
    {
        public enum GodModeType { Off, ZeroDamage, SkipDamage, Invincible }
        public static GodModeType A_GodMode { get; set; } = GodModeType.Off;
        public static GameController C_GC => GameController.gameController;
        public static Unlocks C_Unlocks
        {
            get
            {
                GameController gc = C_GC;
                if (gc == null) { return null; }
                return gc.unlocks;
            }
        }
        public static Agent C_Player
        {
            get
            {
                GameController gc = C_GC;
                if (gc == null) { return null; }
                return gc.playerAgent;
            }
        }

        public static void A_AddMoney(int num)
        {
            Agent player = C_Player;
            if (player == null) { return; }
            InvItem invItem = new InvItem();
            invItem.invItemName = "Money";
            invItem.invItemCount = num;
            invItem.ItemSetup(true);
            player.inventory.AddItem(invItem);
        }

        [HarmonyPrefix, HarmonyPatch(typeof(StatusEffects), nameof(StatusEffects.ChangeHealth),
            new Type[]
            {
                typeof(float),
                typeof(PlayfieldObject),
                typeof(uint),
                typeof(float),
                typeof(string),
                typeof(byte)
            }
            )]
        public static bool StatusEffects_ChangeHealth_Prefix(StatusEffects __instance, ref float healthNum, ref float clientFinalHealthNum)
        {
            if (A_GodMode != GodModeType.Off && __instance.agent != null && __instance.agent.localPlayer)
            {
                if (A_GodMode == GodModeType.SkipDamage) { return false; }
                if (A_GodMode == GodModeType.ZeroDamage)
                {
                    // if (healthNum == -200f || healthNum == -2000f) { return true; }
                    if (healthNum < 0f) { healthNum = 0f; }
                    if (clientFinalHealthNum < 0f && clientFinalHealthNum != -999f) { clientFinalHealthNum = 0f; }
                }
            }
            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(StatusEffects), nameof(StatusEffects.hasStatusEffect))]
        public static bool StatusEffects_hasStatusEffect_Prefix(StatusEffects __instance, string statusEffectName, ref bool __result)
        {
            if (A_GodMode == GodModeType.Invincible && __instance.agent != null && __instance.agent.localPlayer)
            {
                if (statusEffectName == "Invincible")
                {
                    __result = true;
                    return false;
                }
            }
            return true;
        }
    }
}

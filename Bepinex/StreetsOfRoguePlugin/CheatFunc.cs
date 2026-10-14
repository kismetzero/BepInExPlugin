
using HarmonyLib;
using System;

namespace StreetsOfRoguePlugin
{
    public class CheatFunc
    {
        public static GameController C_GC => GameController.gameController;
        public static Unlocks C_Unlocks => (C_GC != null) ? C_GC.unlocks : null;
        public static InvDatabase C_inventory => (C_Player != null) ? C_Player.inventory : null;

        public static Agent C_Player => (C_GC != null) ? C_GC.playerAgent : null;
        public static float A_health
        {
            get => (C_Player != null) ? C_Player.health : 0f;
            set { if (C_Player != null) { C_Player.health = value; } }
        }
        public static float A_healthMax
        {
            get => (C_Player != null) ? C_Player.healthMax : 0f;
            set { if (C_Player != null) { C_Player.healthMax = value; } }
        }
        public static bool A_invisible
        {
            get => (C_Player != null) ? C_Player.invisible : false;
            set { if (C_Player != null) { C_Player.invisible = value; } }
        }

        public static bool A_aboveTheLaw
        {
            get => (C_Player != null) ? C_Player.aboveTheLaw : false;
            set { if (C_Player != null) { C_Player.aboveTheLaw = value; } }
        }

        public enum GodModeType { Off, ZeroDamage, SkipDamage, Invincible }
        public static GodModeType A_GodMode { get; set; } = GodModeType.Off;

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

        public static void A_AddItem(string name)
        {
            Agent player = C_Player;
            if (player == null) { return; }
            InvItem invItem = new InvItem();
            invItem.invItemName = name;
            invItem.ItemSetup(true);
            invItem.invItemCount = invItem.rewardCount;
            player.inventory.AddItem(invItem);
        }

        public static void A_AddTrait(string name)
        {
            if (C_Player == null) { return; }
            C_Player.statusEffects.AddTrait(name);
        }

        public static void A_AddStatusEffect(string name)
        {
            if (C_Player == null) { return; }
            C_Player.statusEffects.AddStatusEffect(name);
        }
    }
}

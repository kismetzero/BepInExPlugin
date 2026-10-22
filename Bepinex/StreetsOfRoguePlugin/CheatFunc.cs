
using HarmonyLib;
using System;

namespace StreetsOfRoguePlugin
{
    public class CheatFunc
    {
        public static GameController GC => GameController.gameController;

        public static Agent Player => (GC != null) ? GC.playerAgent : default;
        public static float Health
        {
            get => (Player != null) ? Player.health : default;
            set { if (Player != null) { Player.health = value; } }
        }
        public static float HealthMax
        {
            get => (Player != null) ? Player.healthMax : default;
            set { if (Player != null) { Player.healthMax = value; } }
        }
        public static bool Invisible
        {
            get => (Player != null) ? Player.invisible : default;
            set { if (Player != null) { Player.invisible = value; } }
        }

        public static bool AboveTheLaw
        {
            get => (Player != null) ? Player.aboveTheLaw : default;
            set { if (Player != null) { Player.aboveTheLaw = value; } }
        }

        public enum GodModeType { Off, ZeroDamage, SkipDamage, Invincible }
        public static GodModeType GodMode { get; set; } = GodModeType.Off;

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
            if (GodMode != GodModeType.Off && __instance.agent != null && __instance.agent.localPlayer)
            {
                if (GodMode == GodModeType.SkipDamage) { return false; }
                if (GodMode == GodModeType.ZeroDamage)
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
            if (GodMode == GodModeType.Invincible && __instance.agent != null && __instance.agent.localPlayer)
            {
                if (statusEffectName == "Invincible")
                {
                    __result = true;
                    return false;
                }
            }
            return true;
        }

        public static void AddMoney(int num)
        {
            Agent player = Player;
            if (player == null) { return; }
            InvItem invItem = new InvItem();
            invItem.invItemName = "Money";
            invItem.invItemCount = num;
            invItem.ItemSetup(true);
            player.inventory.AddItem(invItem);
        }

        public static void AddItem(string name)
        {
            Agent player = Player;
            if (player == null) { return; }
            InvItem invItem = new InvItem();
            invItem.invItemName = name;
            invItem.ItemSetup(true);
            invItem.invItemCount = invItem.rewardCount;
            player.inventory.AddItem(invItem);
        }

        public static void AddTrait(string name)
        {
            if (Player == null) { return; }
            Player.statusEffects.AddTrait(name);
        }

        public static void AddStatusEffect(string name)
        {
            if (Player == null) { return; }
            Player.statusEffects.AddStatusEffect(name);
        }
    }
}

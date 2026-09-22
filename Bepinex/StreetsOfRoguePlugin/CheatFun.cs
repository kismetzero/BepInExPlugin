
using HarmonyLib;
using System;

namespace StreetsOfRoguePlugin
{
    public class CheatFun
    {
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
        public static void StatusEffects_ChangeHealth_Prefix( StatusEffects __instance,
            ref float healthNum,
            PlayfieldObject damagerObject,
            uint cameFromClient,
            float clientFinalHealthNum,
            string damagerObjectName,
            byte extraVar )
        {

        }
    }
}

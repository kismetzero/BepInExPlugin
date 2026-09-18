using HarmonyLib;

namespace EnterTheGungeonPlugin
{
    public class CheatFunc
    {
        public static GameManager GM => GameManager.Instance;
        public static PlayerController PlayerCtrl => GameManager.Instance?.PrimaryPlayer;

        public enum BlankModeType { Off, NoDecrease, Locked }
        public static BlankModeType BlankMode = BlankModeType.Off;

        public static PunchoutController PunchCtrl;
        public enum PunchGodModeType { Off, ZeroDamage, SkipHit }
        public static PunchGodModeType PunchGodMode = PunchGodModeType.Off;
        public static bool PunchNoFat = false;

        public static void A_AddCurrency(int value = 10)
        {
            PlayerController player = PlayerCtrl;
            if (player == null) { return; }
            PlayerConsumables cc = player.carriedConsumables;
            if (cc == null) { return; }
            cc.Currency += value;
        }
        public static void A_AddKey(int value = 1)
        {
            PlayerController player = PlayerCtrl;
            if (player == null) { return; }
            PlayerConsumables cc = player.carriedConsumables;
            if (cc == null) { return; }
            cc.KeyBullets += value;
        }
        public static void A_AddRatKeys(int value = 1)
        {
            PlayerController player = PlayerCtrl;
            if (player == null) { return; }
            PlayerConsumables cc = player.carriedConsumables;
            if (cc == null) { return; }
            cc.ResourcefulRatKeys += value;
        }
        public static void A_AddBlank(int value = 1)
        {
            PlayerController player = PlayerCtrl;
            if (player == null) { return; }
            player.Blanks += value;
        }
        public static void A_FullHeal()
        {
            PlayerController player = PlayerCtrl;
            if (player == null) { return; }
            HealthHaver hh = player.healthHaver;
            if (hh == null) { return; }
            hh.FullHeal();
        }
        public static void A_GodMode()
        {
            PlayerController player = PlayerCtrl;
            if (player == null) { return; }
            HealthHaver hh = player.healthHaver;
            if (hh == null) { return; }
            hh.IsVulnerable = !hh.IsVulnerable;
        }
        public static void A_SetPunchTime(float time)
        {
            if (PunchCtrl != null) { PunchCtrl.Timer = time; }
        }
        public static void A_SetPunchFat(float fat)
        {
            PunchoutPlayerController ppc = PunchCtrl?.Player;
            if (ppc != null) { ppc.CurrentExhaust = fat; }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Blanks), MethodType.Setter)]
        public static bool PlayerController_Blanks_Setter_Prefix(PlayerController __instance, ref int value)
        {
            if (BlankMode == BlankModeType.Off) { return true; }
            int currentBlanks = __instance.Blanks;
            switch (BlankMode)
            {
                case BlankModeType.NoDecrease:
                    if (value < currentBlanks) { value = currentBlanks; }
                    return true;
                case BlankModeType.Locked: return false;
            }
            return true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PunchoutController), nameof(PunchoutController.Init))]
        public static void PunchoutController_Init_Postfix(PunchoutController __instance)
        {
            if (__instance != null) { PunchCtrl = __instance; }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PunchoutController), "OnDestroy")]
        public static void PunchoutController_OnDestroy_Postfix()
        {
            PunchCtrl = null;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(PunchoutPlayerController), nameof(PunchoutPlayerController.Hit))]
        public static bool PunchoutPlayerController_Hit_Prefix(ref float damage)
        {
            switch (PunchGodMode)
            {
                case PunchGodModeType.Off: return true;
                case PunchGodModeType.ZeroDamage:
                    damage = 0f;
                    return true;
                case PunchGodModeType.SkipHit: return false;
            }
            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(PunchoutPlayerController), nameof(PunchoutPlayerController.CurrentExhaust), MethodType.Setter)]
        public static void PunchoutPlayerController_CurrentExhaust_Setter_Prefix(ref float value)
        {
            if (PunchNoFat) { value = 0f; }
        }
    }
}

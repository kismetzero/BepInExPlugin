using HarmonyLib;

namespace EnterTheGungeonPlugin
{
    public class CheatFunc
    {
        public enum BlankModeType { Off, NoDecrease, Locked }
        public static BlankModeType A_BlankMode = BlankModeType.Off;

        public static PunchoutController PunchCtrl;
        public enum PunchGodModeType { Off, ZeroDamage, SkipHit }
        public static PunchGodModeType A_PunchGodMode = PunchGodModeType.Off;
        public static bool A_PunchNoFat = false;

        // public static GameManager C_GM { get { return GameManager.Instance; } }
        public static GameManager C_GM => GameManager.Instance;
        public static PlayerController C_PlayerCtrl
        {
            get
            {
                GameManager gm = C_GM;
                if (gm == null) { return null; }
                return gm.PrimaryPlayer;
            }
        }
        public static PlayerConsumables C_CarriedConsumables
        {
            get
            {
                PlayerController player = C_PlayerCtrl;
                if (player == null) { return null; }
                return player.carriedConsumables;
            }
        }
        public static HealthHaver C_HealthHaver
        {
            get
            {
                PlayerController player = C_PlayerCtrl;
                if (player == null) { return null; }
                return player.healthHaver;
            }
        }
        public static int B_Currency
        {
            get
            {
                PlayerConsumables cc = C_CarriedConsumables;
                if (cc == null) { return 0; }
                return cc.Currency;
            }
            set
            {
                PlayerConsumables cc = C_CarriedConsumables;
                if (cc == null) { return; }
                cc.Currency = value;
            }
        }
        public static int B_KeyBullets
        {
            get
            {
                PlayerConsumables cc = C_CarriedConsumables;
                if (cc == null) { return 0; }
                return cc.KeyBullets;
            }
            set
            {
                PlayerConsumables cc = C_CarriedConsumables;
                if (cc == null) { return; }
                cc.KeyBullets = value;
            }
        }
        public static int B_RatKeys
        {
            get
            {
                PlayerConsumables cc = C_CarriedConsumables;
                if (cc == null) { return 0; }
                return cc.ResourcefulRatKeys;
            }
            set
            {
                PlayerConsumables cc = C_CarriedConsumables;
                if (cc == null) { return; }
                cc.ResourcefulRatKeys = value;
            }
        }
        public static int A_Blanks
        {
            get
            {
                PlayerController player = C_PlayerCtrl;
                if (player == null) { return 0; }
                return player.Blanks;
            }
            set
            {
                PlayerController player = C_PlayerCtrl;
                if (player == null) { return; }
                player.Blanks = value;
            }
        }
        public static void A_FullHeal()
        {
            HealthHaver hh = C_HealthHaver;
            if (hh == null) { return; }
            hh.FullHeal();
        }
        public static bool A_GodMode
        {
            get
            {
                HealthHaver hh = C_HealthHaver;
                if (hh == null) { return false; }
                return !hh.IsVulnerable;
            }
            set
            {
                HealthHaver hh = C_HealthHaver;
                if (hh == null) { return; }
                hh.IsVulnerable = !value;
            }
        }
        public static float A_Armor
        {
            get
            {
                HealthHaver hh = C_HealthHaver;
                if (hh == null) { return 0f; }
                return hh.Armor;
            }
            set
            {
                HealthHaver hh = C_HealthHaver;
                if (hh == null) { return; }
                hh.Armor = value;
            }
        }
        public static float A_PunchTime
        {
            get
            {
                if (PunchCtrl == null) { return 0f; }
                return PunchCtrl.Timer;
            }
            set
            {
                if (PunchCtrl == null) { return; }
                PunchCtrl.Timer = value;
            }
        }
        public static float A_PunchFat
        {
            get
            {
                if (PunchCtrl == null) { return 0f; }
                PunchoutPlayerController ppc = PunchCtrl.Player;
                if (ppc == null) { return 0f; }
                return ppc.CurrentExhaust;
            }
            set
            {
                if (PunchCtrl == null) { return; }
                PunchoutPlayerController ppc = PunchCtrl.Player;
                if (ppc == null) { return; }
                ppc.CurrentExhaust = value;
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Blanks), MethodType.Setter)]
        public static bool PlayerController_Blanks_Setter_Prefix(PlayerController __instance, ref int value)
        {
            if (A_BlankMode == BlankModeType.Off) { return true; }
            int currentBlanks = __instance.Blanks;
            switch (A_BlankMode)
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
            switch (A_PunchGodMode)
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
            if (A_PunchNoFat) { value = 0f; }
        }
    }
}

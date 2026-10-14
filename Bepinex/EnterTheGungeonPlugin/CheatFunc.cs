using HarmonyLib;

namespace EnterTheGungeonPlugin
{
    public class CheatFunc
    {
        public static GameManager C_GM => GameManager.Instance;

        // --------------------
        // 空响弹
        // --------------------
        public static PlayerController C_PlayerCtrl => (C_GM != null) ? C_GM.PrimaryPlayer : null;
        public static int A_Blanks
        {
            get => (C_PlayerCtrl != null) ? C_PlayerCtrl.Blanks : 0;
            set { if (C_PlayerCtrl != null) { C_PlayerCtrl.Blanks = value; } }
        }
        public static bool A_BlanksInf { get; set; } = false;
        public static bool A_BlanksLock { get; set; } = false;

        [HarmonyPrefix, HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Blanks), MethodType.Setter)]
        public static bool PlayerController_Blanks_Setter_Prefix(PlayerController __instance, ref int value)
        {
            if (A_BlanksLock) { return false; }
            if (A_BlanksInf)
            {
                int currentBlanks = __instance.Blanks;
                if (value < currentBlanks) { return false; }
            }
            return true;
        }

        // --------------------
        // HP、护盾
        // --------------------
        public static HealthHaver C_HealthHaver => (C_PlayerCtrl != null) ? C_PlayerCtrl.healthHaver : null;
        public static float A_Health
        {
            get => (C_HealthHaver != null) ? C_HealthHaver.GetCurrentHealth() : 0f;
            set { if (C_HealthHaver != null) { Traverse.Create(C_HealthHaver).Field("currentHealth").SetValue(value); } }
        }
        public static float A_HealthMax
        {
            get => (C_HealthHaver != null) ? C_HealthHaver.GetMaxHealth() : 0f;
            set { if (C_HealthHaver != null) { Traverse.Create(C_HealthHaver).Field("maximumHealth").SetValue(value); } }
        }
        public static float A_Armor
        {
            get => (C_HealthHaver != null) ? C_HealthHaver.Armor : 0f;
            set { if (C_HealthHaver != null) { C_HealthHaver.Armor = value; } }
        }
        public static bool A_GodMode { get; set; } = false;

        [HarmonyPostfix, HarmonyPatch(typeof(HealthHaver), nameof(HealthHaver.IsVulnerable), MethodType.Getter)]
        public static void HealthHaver_IsVulnerable_Getter_Postfix(HealthHaver __instance, ref bool __result)
        {
            if (A_GodMode && __instance.GetComponent<PlayerController>() != null) { __result = false; }
        }

        // --------------------
        // 金币、钥匙
        // --------------------
        public static PlayerConsumables C_CarriedConsumables => (C_PlayerCtrl != null) ? C_PlayerCtrl.carriedConsumables : null;
        public static int B_Currency
        {
            get => (C_CarriedConsumables != null) ? C_CarriedConsumables.Currency : 0;
            set { if (C_CarriedConsumables != null) { C_CarriedConsumables.Currency = value; } }
        }
        public static int B_KeyBullets
        {
            get => (C_CarriedConsumables != null) ? C_CarriedConsumables.KeyBullets : 0;
            set { if (C_CarriedConsumables != null) { C_CarriedConsumables.KeyBullets = value; } }
        }
        public static int B_RatKeys
        {
            get => (C_CarriedConsumables != null) ? C_CarriedConsumables.ResourcefulRatKeys : 0;
            set { if (C_CarriedConsumables != null) { C_CarriedConsumables.ResourcefulRatKeys = value; } }
        }


        public static PunchoutController PunchCtrl { get; set; }

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

        public static float A_PunchTime
        {
            get => (PunchCtrl != null) ? PunchCtrl.Timer : 0f;
            set { if (PunchCtrl != null) { PunchCtrl.Timer = value; } }
        }
        public static bool A_PunchTimerFreeze { get; set; } = false;

        [HarmonyPrefix, HarmonyPatch(typeof(PunchoutController), nameof(PunchoutController.Timer), MethodType.Setter)]
        public static bool PunchoutController_Timer_Setter_Prefix(PunchoutController __instance, ref float value)
        {
            if (A_PunchTimerFreeze)
            {
                float currentTimer = __instance.Timer;
                if (value < currentTimer) { return false; }
            }
            return true;
        }

        public static PunchoutPlayerController C_PunchPlayer => (PunchCtrl != null) ? PunchCtrl.Player : null;
        public static float A_PunchHealth
        {
            get => (C_PunchPlayer != null) ? C_PunchPlayer.Health : 0f;
            set { if (C_PunchPlayer != null) { C_PunchPlayer.Health = value; } }
        }
        public enum PunchGodModeType { Off, ZeroDamage, SkipHit }
        public static PunchGodModeType A_PunchGodMode { get; set; } = PunchGodModeType.Off;

        [HarmonyPrefix, HarmonyPatch(typeof(PunchoutPlayerController), nameof(PunchoutPlayerController.Hit))]
        public static bool PunchoutPlayerController_Hit_Prefix(ref float damage)
        {
            if (A_PunchGodMode == PunchGodModeType.SkipHit) { return false; }
            if (A_PunchGodMode == PunchGodModeType.ZeroDamage) { damage = 0f; }
            return true;
        }

        public static float A_PunchFat
        {
            get => (C_PunchPlayer != null) ? C_PunchPlayer.CurrentExhaust : 0f;
            set { if (C_PunchPlayer != null) { C_PunchPlayer.CurrentExhaust = value; } }
        }
        public static bool A_PunchNoFat { get; set; } = false;

        [HarmonyPrefix, HarmonyPatch(typeof(PunchoutPlayerController), nameof(PunchoutPlayerController.CurrentExhaust), MethodType.Setter)]
        public static void PunchoutPlayerController_CurrentExhaust_Setter_Prefix(ref float value)
        {
            if (A_PunchNoFat) { value = 0f; }
        }

        public static int A_PunchStars
        {
            get => (C_PunchPlayer != null) ? C_PunchPlayer.Stars : 0;
            set { if (C_PunchPlayer != null) { C_PunchPlayer.Stars = value; } }
        }
    }
}

using HarmonyLib;

namespace EnterTheGungeonPlugin
{
    public class CheatFunc
    {
        public static GameManager GM => GameManager.Instance;

        // --------------------
        // 空响弹
        // --------------------
        public static PlayerController PlayerCtrl => (GM != null) ? GM.PrimaryPlayer : default;
        public static int Blanks
        {
            get => (PlayerCtrl != null) ? PlayerCtrl.Blanks : default;
            set { if (PlayerCtrl != null) { PlayerCtrl.Blanks = value; } }
        }
        public static bool BlanksInf { get; set; } = false;
        public static bool BlanksLock { get; set; } = false;

        [HarmonyPrefix, HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Blanks), MethodType.Setter)]
        public static bool PlayerController_Blanks_Setter_Prefix(PlayerController __instance, ref int value)
        {
            if (BlanksLock) { return false; }
            if (BlanksInf)
            {
                int currentBlanks = __instance.Blanks;
                if (value < currentBlanks) { return false; }
            }
            return true;
        }

        // --------------------
        // HP、护盾
        // --------------------
        public static HealthHaver HH => (PlayerCtrl != null) ? PlayerCtrl.healthHaver : default;
        public static float Health
        {
            get => (HH != null) ? HH.GetCurrentHealth() : default;
            set { if (HH != null) { Traverse.Create(HH).Field("currentHealth").SetValue(value); } }
        }
        public static float HealthMax
        {
            get => (HH != null) ? HH.GetMaxHealth() : default;
            set { if (HH != null) { Traverse.Create(HH).Field("maximumHealth").SetValue(value); } }
        }
        public static float Armor
        {
            get => (HH != null) ? HH.Armor : default;
            set { if (HH != null) { HH.Armor = value; } }
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
        public static PlayerConsumables PlayerConsumables => (PlayerCtrl != null) ? PlayerCtrl.carriedConsumables : default;
        public static int Coin
        {
            get => (PlayerConsumables != null) ? PlayerConsumables.Currency : default;
            set { if (PlayerConsumables != null) { PlayerConsumables.Currency = value; } }
        }
        public static int Keys
        {
            get => (PlayerConsumables != null) ? PlayerConsumables.KeyBullets : default;
            set { if (PlayerConsumables != null) { PlayerConsumables.KeyBullets = value; } }
        }
        public static int RatResKeys
        {
            get => (PlayerConsumables != null) ? PlayerConsumables.ResourcefulRatKeys : default;
            set { if (PlayerConsumables != null) { PlayerConsumables.ResourcefulRatKeys = value; } }
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

        public static float PunchTime
        {
            get => (PunchCtrl != null) ? PunchCtrl.Timer : default;
            set { if (PunchCtrl != null) { PunchCtrl.Timer = value; } }
        }
        public static bool PunchTimerFreeze { get; set; } = false;

        [HarmonyPrefix, HarmonyPatch(typeof(PunchoutController), nameof(PunchoutController.Timer), MethodType.Setter)]
        public static bool PunchoutController_Timer_Setter_Prefix(PunchoutController __instance, ref float value)
        {
            if (PunchTimerFreeze)
            {
                float currentTimer = __instance.Timer;
                if (value < currentTimer) { return false; }
            }
            return true;
        }

        public static PunchoutPlayerController PunchPlayer => (PunchCtrl != null) ? PunchCtrl.Player : null;
        public static float PunchHealth
        {
            get => (PunchPlayer != null) ? PunchPlayer.Health : 0f;
            set { if (PunchPlayer != null) { PunchPlayer.Health = value; } }
        }
        public enum PunchGodModeType { Off, ZeroDamage, SkipHit }
        public static PunchGodModeType PunchGodMode { get; set; } = PunchGodModeType.Off;

        [HarmonyPrefix, HarmonyPatch(typeof(PunchoutPlayerController), nameof(PunchoutPlayerController.Hit))]
        public static bool PunchoutPlayerController_Hit_Prefix(ref float damage)
        {
            if (PunchGodMode == PunchGodModeType.SkipHit) { return false; }
            if (PunchGodMode == PunchGodModeType.ZeroDamage) { damage = 0f; }
            return true;
        }

        public static float PunchFat
        {
            get => (PunchPlayer != null) ? PunchPlayer.CurrentExhaust : default;
            set { if (PunchPlayer != null) { PunchPlayer.CurrentExhaust = value; } }
        }
        public static bool PunchNoFat { get; set; } = false;

        [HarmonyPrefix, HarmonyPatch(typeof(PunchoutPlayerController), nameof(PunchoutPlayerController.CurrentExhaust), MethodType.Setter)]
        public static void PunchoutPlayerController_CurrentExhaust_Setter_Prefix(ref float value)
        {
            if (PunchNoFat) { value = 0f; }
        }

        public static int PunchStars
        {
            get => (PunchPlayer != null) ? PunchPlayer.Stars : default;
            set { if (PunchPlayer != null) { PunchPlayer.Stars = value; } }
        }
    }
}

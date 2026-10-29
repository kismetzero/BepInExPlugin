

namespace RwbyGePlugin
{
    public class CheatFunc
    {
        public static ApplicationManager AM => Roost.Singleton_MonoBehaviour<ApplicationManager>.Instance;
        public static PlayerProfile Profile => (AM != null) ? AM.Profile : null;
        public static GameManager GM => Roost.Singleton_MonoBehaviour<GameManager>.Instance;
        public static GameMode Mode => (GM != null) ? GM.Mode : null;
        public static PlayerCharacter PC => (GM != null) ? GM.LocalPlayer : null;
        public static PlayerData Data => (PC != null) ? PC.Data : null;

        public static int Health
        {
            get => (PC != null) ? PC.Stats.m_stats.curHealth : default;
            set { if (PC != null) { PC.Stats.m_stats.curHealth = value; } }
        }
        public static int HealthMax
        {
            get => (PC != null) ? PC.Stats.m_stats.maxHealth : default;
            set { if (PC != null) { PC.Stats.m_stats.maxHealth = value; } }
        }
        public static int Shields
        {
            get => (PC != null) ? PC.Stats.m_stats.currentShield : default;
            set { if (PC != null) { PC.Stats.m_stats.currentShield = value; } }
        }
        public static int ShieldsMax
        {
            get => (PC != null) ? PC.Stats.m_stats.maxShield : default;
            set { if (PC != null) { PC.Stats.m_stats.maxShield = value; } }
        }
        public static int Special
        {
            get => (PC != null) ? PC.Stats.m_stats.curSpecial : default;
            set { if (PC != null) { PC.Stats.m_stats.curSpecial = value; } }
        }
        public static int SpecialMax
        {
            get => (PC != null) ? PC.Stats.m_stats.maxSpecial : default;
            set { if (PC != null) { PC.Stats.m_stats.maxSpecial = value; } }
        }
        public static int Experience
        {
            get => (Data != null) ? Data.Experience : default;
            set { if (Data != null) { Data.Experience = value; } }
        }
        public static int SkillPoints
        {
            get => (Data != null) ? Data.SkillPoints : default;
            set { if (Data != null) { Data.SkillPoints = value; } }
        }

        public static void AddExp(int exp)
        {
            if (Mode != null && Data != null) { Mode.AwardExperience(Data, exp); }
        }
    }
}

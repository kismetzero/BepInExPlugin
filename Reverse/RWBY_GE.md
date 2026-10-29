## RWBY: 戮兽之蚀 RWBY: Grimm Eclipse

### 实现
```csharp

Singleton_MonoBehaviour<GameManager>.Instance.LocalPlayer;

```


### 参考
~. 
```csharp
public class CharacterStats : EntityComponent<BaseEntity> {
    public int CurrentSpecial { get { return this.m_stats.curSpecial; } }
    public int MaxSpecial { get { return this.m_stats.maxSpecial; } }
    public float CurrentHealth { get { return (float)this.m_stats.curHealth; } }
    public float MaxHealth { get { return (float)this.m_stats.maxHealth; } }
    public int MaxShield { get { return this.m_stats.maxShield; } }
    public CharacterStats.Stats m_stats = new CharacterStats.Stats();
    public class Stats {
        public string charName = string.Empty;
        public int curSpecial = 4;
        public int maxSpecial = 4;
        public int curHealth = 250;
        public int maxHealth = 250;
        public int currentShield;
        public int maxShield;
    }
}

public class BaseEntity : Photon.MonoBehaviour {
    public CharacterStats Stats { get { return this.m_statsScript; } }
    protected CharacterStats m_statsScript;
    // base -> UnityEngine.Component
    protected virtual void Awake() { this.m_statsScript = base.GetComponent<CharacterStats>(); }
}

public class Photon.MonoBehaviour : MonoBehaviour {}

public class Ruby : PlayerCharacter {}

public abstract class PlayerCharacter : BaseCharacter {
    public PlayerData Data { get; }
}

public abstract class BaseCharacter : BaseEntity {}

public sealed class GameManager : Singleton_MonoBehaviour<GameManager> {
    public PlayerCharacter LocalPlayer { get; private set; }
    public GameData GameData { get { return Singleton_MonoBehaviour<ApplicationManager>.Instance.Data; } }
    public GameState State { get { return Singleton_MonoBehaviour<ApplicationManager>.Instance.State; } }
    public GameMode Mode { get { return Singleton_MonoBehaviour<ApplicationManager>.Instance.Mode as GameMode; } }
    public GameState State { get { return Singleton_MonoBehaviour<ApplicationManager>.Instance.State; } }
}

public class ApplicationManager : Singleton_MonoBehaviour<ApplicationManager> {
    public GameState State { get { return this.m_state; } }
    public ApplicationMode Mode { get { return this.m_mode; } }
    public GameData Data { get { return this.m_gameData; } }
    public GameplayDatabase GameplayDatabase { get { return this.m_gameplayDatabase; } }
    public PlayerProfile Profile { get { return this.m_playerProfile; } }
}

public static class GlobalSettings {
    public static bool GodMode = false;
    public static bool IgnorePrestigeRequirements = false;
}

public class GameMode : ApplicationMode {
    public void AwardExperience(PlayerData playerData, int experienceReward);
}

public class GameData {
    public PlayerData GetPlayerData(PlayerCharacter playerCharacter);
    public PlayerData GetPlayerDataFromPhotonPlayer(PhotonPlayer photonPlayer);
    public PlayerData GetPlayerDataFromCharacterPhotonViewID(int characterPhotonViewID) { return this.Players.FirstOrDefault((PlayerData player) => player.CharacterPhotonViewID == characterPhotonViewID); }
    public PlayerData GetPlayerDataFromPhotonPlayerID(int photonPlayerID) { return this.Players.FirstOrDefault((PlayerData player) => player.PhotonPlayerID == photonPlayerID); }
    public PlayerData GetLocalPlayerData() { return this.GetPlayerDataFromPhotonPlayer(PhotonNetwork.player); }
}

public class PlayerData {
    public int Experience;
    public int SkillPoints;
    public int TotalMoney;
}

public static class PhotonNetwork {
    public static PhotonPlayer player { get; }
    public static PhotonPlayer[] playerList { get; }
}

public class PlayerProfile {
    public bool DidPlayerUnlockAchievement(Achievement achievement);
}

```

Roost
~. 
```csharp
public class Singleton_MonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
    public static T Instance { get { return Singleton_MonoBehaviour<T>.s_instance; } }
    private static T s_instance;
}
```

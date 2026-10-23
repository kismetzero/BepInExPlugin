## 霓虹深渊 Neon Abyss

### 实现
```csharp
// 移除控制台惩罚
NEON.Framework.InGameConsole.ProcessCommand();
Services.GameState.IsNoAchievement = true;  // 改成 false
neonplayerState.HasUseConsole = true;       // 改成 false

BossCoins       // 信仰宝石
FinalCoins      // 
AbyssCoins      // 深渊水晶
ExchangeCoins   // 

```

### 参考
~.
```csharp
public static class Services {
    public static XxxService XxxService
	{
		get
		{
			if (Services._XxxService == null)
			{
				Services._XxxService = Global.GetService<XxxService>(false);
			}
			return Services._XxxService;
		}
	}
    public static GameState GameState { get; }
    public static InGameConsole InGameConsole { get; }
    public static SaveDataService SaveDataService
    public static UnlockService UnlockService { get; }
}
```

NEON.Framework
```csharp
public class Attri {
    public FixedValueDict FixedValues = new FixedValueDict();
    public StringString Formulas = new StringString();
    public StringRange ClampList = new StringRange();
}
public class FixedValueDict : Dict<string, double> {}
public class StringString : Dict<string, string> {}
public class StringRange : Dict<string, Vector2> {}

public abstract class GameMode : MonoBehaviour {
    public PlayerState playerState { get; private set; }
}

public class GameState : PersistentService {
    public bool IsNoAchievement { get; set; }
}

public class Global : MonoBehaviour {
    public static T GetGameMode<T>() where T : GameMode;
    public static Global Instance { get; }
    public static GameState Gamestate { get; }
    public static PlayerState PlayerState { get { return Global.GetPlayerState<PlayerState>(); } }
    public static T GetPlayerState<T>() where T : PlayerState;
    public static T GetService<T>(bool noCreate = false) where T : Service;
}

public class InGameConsole : PersistentService {
    private void ProcessCommand();
}

public class PlayerState : Service {
    public Attri attrs = new Attri();
}

public struct PlayerStateInt { }
```

NEON.Game
```csharp
public class Hearts : Killable {
    public int currentArmor { get; set; }
    public int currentHearts { get; set; }
    private NEONPlayerState ps { get { return (Global.PlayerState as NEONPlayerState) ?? UnityEngine.Object.FindObjectOfType<NEONPlayerState>(); } }
    private PlayerActor player { get { return Global.GetGameMode<ControlPlayerMode>().Player; } }
    public int maxHearts { get; set; }
    public int maxArmor { get; set; }
    protected override bool DeductHP(DamageInfo damageInfo, int DamageCountThisFrame);
    public bool isInvincible;
    public bool UnKillable;
}

public abstract class Killable : MonoBehaviour {
    public bool TakeDamage(DamageInfo damage);
    protected abstract bool DeductHP(DamageInfo damageInfo, int damagedCount);
    public bool isGodLike;
}
```

NEON.Game.Actors
```csharp
public class PlayerActor : Actor, IExternalForceHandler, IEqualityComparer {
    private NEONPlayerState ps { get { return Global.PlayerState as NEONPlayerState; } }
    public bool canJump { get { return this.isOnGround; } }
    public override bool isOnGround { get; set; }
    public Hearts _hearts;
}
```

NEON.Game.GameModes
```csharp
public class BattleMode : ControlPlayerMode, ILoading {
    public void SaveInGameProgress(bool incrementLevel = false);
}

public abstract class ControlPlayerMode : GameMode {
    public PlayerActor Player;
}
```

NEON.Game.Managers
```csharp
public class NEONPlayerState : PlayerState {
    public bool HasUseConsole { get; set; }

    public void AddKey(int amount);
    public void AddCoin(int amount);
    public void AddMana(int value);
    public void AddFullMana() { this.AddMana(this.maxManas); }
    public void AddShield(int value);
    public void AddHeart(int value);

    private PlayerStateInt Coin = "Coin";
    private PlayerStateInt maxCoin = "MaxCoin";
    private PlayerStateInt currentManas = "CurrentMana";
    private PlayerStateInt maxManas = "MaxMana";
    private PlayerStateInt currentHearts = "CurrentHearts";
    private PlayerStateInt maxHearts = "MaxHearts";
    private PlayerStateInt currentShields = "CurrentShields";
    private PlayerStateInt maxShields = "MaxShields";
    private PlayerStateInt maxBombCount = "MaxBomb";

    private PlayerStateInt InfiniteKey = "InfinitKey";
    private PlayerStateInt Key = "Key";
    private PlayerStateInt maxKey = "MaxKey";

    private PlayerStateInt DefaultBomb = "DefaultBomb";
    private PlayerStateInt FireBomb = "FireBomb";
    private PlayerStateInt IceBomb = "IceBomb";
    private PlayerStateInt ThunderBomb = "ThunderBomb";
    private PlayerStateInt AlcoholBomb = "Liquor_Bomb";

    public void AddBossCoinMax() { Services.UnlockService.IncBossCoins(999); }
    public void AddFinalCoinMax() { Services.UnlockService.IncFinalCoins(999); }
}
```

NEON.Framework.SaveData
```csharp
public class SaveDataService : SaveDataServiceBase {
    public SaveData Data;
}

public class SaveData {
    public UnlockSystemData UnlockSystemData = new UnlockSystemData();
}
```

NEON.UI.UnlockSystem.Data
```csharp
public class UnlockSystemData {
    public int BossCoins;
    public int FinalCoins;
    public int AbyssCoins;
    public int ExchangeCoins;
}
```

NEON.UI.UnlockSystem.Service
```csharp
public class UnlockService : UnlockServiceBase {
    public int IncBossCoins(int coins);
    public int IncFinalCoins(int coins);
    public int IncAbyssCoins(int coins);
    public int IncExchangeCoins(int coins);
}

public abstract class UnlockServiceBase : PersistentService {
    protected UnlockSystemData Data { get { return Services.SaveDataService.Data.UnlockSystemData; } }
}
```

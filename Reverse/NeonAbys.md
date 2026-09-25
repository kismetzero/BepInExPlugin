## 霓虹深渊 Neon Abyss

### 实现
```csharp
// 移除控制台惩罚
NEON.Framework.InGameConsole.ProcessCommand();
Services.GameState.IsNoAchievement = true;
neonplayerState.HasUseConsole = true; 

```

### 参考
~.
```csharp
public static class Services {
    public static XxxService XxxService
	{
		get
		{
			if (Services.XxxService == null)
			{
				Services.XxxService = Global.GetService<XxxService>(false);
			}
			return Services.XxxService;
		}
	}
    public static GameState GameState { get; }
    public static InGameConsole InGameConsole { get; }
    public static SaveDataService SaveDataService
    public static UnlockService UnlockService { get; }
}
```

NEON.Game.Managers
```csharp
public class NEONPlayerState : PlayerState {
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

NEON.Framework
```csharp
public struct PlayerStateInt { }
public class InGameConsole : PersistentService {
    private void ProcessCommand();
}

public class GameState : PersistentService {
    public bool IsNoAchievement { get; set; }
}
```

NEON.UI.UnlockSystem.Service
```csharp
public class UnlockService : UnlockServiceBase {
    public int IncAbyssCoins(int coins)
    {
        base.Data.AbyssCoins += coins;
        base.Data.AbyssCoins = Mathf.Max(0, base.Data.AbyssCoins);
        this._anyUnlockableCacheDirty = true;
        return base.Data.AbyssCoins;
    }
    public int IncBossCoins(int coins)
    {
        base.Data.BossCoins += coins;
        if (coins > 0)
        {
            Global.GetService<AchievementService>(false).AchievementBossCoinGet(coins);
        }
        base.Data.BossCoins = Mathf.Max(0, base.Data.BossCoins);
        this._anyUnlockableCacheDirty = true;
        return base.Data.BossCoins;
    }
    public int IncExchangeCoins(int coins)
    {
        base.Data.ExchangeCoins += coins;
        base.Data.ExchangeCoins = Mathf.Max(0, base.Data.ExchangeCoins);
        this._anyUnlockableCacheDirty = true;
        return base.Data.ExchangeCoins;
    }
    public int IncFinalCoins(int coins)
    {
        base.Data.FinalCoins += coins;
        if (coins > 0)
        {
            Global.GetService<AchievementService>(false).AchievementBossCoinGet(coins);
        }
        base.Data.FinalCoins = Mathf.Max(0, base.Data.FinalCoins);
        this._anyUnlockableCacheDirty = true;
        return base.Data.FinalCoins;
    }
}

public abstract class UnlockServiceBase : PersistentService {
    protected UnlockSystemData Data { get { eturn Services.SaveDataService.Data.UnlockSystemData; } }
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

NEON.Framework.SaveData
```csharp
public class SaveDataService : SaveDataServiceBase {
    public SaveData Data;
}

public class SaveData {
    public UnlockSystemData UnlockSystemData = new UnlockSystemData();
}
```

NEON.Game.GameModes
```csharp
public class BattleMode : ControlPlayerMode, ILoading {
    public void SaveInGameProgress(bool incrementLevel = false);
}
```

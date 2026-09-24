## 霓虹深渊 Neon Abyss

### 实现
```csharp
// 移除作弊惩罚
NEON.Framework.InGameConsole.ProcessCommand();
Services.GameState.IsNoAchievement = true;
neonplayerState.HasUseConsole = true; 

NEON.Game.Managers.NEONPlayerState;
this.DefaultBomb            //默认手雷
this.FireBomb               //火焰手雷
this.IceBomb                //寒冰手雷
this.ThunderBomb            //雷电手雷
this.AlcoholBomb            //酒精手雷（眩晕手雷？）

NEON.UI.UnlockSystem.Service.UnlockService
base.Data.BossCoins         //Boss币（信仰宝石？）
base.Data.FinalCoins        //最终币？（信仰宝石？）
base.Data.AbyssCoins        //深渊币（深渊宝石）
base.Data.ExchangeCoins     //交换币（霓虹币）

// 游戏内有关信仰宝石的修改方法
NEON.Game.GameModes.BattleMode.SaveInGameProgress();
Services.SaveDataService.Data.InGameSave.bossCoin = playerState.attrs.i("BossCoin");
Services.SaveDataService.Data.InGameSave.bossCoin = 999;

```

### 参考
~.
```csharp
public static class Services {
    public static GameState GameState { get; }
    public static InGameConsole InGameConsole { get; }
    public static SaveDataService SaveDataService
    public static UnlockService UnlockService { get; }

}
```

NEON.Game.Managers
```csharp
public class NEONPlayerState : PlayerState {
    private PlayerStateInt Coin = "Coin";
    private PlayerStateInt maxCoin = "MaxCoin";
    private PlayerStateInt currentManas = "CurrentMana";
    private PlayerStateInt maxManas = "MaxMana";
    private PlayerStateInt currentHearts = "CurrentHearts";
    private PlayerStateInt maxHearts = "MaxHearts";
    private PlayerStateInt currentShields = "CurrentShields";
    private PlayerStateInt maxShields = "MaxShields";
    private PlayerStateInt InfiniteKey = "InfinitKey";
    private PlayerStateInt Key = "Key";
    private PlayerStateInt maxKey = "MaxKey";
    public void AddBossCoinMax() { Services.UnlockService.IncBossCoins(999); }
    public void AddCoin(int amount);
    public void AddFinalCoinMax() { Services.UnlockService.IncFinalCoins(999); }
    public void AddFullMana() { this.AddMana(this.maxManas); }
    public void AddHeart(int value);
    public void AddKey(int amount);
    public void AddMana(int value);
    public void AddShield(int value);
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

}
```

NEON.Game.GameModes
```csharp
public class BattleMode : ControlPlayerMode, ILoading {
    public void SaveInGameProgress(bool incrementLevel = false);
}
```

## 挺进地牢 Enter The Gungeon

### 第三方api
> Mod the Gungeon API
> - Thunderstore: https://thunderstore.io/c/enter-the-gungeon/p/MtG_API/Mod_the_Gungeon_API/
> - GitHub: https://github.com/SpecialAPI/ModTheGungeonAPI


### 实现
```csharp
PlayerController player = GameManager.Instance.PrimaryPlayer;

player.Blanks = 5; // 空响弹
player.SetIsFlying(true, "debug_flight", true, false);   // 飞行

HealthHaver hh = playerl.healthHaver;
hh.FullHeal();  // 回满血
hh.IsVulnerable = false // 无敌

PlayerConsumables pc = player.carriedConsumables;
pc.Currency = 100;  // 货币
pc.KeyBullets = 10; // 钥匙
pc.ResourcefulRatKeys = 10; // 老鼠资源钥匙

PunchoutController punchCtrl;
punchCtrl.Timer = 120f;  // 打拳时间
PunchoutPlayerController punchPlayer= punchCtrl.Player;
punchPlayer.Health = 100f;  // 打拳玩家血量
punchPlayer.CurrentExhaust = 0f // 打拳玩家疲劳值

```


### 参考
~. 
```csharp
public class GameManager : BraveBehaviour {
    public static GameManager Instance { get; }
    public PlayerController[] AllPlayers { get; }
    public PlayerController PrimaryPlayer { get; set; }
}

public class PlayerController : GameActor, ILevelLoadedListener {
    public int Blanks { get; set; }
    public PlayerConsumables carriedConsumables;
    public bool HasTakenDamageThisRun { get; set; }
    public bool HasTakenDamageThisFloor { get; set; }
}

public abstract class GameActor : DungeonPlaceableBehaviour, IAutoAimTarget {
    public void SetIsFlying(bool value, string reason, bool adjustShadow = true, bool modifyPathing = false);
}

public class DungeonPlaceableBehaviour : BraveBehaviour, IHasDwarfConfigurables { }

public class BraveBehaviour : MonoBehaviour {
    public HealthHaver healthHaver { get; set; }
}

public class HealthHaver : BraveBehaviour {
    protected float AdjustedMaxHealth { get; set; }
    public float Armor { get; set; }
    public bool IsVulnerable { get; set; }
    protected float currentArmor;
    protected float currentHealth = 10f;
    public void FullHeal();
}

public class PlayerConsumables {
    public int Currency { get; set; }
    public int KeyBullets { get; set; }
    public int ResourcefulRatKeys { get; set; }
    public bool InfiniteKeys { get; set; }
}

public class Gun : PickupObject, IPlayerInteractable {
    public int ammo = 25;
    public int CurrentAmmo { get; set; }
    public bool InfiniteAmmo { get; set; }
}

public class PunchoutController : MonoBehaviour {
    public float Timer { get; set; }
    public PunchoutPlayerController Player;
}

public class PunchoutPlayerController : PunchoutGameActor {
    public float CurrentExhaust { get; set; }
}

public abstract class PunchoutGameActor : BraveBehaviour {
    public float Health = 100f;
}

```
## 挺进地牢 Enter The Gungeon

### 第三方api

> Mod the Gungeon API
> - Thunderstore: https://thunderstore.io/c/enter-the-gungeon/p/MtG_API/Mod_the_Gungeon_API/
> - GitHub: https://github.com/SpecialAPI/ModTheGungeonAPI

### 实现

```csharp
PlayerController playerCtrl = GameManager.Instance.PrimaryPlayer;

playerCtrl.Blanks = 5; // 空响弹
playerCtrl.SetIsFlying(true, "debug_flight", true, false);   // 飞行

HealthHaver hh = playerCtrl.healthHaver;
hh.FullHeal();  // 回满血
hh.IsVulnerable = false // 无敌

PlayerConsumables pc = playerCtrl.carriedConsumables;
pc.Currency = 100;  // 货币
pc.KeyBullets = 10; // 钥匙
pc.ResourcefulRatKeys = 10; // 老鼠资源钥匙

PunchoutController punchCtrl;
punchCtrl.Timer = 120f;  // 打拳时间
PunchoutPlayerController punchPlayer= punchCtrl.Player;
punchPlayer.Health = 100f;  // 打拳玩家血量

```

### ~. 参考

```csharp
public class GameManager : BraveBehaviour {
    public static GameManager Instance;
    public PlayerController[] AllPlayers;
    public PlayerController PrimaryPlayer;
}

public class PlayerController : GameActor, ILevelLoadedListener {
    public int Blanks;
    public PlayerConsumables carriedConsumables;
    public bool HasTakenDamageThisRun;
    public bool HasTakenDamageThisFloor;
}

public abstract class GameActor : DungeonPlaceableBehaviour, IAutoAimTarget {
    public void SetIsFlying(bool value, string reason, bool adjustShadow = true, bool modifyPathing = false);
}

public class DungeonPlaceableBehaviour : BraveBehaviour, IHasDwarfConfigurables { }

public class BraveBehaviour : MonoBehaviour {
    public HealthHaver healthHaver;
}

public class HealthHaver : BraveBehaviour {
    public bool IsVulnerable;
    protected float AdjustedMaxHealth;
    public float Armor;
    protected float currentArmor;
    protected float currentHealth = 10f;
    public void FullHeal();
}

public class PlayerConsumables {
    public int Currency;
    public int KeyBullets;
    public int ResourcefulRatKeys;
    public bool InfiniteKeys;
}

public class Gun : PickupObject, IPlayerInteractable {
    public int CurrentAmmo;
    public bool InfiniteAmmo;
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
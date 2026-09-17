## 挺进地牢 Enter The Gungeon

### 实现

```csharp
PlayerController pCtrl = GameManager.Instance.PrimaryPlayer;

HealthHaver hh = pCtrl.healthHaver;
hh.FullHeal();

PlayerConsumables pc = pCtrl.carriedConsumables;
pc.Currency = 100;
pc.KeyBullets = 10;
pc.ResourcefulRatKeys = 10;

pCtrl.m_isFlying.AddOverride("MyMod_Flight", null);

```

### ~. 参考

```csharp
public class PlayerController : GameActor, ILevelLoadedListener {
    public int Blanks;
    public bool HasTakenDamageThisRun;
    public bool HasTakenDamageThisFloor;
}

public class Gun : PickupObject, IPlayerInteractable {
    public int CurrentAmmo;
    public bool InfiniteAmmo;
}

```
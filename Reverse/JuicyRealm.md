## 恶果之地 Juicy Realm

### 实现

```csharp
// 生成 Boss 箱
LootManager lm = GameManager.GetManager<LootManager>();
GameObject gobj = lm.GenerateChest(ChestType.Pro, 0, 0, 0);
lm.ShowChest(gobj, null);


DataManager dm = GameManager.GetManager<DataManager>();
dm.Core = 100;  // 果核

```

### 参考

~.

```csharp
public class PlayerObject : HumanoidObject {
    public int Coin { get; set; }
    public void LootCoin(int amount, bool isFromPlayer);
    public void LootCore(int amount);
    public bool UseCoin(int amount);
    public bool UseHp(int amount);
    public bool UseHpMax(int amount);
}

public class HumanoidObject : CreatureObject { }

public class CreatureObject : NetworkBehaviour, IDamageable, IFaction, IPlayerControl, IAimable, IPetAttackable {
    public virtual float CurrentHp { get; set; }
    public virtual float HpMax { get; set; }
    public virtual float CurrentShield { get; set; }
    public virtual float ShieldMax  { get; }
}

public class ClientManager : NetworkBehaviour {
    public static ClientManager Instance { get; private set; }
    public Character character;
    public PlayerObject player;
}

public class SkillItem : Item {
    public float cd;
    public float timer;
    public virtual bool CanUse() {
	    return this.timer >= this.cd;
    }
}

public class Item : ItemObject { }

public class ItemObject : NetworkBehaviour {}

```

SpaceCan

```csharp
public class GameManager : MonoBehaviour {
    public static T GetManager<T>() where T : class
}

public class DataManager : IManager {
    public int Core  { get; set; }
}

public class LootManager : IManager {
    public GameObject GenerateBossWeapon(LevelCategory level);
    public GameObject GenerateChest(ChestType chestType = ChestType.Nice, int offsetLevelMin = 0, int offsetLevelMax = 0, int coreCount = 0, int addID = 0);
    public GameObject GenerateWeaponLoot(int offsetLevelMin, int offsetLevelMax, int addID);
    public GameObject GenerateLevelWeapon(int minOffsetLevel, int maxOffsetLevel, int extraId = 0);
}

public class LogicManager : IManager {
    public List<SCPlayer> GetPlayers();
}

public class SCPlayer {
    public PlayerObject creature;
}


```

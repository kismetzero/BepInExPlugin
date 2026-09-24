## 恶果之地 Juicy Realm

### 实现
```csharp
// 生成 Boss 箱
LootManager loot = GameManager.GetManager<LootManager>();
GameObject gobj = loot.GenerateChest(ChestType.Pro, 0, 0, 0);
loot.ShowChest(gobj, null);

DataManager data = GameManager.GetManager<DataManager>();
data.Core = 100;  // 果核

LogicManager logic = GameManager.GetManager<LogicManager>();
List<SCPlayer> players = logic.GetPlayers();
PlayerObject player = players[player_num].creature;

player.CurrentHp = 2f;      // HP
player.HpMax = 2f;          // HP上限
player.CurrentShield = 2f;  // 护盾值
player.ShieldMax = 2f;      // 护盾上限
player.currentEnergy = 2f;  // 能量值
player.energyMax = 2f;      // 能量上限
player.Coin = 100;          // 金币

```


### 参考
~.
```csharp
public class PlayerObject : HumanoidObject {
    public int Coin { get; set; }
    public float currentEnergy;
    public float energyMax;
    public SkillItem currentSkill;
    public override float ShieldMax { get; }
    protected override void Hurt(DamageInfo damageInfo);
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
    protected virtual void Hurt(DamageInfo damageInfo);
    public virtual void OnDamage(DamageInfo damageInfo);
    private MultiBool canNotHurt = new MultiBool();
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
    public Config config;
}

public class Config {
    public List<PlayerDefine> Player = new List<PlayerDefine>();
    public List<EnemyDefine> Enemy = new List<EnemyDefine>();
    public List<ItemDefine> Item = new List<ItemDefine>();
    public List<WeaponDefine> Weapon = new List<WeaponDefine>();
    public List<WeaponBuffDefine> WeaponBuff = new List<WeaponBuffDefine>();
    public List<PetDefine> Pet = new List<PetDefine>();
}

public class LogicManager : IManager {
    public List<SCPlayer> GetPlayers();
}

public class SCPlayer {
    public PlayerObject creature;
}

public class LootManager : IManager {
    public GameObject GenerateBossWeapon(LevelCategory level);
    public GameObject GenerateChest(ChestType chestType = ChestType.Nice, int offsetLevelMin = 0, int offsetLevelMax = 0, int coreCount = 0, int addID = 0);
    public GameObject GenerateItem(int lootId);
    public GameObject GenerateItem(string prefabName, bool isLoot = true);
    public GameObject GenerateChestPet(bool isLoot = true);
    public GameObject GenerateChestPet(string key, bool isLoot = true);
    public GameObject GenerateChestWeaponBuffItem();
    public GameObject GenerateChestWeaponBuffItem(string key);
    public GameObject GenerateWeaponLoot(int offsetLevelMin, int offsetLevelMax, int addID);
    public GameObject GenerateLevelWeapon(int minOffsetLevel, int maxOffsetLevel, int extraId = 0);
}


```

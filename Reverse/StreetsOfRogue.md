## 地痞街区 Streets Of Rogue

### 实现
```csharp
GameController gc = GameController.gameController;
gc.unlocks.AddNuggets(50);      // 增加鸡块

Agent player = gc.playerAgent
player.health = 80f;            // hp
player.healthMax = 80f;         // hp上限
player.ghost = true;            // 幽灵状态，不能攻击
player.invisible = true;        // 隐身
player.dontHate = true;         // 不仇恨
player.copsDontCare = true;     // 警察无视
player.aboveTheLaw = true       // 法外狂徒
player.resurrect = true         // 复活
player.quickResurrect = true    // 快速复活

// 添加金币
InvItem invItem = new InvItem();
invItem.invItemName = "Money";
invItem.invItemCount = num;
invItem.ItemSetup(true);
player.inventory.AddItem(invItem);

```


### 参考
~. 
```csharp
public class GameController : MonoBehaviour {
    public static GameController gameController;
    public Agent playerAgent;
    public Unlocks unlocks;
    public SessionDataBig sessionDataBig;
    public SpawnerMain spawnerMain;
}

public class Agent : PlayfieldObject {
    public int currentHealth {
	    get { return this.objectMultAgent.currentHealth; }
	    set { this.objectMultAgent.NetworkcurrentHealth = value; }
    }
    public float health = 100f;
    public float healthMax;
    public StatusEffects statusEffects;
    public InvDatabase inventory;
    public bool ghost;
    public bool invisible;
    public bool dontHate;
    public bool copsDontCare;
    public bool aboveTheLaw;
    public bool resurrect;
    public bool quickResurrect;
}

public class PlayfieldObject : MonoBehaviour {
    public ObjectMultAgent objectMultAgent;
}

public class ObjectMultAgent : ObjectMult { }

public class ObjectMult : ObjectMultPlayfield {
    public int currentHealth;
}

public class InvDatabase : MonoBehaviour {
    public InvItem AddItem(InvItem item);
    public List<InvItem> InvItemList = new List<InvItem>();
}

public class InvItem : IComparable<InvItem> {
    public string invItemName;
    public int invItemCount;
    public void ItemSetup(bool notNew);
    public void SetupDetails(bool notNew);
}

public class Unlocks : MonoBehaviour {
    public void AddNuggets(int numNuggets)
    {
        this.gc.sessionDataBig.nuggets += numNuggets;
        if (this.gc.sessionDataBig.nuggets > 99)
        {
            this.gc.sessionDataBig.nuggets = 99;
        }
        this.gc.unlocks.SaveUnlockData(true);
    }
}

public class SessionDataBig : MonoBehaviour {
    public int nuggets;
}

public class SpawnerMain : MonoBehaviour { }

public class StatusEffects : MonoBehaviour {
    public List<StatusEffect> StatusEffectList = new List<StatusEffect>();
    public bool ignoreInvincible;
    public void ChangeHealth(float healthNum, PlayfieldObject damagerObject, uint cameFromClient, float clientFinalHealthNum, string damagerObjectName, byte extraVar);
    public void AddStatusEffect(string statusEffectName, bool showText, Agent causingAgent, uint cameFromClient, bool dontPrevent, int specificTime);
    
}

```

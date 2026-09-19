## 武装原型 Broforce

### 实现

```csharp
HeroController.AddLife(playerNum);  // 增加生命
HeroController.ChangeBro(playerNum, HeroType.None); // 切换英雄

Player player = HeroController.players[playerNum];
player.character.invulnerable = true;   // 无敌

var broBase = player.character as BroBase;
broBase.SpecialAmmo += 1;   // 增加特殊弹药
```

### 参考

~. 

```csharp
public class HeroController : NetworkObject, ISerializationCallbackReceiver {
    public static HeroController Instance { get;}
    public static Player[] players = new Player[4];
    public static void AddLife(int playerNum);
    public static void ChangeBro(int playerNum, HeroType newHeroType);
}

public class Player : NetworkObject {
    public TestVanDammeAnim character { get; set; }
    public int Lives { get; set; }
    public void AddLife();
    public void RemoveLife();
    public void SpawnHero(HeroType nextHeroType);
}

public class NetworkObject : MonoBehaviour { }

public class BroBase : TestVanDammeAnim {
    public override int SpecialAmmo { get; set; }
}

public class TestVanDammeAnim : Unit { }

public class Unit : NetworkedUnit {
    public virtual bool invulnerable { get; set; }
}

public class NetworkedUnit : BroforceObject { }

public class BroforceObject : NetworkObject { }

```

## 武装原型 Broforce

### 实现

```csharp
HeroController.players[playerNum].character.invulnerable = true;

HeroController.AddLife(playerNum);

HeroController.ChangeBro(playerNum, HeroType.None);

var broBase = HeroController.players[playerNum].character as BroBase;
broBase.SpecialAmmo += 1;
```

### ~. 参考

```csharp
public class Unit : NetworkedUnit {
    public virtual bool invulnerable;
}

public class TestVanDammeAnim : Unit { }

public class BroBase : TestVanDammeAnim {
    public override int SpecialAmmo;
}

public class Player : NetworkObject {
    public TestVanDammeAnim character;
    public void AddLife();
    public void RemoveLife();
    public void SpawnHero(HeroType nextHeroType);
}

public class HeroController : NetworkObject, ISerializationCallbackReceiver {
    public static HeroController Instance;
    public static Player[] players = new Player[4];
    public static void AddLife(int playerNum);
    public static void ChangeBro(int playerNum, HeroType newHeroType);
}

```

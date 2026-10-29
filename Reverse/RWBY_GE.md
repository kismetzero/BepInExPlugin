## RWBY: 戮兽之蚀 RWBY: Grimm Eclipse

### 实现
```csharp

```


### 参考
~. 
```csharp
public class CharacterStats : EntityComponent<BaseEntity> {
    public int CurrentSpecial { get { return this.m_stats.curSpecial; } }
    public int MaxSpecial { get { return this.m_stats.maxSpecial; } }
    public float CurrentHealth { get { return (float)this.m_stats.curHealth; } }
    public float MaxHealth { get { return (float)this.m_stats.maxHealth; } }
    public int MaxShield { get { return this.m_stats.maxShield; } }
    public CharacterStats.Stats m_stats = new CharacterStats.Stats();
    public class Stats {
        public string charName = string.Empty;
        public int curSpecial = 4;
        public int maxSpecial = 4;
        public int curHealth = 250;
        public int maxHealth = 250;
        public int currentShield;
        public int maxShield;
    }
}

public class BaseEntity : Photon.MonoBehaviour {
    public CharacterStats Stats { get { return this.m_statsScript; } }
    protected CharacterStats m_statsScript;
    // base -> UnityEngine.Component
    protected virtual void Awake() { this.m_statsScript = base.GetComponent<CharacterStats>(); }
}

public class Photon.MonoBehaviour : MonoBehaviour {}
```

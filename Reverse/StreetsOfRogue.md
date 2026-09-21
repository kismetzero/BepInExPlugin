## 暴力街区 StreetsOfRogue

### 实现

```csharp
GameController gc = GameController.gameController;
gc.unlocks.AddNuggets(50);  // 增加鸡块
```

### 参考

~. 

```csharp
public class GameController : MonoBehaviour {
    public static GameController gameController;
    public Agent playerAgent;
    public Unlocks unlocks;
}

public class Agent : PlayfieldObject {
    
}

public class Unlocks : MonoBehaviour {
    public void AddNuggets(int numNuggets);
}

```

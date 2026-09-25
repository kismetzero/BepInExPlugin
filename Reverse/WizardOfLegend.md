## 传说中的法师 Wizard Of Legend

### 实现
```csharp
DebugMenu.Instance.Toggle();    // 调用Debug菜单
```


### 参考
~. 
```csharp
public class DebugMenu : MonoBehaviour {
    public static DebugMenu Instance { get{ return DebugMenu.instance; } }
    public void Toggle();
}
```

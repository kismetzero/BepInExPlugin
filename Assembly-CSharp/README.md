<h1>Assembly-CSharp</h1>

[霓虹深渊NeonAbyss](#1)
[恶果之地JuicyRealm](#2)

<h3 id="1">霓虹深渊NeonAbyss</h3>

<h5>NEON.Game.Managers.NEONPlayerState  //角色状态类</h5>

<details>
  <summary><mark><font color=darkred>点击查看详细内容</font></mark></summary>
     <br />
  <p> 角色各项属性</p>
  <pre><code>  
this.Key.Set  
this.Coin.Set  
this.currentManas.Set  
this.currentHearts.Set  
this.currentShields.Set  
   <br />
this.DefaultBomb.Set  
this.FireBomb.Set  
this.IceBomb.Set  
this.ThunderBomb.Set  
this.AlcoholBomb.Set 
  </code></pre>
</details>

<h5>NEON.UI.UnlockSystem.Service.UnlockService  //解锁相关类(特殊货币)</h5>

<details>
  <summary><mark><font color=darkred>点击查看详细内容</font></mark></summary>
     <br />
  <p> 特殊货币属性</p>
  <pre><code>  
base.Data.BossCoins  
base.Data.FinalCoins  
base.Data.AbyssCoins  
base.Data.ExchangeCoins
  </code></pre>
</details>

<h5>NEON.Game.GameModes.BattleMode  //(游戏内信仰宝石)</h5>

<details>
  <summary><mark><font color=darkred>点击查看详细内容</font></mark></summary>
     <br />
  <p> 游戏内有关信仰宝石的修改方法</p>
  <pre>
  复制修改public void SaveInGameProgress()方法中的  
Services.SaveDataService.Data.InGameSave.bossCoin = playerState.attrs.i("BossCoin");  
的playerState.attrs.i("BossCoin")
  </pre>
</details>

<h5>NEON.Framework.InGameConsole  //游戏中作弊控制台类</h5>

<details>
  <summary><mark><font color=darkred>点击查看详细内容</font></mark></summary>
     <br />
  <p> 移除作弊惩罚</p>
  <pre>
  修改private void ProcessCommand()方法中的  
Services.GameState.IsNoAchievement = true;  
neonplayerState.HasUseConsole = true;  
的值为false  
  </pre>
</details>

<h3 id="2">恶果之地JuicyRealm</h3>

<h5>~.PlayerObject  //角色类</h5>

<details>
  <summary><mark><font color=darkred>点击查看详细内容</font></mark></summary>
     <br />
  <p> 角色属性</p>
  <pre><code>  
this.Coin  
this.CurrentHp  
  </code></pre>
</details>

<h5>SpaceCan.DataManager  //数据处理类</h5>
Core  果核

<h5>CreatureObject  //生物类</h5>
CurrentHp 当前生命值

<h5>ClientManager  //核心类</h5>
private void CallTargetCostCore 花费果核方法

<h5>RuntimeTestWindow  //测试类</h5>
GameManager.GetManager<DataManager>().Core






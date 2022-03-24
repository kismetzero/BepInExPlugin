# 霓虹深渊 Neon Abyss

### NEON.Game.Managers.NEONPlayerState  //角色状态类

##### 角色各项属性

    this.Key                    //钥匙
    this.Coin                   //金币
    this.currentManas           //玛娜（水晶）
    this.currentHearts          //生命
    this.currentShields         //护盾
    
    this.DefaultBomb            //默认手雷
    this.FireBomb               //火焰手雷
    this.IceBomb                //寒冰手雷
    this.ThunderBomb            //雷电手雷
    this.AlcoholBomb            //酒精手雷（眩晕手雷？）

### NEON.UI.UnlockSystem.Service.UnlockService  //解锁相关类(特殊货币)

##### 特殊货币属性

    base.Data.BossCoins         //Boss币（信仰宝石？）
    base.Data.FinalCoins        //最终币？（信仰宝石？）
    base.Data.AbyssCoins        //深渊币（深渊宝石）
    base.Data.ExchangeCoins     //交换币（霓虹币）

### NEON.Game.GameModes.BattleMode  //(游戏内信仰宝石)

##### 游戏内有关信仰宝石的修改方法

复制修改public void SaveInGameProgress()方法中的

    Services.SaveDataService.Data.InGameSave.bossCoin = playerState.attrs.i("BossCoin");   
    Services.SaveDataService.Data.InGameSave.bossCoin = 999;

### NEON.Framework.InGameConsole  //游戏中作弊控制台类

##### 移除作弊惩罚

修改private void ProcessCommand()方法中的

    Services.GameState.IsNoAchievement = true;      //true --> false
    neonplayerState.HasUseConsole = true;           //true --> false



!!!!!一定得从videoscene开始，否则会没有GameManager会无法运行

如何更改每波的敌人种类和数量
=====

敌人种类路径：Assets/Resources/Prefabs/Enemy/
敌人种类数量设定檔：Assets/GameData/Enemy Generator Data/EnemyGenerator.json


如何添加剧情文本
=====

文本格式：.txt
文本设定asset路径：Assets/GameData/Dialogue_Data/A/TypeA Dialogue Data.asset
文本放置路径：Assets/Dialogue/


如何更改道具参数
=====

道具A路径：Assets/Resources/Prefabs/item/A/
道具A各种类参数设定檔：Assets/GameData/Item Data/A/


如何更改枪支升级参数
=====

枪支升级设定檔：Assets/GameData/Guns Upgrade Data/BanbooGunUpgrade.json
枪支升级参考：Assets\Scripts\Canvas\SceneBattle.cs 的151行 SendToGM function
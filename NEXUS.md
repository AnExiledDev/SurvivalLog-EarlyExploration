Name: Early Exploration
Summary: Choose the day each exploration zone opens after the disaster, as early as day one: Hardware Store, Supermarket, School, Hospital and Tech Company. Every zone opens when it normally does until you set its day. Story locks are lifted on your day, so you can go before the story sends you. Travel time and trip items don't change. Needs Exiled's Toolkit.

Description (BBCode):
[center][img]https://sldb.core-directive.com/media/save/2-changes-stay.png[/img]
[img]https://sldb.core-directive.com/media/notice/off.png[/img][/center]

Waiting for the story to let you out gets old. Early Exploration lets you pick the day each exploration zone opens after the disaster, as early as the first day.

A fresh install changes nothing. Every zone opens when it normally does until you set its day to 1 or later, on the Mod Manager's Mods screen or in the config file.

[size=5]Main features[/size]
[list]
[*][b]Every zone from day one[/b]: set them all to 1 and the Hardware Store, Deserted Supermarket, Abandoned School, Abandoned Hospital and Tech Company are open on the exploration map from the first day after the disaster.[/*]
[*][b]A day for each zone[/b]: each zone has its own setting for the day it opens. Pick a day later than it normally opens and the zone stays closed until then.[/*]
[*][b]0 changes nothing[/b]: leave a zone at 0, the default, and it opens when it normally does.[/*]
[*][b]Story locks are lifted[/b]: a zone the story would open later opens on your day instead, and it stays open in your save.[/*]
[*][b]Leave the shelter early[/b]: normally you can't leave the shelter until the "Exploration Tool" story event on day 8, where you learn to make a Noisemaker. That event now starts on the first day any zone opens. You need the workbench to make a Noisemaker, so the "Repair Workbench" event starts that day too, as if you had found the workbench manual. Repair the workbench, make a Noisemaker and head out.[/*]
[*][b]Trips don't change[/b]: travel time and the items a trip needs (such as a Noisemaker) are the same as normal.[/*]
[/list]
Before the disaster nothing changes. Endless and challenge runs aren't touched.

Every setting is explained in English and Chinese.

[size=5]Known issues[/size]
[list]
[*]Going to a zone before the story sends you there can mean arriving without what the story would have given you first, such as a key or an access card for a locked door inside. Those doors stay locked until you find what opens them.[/*]
[*]Setting a zone back to 0 brings back its normal opening day, but a story lock the mod already lifted stays lifted in that save.[/*]
[/list]

[center][img]https://sldb.core-directive.com/img/mods-banner.jpg[/img]
[size=4][url=https://www.nexusmods.com/survivallog/mods/73]Get Mod Manager[/url] to set up all your mods in game  |  [url=https://sldb.core-directive.com/en/]Open the Survival Log wiki[/url] for items, recipes and maps[/size][/center]

[size=5]Requirements[/size]
[list]
[*]Survival Log 1.1.20719. A game update can break it until the mod is updated.[/*]
[*][url=https://www.nexusmods.com/survivallog/mods/12]BepInEx for Survival Log[/url]. Install it first.[/*]
[*][url=https://www.nexusmods.com/survivallog/mods/61]Exiled's Toolkit[/url] 0.20.0 or newer, which applies settings changed on the Mod Manager's Mods screen without restarting the game. Early Exploration does not load without it.[/*]
[*]Optional: [url=https://www.nexusmods.com/survivallog/mods/73]Mod Manager[/url]. With it, you can change every setting in game on its Mods screen and see what each one does, without opening the config file.[/*]
[/list]

[size=5]Installation[/size]
[list=1]
[*]Install [url=https://www.nexusmods.com/survivallog/mods/12]BepInEx[/url] [i](if no other mods were installed before, start the game once so BepInEx finishes setup, then quit)[/i].[/*]
[*]Install Exiled's Toolkit the same way: download its zip and extract it into the game folder.[/*]
[*]Download the zip from the Files tab.[/*]
[*]Extract it into the game folder (the folder with [font=Courier New]SurvivalLog.exe[/font]). The zip holds a BepInEx folder, so the DLL lands in the right place. When asked, merge the folders. The result should be:
[code]C:\Program Files (x86)\Steam\steamapps\common\Survival Log\BepInEx\plugins\EarlyExploration.dll[/code][/*]
[/list]

[size=5]Uninstall[/size]
Delete [font=Courier New]EarlyExploration.dll[/font] from the [font=Courier New]BepInEx\plugins[/font] folder. Zones already opened in a save stay open in it.

[size=5]Configuration[/size]
To change settings, edit [font=Courier New]BepInEx\config\com.dev.survivallog.earlyexploration.cfg[/font] while the game is closed, or do it in game on the Mod Manager's Mods screen, where changes take effect at once (see [url=https://github.com/AnExiledDev/SurvivalLog-EarlyExploration/blob/main/CONFIG.md]CONFIG.md[/url] for every setting).

[size=5]Feedback[/size]
Found a bug or have an idea? Come say hi on the [url=https://discord.gg/trTsyxUFqg]Core Directive Discord[/url], or post in this page's Posts tab. For bugs, please include [font=Courier New]BepInEx\LogOutput.log[/font] and [font=Courier New]BepInEx\early-exploration.log[/font].
To reach me directly, message [b]exiled.dev[/b] on Discord. You can find me in the [url=https://discord.gg/MZGg4gQAUe]official Survival Log Discord[/url]. I'm also on X as [url=https://x.com/AnExiledDev]@AnExiledDev[/url].

[size=5]Source and license[/size]
Open source under MIT: [url=https://github.com/AnExiledDev/SurvivalLog-EarlyExploration]github.com/AnExiledDev/SurvivalLog-EarlyExploration[/url]

[line]
[size=5]简体中文[/size]

等剧情放你出门实在太久了。Early Exploration 让你自己设定灾难之后各个探索地点开放的天数，最早可以是第一天。

刚装上时不做任何改动。在 Mod Manager 的「模组」界面或配置文件里把某个地点设为 1 或更大的天数之前，每个地点都和平时一样开放。

[size=5]主要功能[/size]
[list]
[*][b]第一天全部开放[/b]：全部设为 1 后，社区检修小屋、残破超市、废弃学校、废弃医院和科技公司，灾难后第一天就在探索地图上开放。[/*]
[*][b]每个地点单独设置[/b]：每个地点都有单独的开放天数设置。如果设定的天数比平时开放得晚，该地点会一直关闭到那一天。[/*]
[*][b]设为 0 不做改动[/b]：保持 0（默认），该地点就和平时一样开放。[/*]
[*][b]解除剧情锁定[/b]：原本要等剧情推进才开放的地点，会在你设定的那天开放，并且在存档中保持开放。[/*]
[*][b]提前出门[/b]：平时要到第 8 天的「探索工具」剧情事件学会做叮当罐子后才能出门。现在任一地点开放的第一天就会触发这个事件。做叮当罐子要用工作台，所以「修理工作台」事件也会同时触发，就像找到了工作台使用手册一样。修好工作台、做好叮当罐子就能出门。[/*]
[*][b]出行条件不变[/b]：路程时间和出发所需的物品（例如叮当罐子）都和平时一样。[/*]
[/list]
灾难发生前一切照旧。无尽模式和挑战模式不受影响。

每项设置都有中英文说明。

[size=5]已知问题[/size]
[list]
[*]在剧情引导之前前往某个地点，可能还没拿到剧情原本会先给你的东西，例如里面某扇锁着的门的钥匙或门禁卡。找到能打开它的东西之前，这些门会一直锁着。[/*]
[*]把某个地点改回 0 会恢复它平时的开放天数，但模组已经解除的剧情锁定在该存档中不会恢复。[/*]
[/list]

[url=https://www.nexusmods.com/survivallog/mods/73]获取 Mod Manager[/url]，在游戏里设置所有模组。[url=https://sldb.core-directive.com/zh/]打开 Survival Log 维基[/url]，物品、配方、地图一查就有。

[size=5]前置要求[/size]
[list]
[*]Survival Log 1.1.20719。游戏更新后模组可能暂时失效，直到模组更新为止。[/*]
[*][url=https://www.nexusmods.com/survivallog/mods/12]BepInEx for Survival Log[/url]，请先安装。[/*]
[*][url=https://www.nexusmods.com/survivallog/mods/61]Exiled's Toolkit[/url] 0.20.0 或更新版本，用于让在 Mod Manager 的「模组」界面改动的设置无需重启游戏即可生效。没有它，Early Exploration 无法加载。[/*]
[*]可选：[url=https://www.nexusmods.com/survivallog/mods/73]Mod Manager[/url]。装了它，可以在游戏里它的「模组」界面修改每项设置，并看到每项的说明，不用打开配置文件。[/*]
[/list]

[size=5]安装[/size]
[list=1]
[*]安装 [url=https://www.nexusmods.com/survivallog/mods/12]BepInEx[/url] [i]（如果之前没有装过其他模组，先启动一次游戏，让 BepInEx 完成初始化，然后退出）[/i]。[/*]
[*]用同样的方式安装 Exiled's Toolkit：下载它的压缩包并解压到游戏目录。[/*]
[*]在「文件」页下载压缩包。[/*]
[*]把压缩包解压到游戏目录（[font=Courier New]SurvivalLog.exe[/font] 所在的文件夹）。压缩包里带有 BepInEx 文件夹，DLL 会自动放到正确位置。如果提示合并文件夹，选择合并。解压后应为：
[code]C:\Program Files (x86)\Steam\steamapps\common\Survival Log\BepInEx\plugins\EarlyExploration.dll[/code][/*]
[/list]

[size=5]卸载[/size]
从 [font=Courier New]BepInEx\plugins[/font] 文件夹中删除 [font=Courier New]EarlyExploration.dll[/font]。存档中已经开放的地点会保持开放。

[size=5]配置[/size]
可以在游戏内 Mod Manager 的「模组」界面修改，改动立即生效；也可以在游戏关闭时编辑 [font=Courier New]BepInEx\config\com.dev.survivallog.earlyexploration.cfg[/font]。配置文件中每项设置都有中文说明，完整说明见 [url=https://github.com/AnExiledDev/SurvivalLog-EarlyExploration/blob/main/CONFIG.md]CONFIG.md[/url]（英文）。

[size=5]反馈[/size]
发现 bug 或有好点子？欢迎来 [url=https://discord.gg/trTsyxUFqg]Core Directive Discord[/url] 打个招呼，或在本页的「Posts」标签里发帖。报告 bug 时，请附上 [font=Courier New]BepInEx\LogOutput.log[/font] 和 [font=Courier New]BepInEx\early-exploration.log[/font]。
想直接联系我，可以在 Discord 上给 [b]exiled.dev[/b] 发消息，我在 [url=https://discord.gg/MZGg4gQAUe]Survival Log 官方 Discord[/url] 里。也可以在 X 上找我：[url=https://x.com/AnExiledDev]@AnExiledDev[/url]。

[size=5]源代码与许可[/size]
基于 MIT 许可开源：[url=https://github.com/AnExiledDev/SurvivalLog-EarlyExploration]github.com/AnExiledDev/SurvivalLog-EarlyExploration[/url]

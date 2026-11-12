# Early Exploration

Lets you pick the day each exploration zone opens after the disaster, as early as day one. A fresh install changes nothing: every zone opens when it normally does until you set its day (`1` is the first day after the disaster).

In Survival Log the places you can explore after the disaster open slowly: the Hardware Store on day 5, the others only once the story gets there. Early Exploration gives each zone its own setting for the day it opens.

## Features

- **Every zone from day one**: set them all to 1 and the Hardware Store, Deserted Supermarket, Abandoned School, Abandoned Hospital and Tech Company are open on the exploration map from the first day after the disaster.
- **A day for each zone**: each zone has its own setting for the day it opens. Pick a day later than it normally opens and the zone stays closed until then.
- **0 changes nothing**: leave a zone at 0, the default, and it opens when it normally does.
- **Story locks are lifted**: a zone the story would open later opens on your day instead. It is opened the same way the story would open it, so it stays open in your save.
- **Leave the shelter early**: normally you can't leave the shelter until the "Exploration Tool" story event on day 8, where you learn to make a Noisemaker. That event now starts on the first day any zone opens. You need the workbench to make a Noisemaker, so the "Repair Workbench" event starts that day too, as if you had found the workbench manual. Repair the workbench, make a Noisemaker and head out.
- **Trips don't change**: travel time and the items a trip needs (such as a Noisemaker) are the same as normal.

Before the disaster nothing changes. Endless and challenge runs aren't touched.

Every config setting is explained in English and Simplified Chinese.

## Known issues and limits

- Going to a zone before the story sends you there can mean arriving without what the story would have given you first, such as a key or an access card for a locked door inside. Those doors stay locked until you find what opens them.
- Story events that would have opened a zone still play when the story reaches them.
- Setting a zone back to 0 brings back its normal opening day, but a story lock this mod already lifted stays lifted in that save.

## Requirements

- Survival Log 1.1.20719. Built for this version. A game update can break it until the mod is updated.
- The [BepInEx Pack for Survival Log](https://www.nexusmods.com/survivallog/mods/12), the BepInEx 6 (IL2CPP) build for the game.
- **Exiled's Toolkit** 0.20.0 or newer, which applies settings changed on the Mod Manager's Mods screen without restarting the game. Early Exploration does not load without it.
- Optional: [Mod Manager](https://www.nexusmods.com/survivallog/mods/73). With it, you can change every setting in game on its Mods screen and see what each one does, without opening the config file.

## Install

1. Install the [BepInEx Pack for Survival Log](https://www.nexusmods.com/survivallog/mods/12) (if no other mods were installed before, start the game once so BepInEx finishes setup, then quit).
2. Install Exiled's Toolkit: download `ExiledToolkit-x.y.z.zip` from [its latest release](https://github.com/AnExiledDev/SurvivalLog-ExiledToolkit/releases/latest) or from [Nexus Mods](https://www.nexusmods.com/survivallog/mods/61), and extract it into the game folder (the folder with `SurvivalLog.exe`).
3. Download `EarlyExploration-x.y.z.zip` from the [latest release](https://github.com/AnExiledDev/SurvivalLog-EarlyExploration/releases/latest) or from [Nexus Mods](https://www.nexusmods.com/survivallog/mods/96).
4. Extract the zip into the game folder (the folder with `SurvivalLog.exe`). It holds a `BepInEx` folder, so the DLL lands in the right place. When asked, merge the folders. The result should be:
   `C:\Program Files (x86)\Steam\steamapps\common\Survival Log\BepInEx\plugins\EarlyExploration.dll`

## Uninstall

Delete `EarlyExploration.dll` from the `BepInEx\plugins` folder. Zones already opened in a save stay open in it. The rest open when they normally do. For a clean slate, also delete `BepInEx\config\com.dev.survivallog.earlyexploration.cfg`, `BepInEx\early-exploration.log` and `BepInEx\early-exploration.log.1` if it is there.

## Configuration

Settings live in `BepInEx\config\com.dev.survivallog.earlyexploration.cfg`, created the first time the game starts with the mod. Change them in game on the Mod Manager's Mods screen, where they take effect at once, or edit the file while the game is closed. See [CONFIG.md](CONFIG.md) for every setting.

## Troubleshooting

Built against BepInEx `6.0.0-be.788`.

Check `BepInEx\LogOutput.log` for `===== Early Exploration 0.1.7 started`, followed by a line listing each zone's open day. Once a run has loaded, each zone logs the day it opens on, `opened, added ...` when its story lock is lifted, `gave the workbench manual's reward` when the workbench repair is started early, and `started the Noisemaker event` when the way out is unlocked early. The same lines are in `BepInEx\early-exploration.log`, which survives restarts. Once it passes 1 MB, it is renamed to `early-exploration.log.1` and a new one is started.

## Feedback and bug reports

Found a bug, have an idea, or something feels off? Come say so on the Core Directive Discord: https://discord.gg/trTsyxUFqg . You can also post in the Posts tab of the mod's Nexus Mods page. When reporting a bug, attach `BepInEx\LogOutput.log`, `BepInEx\early-exploration.log` and `BepInEx\early-exploration.log.1` if it is there.

To reach me directly, message `exiled.dev` on Discord. You can find me in the [official Survival Log Discord](https://discord.gg/MZGg4gQAUe). I'm also on X as [@AnExiledDev](https://x.com/AnExiledDev).

A Survival Log wiki with detailed documentation for all of my mods is coming soon.

## Build

This is a BepInEx 6 IL2CPP plugin. It compiles against BepInEx, the game's IL2CPP interop assemblies and Exiled's Toolkit, so a game install with BepInEx set up and started once is required. Those assemblies are not part of this repo. The .NET 8 SDK is required.

```
dotnet build src/EarlyExploration -c Release -p:GameDir="C:\path\to\Survival Log" -p:ToolkitDll="C:\path\to\ExiledToolkit.dll"
```

`GameDir` is the game folder and `ToolkitDll` is a built `ExiledToolkit.dll` (0.20.0 or newer). The output DLL is at `src\EarlyExploration\bin\Release\EarlyExploration.dll`.

The unit tests cover the open-day rules and need nothing from the game:

```
dotnet test tests/EarlyExploration.Tests
```

## License

MIT. Copyright (c) 2026 AnExiledDev. See [LICENSE](LICENSE).

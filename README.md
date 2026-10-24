# Early Exploration

Opens the exploration zones after the disaster from day one. Set the day each zone opens, or keep the game's own rule for any of them.

In Survival Log the places you can explore after the disaster open slowly: the Hardware Store on day 5, the others only once the story gets there. Early Exploration opens them all on the first day after the disaster by default, and gives each zone its own setting so you can pick the day it opens instead.

## Features

- **Every zone from day one**: the Hardware Store, Deserted Supermarket, Abandoned School, Abandoned Hospital and Tech Company are open on the exploration map from the first day after the disaster.
- **Your own schedule**: each zone has its own setting for the day it opens. A later day than the game's holds that zone shut until then.
- **Keep the game's rule**: set any zone to 0 and it opens the way the game intends.
- **Story locks lifted too**: a zone the story would open later is opened on your day, the same way the story would open it, so it stays open in your save.
- **Trips stay the same**: travel time and the items a trip needs (such as a Noisemaker) work as in the game.

Before the disaster nothing changes. Endless and challenge runs are left to the game's own rules.

Every config setting is explained in English and Simplified Chinese.

## Known issues and limits

- Going to a zone before the story sends you there can mean arriving without what the story would have given you first, such as a key or an access card for a locked door inside. Those doors stay locked until you find what opens them.
- Story events that would have opened a zone still play when the story reaches them.
- Setting a zone back to 0 puts back the game's day lock, but a story lock this mod already lifted stays lifted in that save.
- The Gas Station is not included: the game does not show it in story runs.

## Requirements

- Survival Log 1.1.19611. Built for this version; a game update can break it until the mod is updated.
- The [BepInEx Pack for Survival Log](https://www.nexusmods.com/survivallog/mods/12), the BepInEx 6 (IL2CPP) build for the game.
- Optional: [Exiled's Toolkit](https://github.com/AnExiledDev/SurvivalLog-ExiledToolkit) 0.4.0 or newer. With it, settings changed on the Mod Manager's Mods screen take effect without restarting the game. Without it, they take effect at the next game start.

## Install

1. Install the [BepInEx Pack for Survival Log](https://www.nexusmods.com/survivallog/mods/12) (if no other mods were installed before, start the game once so BepInEx finishes setup, then quit).
2. Optional, to change settings without a restart: install Exiled's Toolkit. Download `ExiledToolkit-x.y.z.zip` from [its latest release](https://github.com/AnExiledDev/SurvivalLog-ExiledToolkit/releases/latest) or from [Nexus Mods](https://www.nexusmods.com/survivallog/mods/61), and extract it into the game folder (the folder with `SurvivalLog.exe`).
3. Download `EarlyExploration-x.y.z.zip` from the [latest release](https://github.com/AnExiledDev/SurvivalLog-EarlyExploration/releases/latest) or from [Nexus Mods](https://www.nexusmods.com/survivallog/mods/96).
4. Extract the zip into the game folder (the folder with `SurvivalLog.exe`). It holds a `BepInEx` folder, so the DLL lands in the right place. When asked, merge the folders. The result should be:
   `C:\Program Files (x86)\Steam\steamapps\common\Survival Log\BepInEx\plugins\EarlyExploration.dll`

## Uninstall

Delete `EarlyExploration.dll` from the `BepInEx\plugins` folder. Zones already opened in a save stay open in it; the rest go back to the game's rules. For a clean slate, also delete `BepInEx\config\com.dev.survivallog.earlyexploration.cfg` and `BepInEx\early-exploration.log`.

## Configuration

Settings live in `BepInEx\config\com.dev.survivallog.earlyexploration.cfg`, created the first time the game starts with the mod. Change them in game on the Mod Manager's Mods screen, where they take effect at once, or edit the file while the game is closed. See [CONFIG.md](CONFIG.md) for every setting. Without Exiled's Toolkit installed, changes made on the Mods screen take effect at the next game start instead.

## Troubleshooting

Built against BepInEx `6.0.0-be.788`.

Check `BepInEx\LogOutput.log` for `===== Early Exploration 0.1.0 started`, followed by a line listing each zone's open day. Once a run has loaded, each zone logs the day it opens on, and `opened, added ...` when its story lock is lifted. The same lines are in `BepInEx\early-exploration.log`, which survives restarts and trims itself past 1 MB.

## Feedback and bug reports

Found a bug, have an idea, or something feels off? Come say so on the Core Directive Discord: https://discord.gg/trTsyxUFqg . You can also post in the Posts tab of the mod's Nexus Mods page. When reporting a bug, attach `BepInEx\LogOutput.log` and `BepInEx\early-exploration.log`.

To reach me directly, message `exiled.dev` on Discord; you can find me in the [official Survival Log Discord](https://discord.gg/MZGg4gQAUe). I'm also on X as [@AnExiledDev](https://x.com/AnExiledDev).

A Survival Log wiki with detailed documentation for all of my mods is coming soon.

## Build

This is a BepInEx 6 IL2CPP plugin. It compiles against BepInEx and the game's IL2CPP interop assemblies, so a game install with BepInEx set up and started once is required. Those assemblies come from the game and are not part of this repo. The .NET 8 SDK is required.

```
dotnet build src/EarlyExploration -c Release -p:GameDir="C:\path\to\Survival Log"
```

`GameDir` is the game folder. The output DLL is at `src\EarlyExploration\bin\Release\EarlyExploration.dll`.

The unit tests cover the open-day rules and need nothing from the game:

```
dotnet test tests/EarlyExploration.Tests
```

## License

MIT. Copyright (c) 2026 AnExiledDev. See [LICENSE](LICENSE).

# Early Exploration settings

Settings live in `BepInEx\config\com.dev.survivallog.earlyexploration.cfg`, created the first time the game starts with the mod. Change them in game on the Mod Manager's Mods screen, where they take effect at once, or edit the file while the game is closed. Delete the file to get the defaults back. Without Exiled's Toolkit installed, changes made on the Mods screen take effect at the next game start instead.

## Zones

Each setting is the day after the disaster that the zone opens on the exploration map. Days count the way the game shows them: 1 is the first day after the disaster. 0 keeps the game's own rule for that zone. The highest value is 100.

| Setting | Default | Zone | The game's own rule |
| --- | --- | --- | --- |
| `HardwareStore` | `1` | Hardware Store | Day 5 |
| `DesertedSupermarket` | `1` | Deserted Supermarket | Through the story, not before day 5 |
| `AbandonedSchool` | `1` | Abandoned School | Through the story |
| `AbandonedHospital` | `1` | Abandoned Hospital | Through the story |
| `TechCompany` | `1` | Tech Company | Through the story |

A zone the story opens is opened on your day even if the story has not reached it yet. A day later than the game's own holds the zone shut until that day, even once the story has opened it.

Opening a zone does not change what a trip there needs: the travel time and any items you must have with you still apply as in the game.

Setting a zone back to `0` puts the game's day lock back, but a story lock this mod already lifted in a save stays lifted.

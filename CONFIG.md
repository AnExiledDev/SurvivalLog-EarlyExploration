# Early Exploration settings

Settings live in `BepInEx\config\com.dev.survivallog.earlyexploration.cfg`, created the first time the game starts with the mod. Change them in game on the Mod Manager's Mods screen, where they take effect at once, or edit the file while the game is closed. Delete the file to get the defaults back.

## Zones

Each setting is the day the zone opens on the exploration map. Days count the way the game shows them: 1 is the first day after the disaster. Leave a setting at 0, the default, and the zone opens when it normally does. The highest value is 100.

| Setting | Default | Zone | Normally opens |
| --- | --- | --- | --- |
| `HardwareStore` | `0` | Hardware Store | Day 5 |
| `DesertedSupermarket` | `0` | Deserted Supermarket | Through the story, not before day 5 |
| `AbandonedSchool` | `0` | Abandoned School | Through the story |
| `AbandonedHospital` | `0` | Abandoned Hospital | Through the story |
| `TechCompany` | `0` | Tech Company | Through the story |

A zone the story opens is opened on your day, even if the story hasn't got there yet. If you pick a day later than the zone normally opens, it stays closed until that day, even after the story has opened it.

Opening a zone early doesn't change what a trip there needs. The travel time and any items you must bring are the same as normal.

Setting a zone back to `0` brings back its normal opening day, but a story lock this mod already lifted in a save stays lifted.

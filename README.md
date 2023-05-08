# DredgeWinchMods
[![Building](https://github.com/SloopyNoSleep/DredgeWinchMods/actions/workflows/NetFWCI.yml/badge.svg?branch=main)](https://github.com/SloopyNoSleep/DredgeWinchMods/actions/workflows/NetFWCI.yml)

Collection of mods for the game Dredge using the [Winch](https://github.com/Hacktix/Winch) mod loader

## Installation
1. Please refer to [Winch#Installation](https://github.com/Hacktix/Winch/wiki/Installation) to install Winch.
    * Remember to run the game at least once so that the `Mods` folder (among other files and directories) are created
1. Download the latest release and extract the contents to your `C:\Program Files (x86)\Steam\steamapps\common\DREDGE\Mods` folder.
    * That is the default path for steam on Windows
    
## Mods

### :x: :fish: ArbitraryFishRemover
#### Description
Removes any arbitrary set of fishes from the game. This means there following are affected
* ItemManager
    * List of fish removed
* Encyclopedia
    * Entries for these fish will no longer appear
* HarvestPOIs
    * Fishing spots that contain these fish will be removed from the game scene
* HarvestZones
    * Harvest Zones are used to determine what fish can be caught by a net within what areas.
#### Configuration
You can modify the Config.json file that is within the `AbitraryFishRemover` mod folder to specifically choose which fish to remove.
You can find a list of fish-ids here: [Winch#ID Tables](https://github.com/Hacktix/Winch/wiki/ID-Tables#item-ids)

#### Potential Problems
* Removing a fish that is currently in your cargo/net/storage can cause the game to not load/crash
* You can prevent yourself from being able to complete quests by removing required fish which can lead to soft-locks

&nbsp;

### ❓ Randomizer
#### Description
Allows different elements of the game to be randomized depending on configuration options and seed

#### Configuration
* RandomizeHarvestPoIs
   * Randomly permutes HarvestPOIs across existing locations.
* RandomizePoiCoordinates
   * Randomizes coordinates for HarvestPOIs. 
* RandomizeSizes
   * Randomize Fish sizes.
* RandomizeQuestsRequirements
   * Randomize fish required for quests.
* RandomizeHarvestMinigamesTypes
   * Randomize minigame associated to fish.
* RandomizeHarvestableType
   * Randomize HarvestableType (Volcanic, Mangrove, Oceanic) etc for each fish.
* RandomizeDifficulty
   * Randomize difficulty for all fish. 
* Seed
   * Can be used to create repeatable experiences 

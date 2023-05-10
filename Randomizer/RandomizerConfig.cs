using System;
using Winch.Config;

namespace Randomizer;

public class RandomizerConfig
{
    private static RandomizerConfig? _instance;

    public static RandomizerConfig Instance => _instance ??= new();

    public bool RandomizeHarvestPoIs { get; set; } = true;
    public bool RandomizePoiCoordinates { get; set; } = true;
    public bool RandomizeSizes { get; set; } = true;
    public bool RandomizeQuestsRequirements { get; set; } = true;
    public bool RandomizeHarvestMinigamesTypes { get; set; } = true;
    public bool RandomizeHarvestableType { get; set; } = true;
    public bool RandomizeDifficulty { get; set; } = true;
    public int? Seed { get; set; } = null;

    public static void Initialize()
    {
        Instance.RandomizeDifficulty = ModConfig.GetProperty("Randomizer", "RandomizeDifficulty", true);
        Instance.RandomizeHarvestPoIs = ModConfig.GetProperty("Randomizer", "RandomizeHarvestPoIs", true);
        Instance.RandomizePoiCoordinates = ModConfig.GetProperty("Randomizer", "RandomizePoiCoordinates", true);
        Instance.RandomizeSizes = ModConfig.GetProperty("Randomizer", "RandomizeSizes", true);
        Instance.RandomizeQuestsRequirements = ModConfig.GetProperty("Randomizer", "RandomizeQuestsRequirements", true);
        Instance.RandomizeHarvestMinigamesTypes = ModConfig.GetProperty("Randomizer", "RandomizeHarvestMinigamesTypes", true);
        Instance.RandomizeHarvestableType = ModConfig.GetProperty("Randomizer", "RandomizeHarvestableType", false);
        Instance.RandomizeDifficulty = ModConfig.GetProperty("Randomizer", "RandomizeDifficulty", true);
        Instance.Seed = ModConfig.GetProperty("Randomizer", "Seed", SeededRng.Seed);
    }
}
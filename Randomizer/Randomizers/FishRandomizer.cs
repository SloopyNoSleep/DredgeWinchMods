using HarmonyLib;

namespace Randomizer;

public class FishRandomizer
{
    private static readonly Array MiniGameEnums = Enum.GetValues(typeof(HarvestMinigameType));
    private static readonly Array DifficultyEnums= Enum.GetValues(typeof(HarvestDifficulty));
    private static readonly Array HarvestableTypeEnums= Enum.GetValues(typeof(HarvestableType));

    // TODO: Remove possibleSizes here. Either separate out single fish randomization of process all fish at once
    public static void RandomizeFish(RandomizerConfig config, FishItemData fish, List<(float, float)>? possibleSizes = null)
    {
        if (RandomizerConfig.Instance.RandomizeSizes)
        {
            Traverse.Create(fish).Field("minSizeCentimeters").SetValue(possibleSizes[SeededRng.Rng.Next(0,possibleSizes.Count)].Item1);
            Traverse.Create(fish).Field("maxSizeCentimeters").SetValue(possibleSizes[SeededRng.Rng.Next(0,possibleSizes.Count)].Item2);
        }

        if (RandomizerConfig.Instance.RandomizeHarvestMinigamesTypes)
            Traverse.Create(fish).Field("harvestMinigameType")
                .SetValue((HarvestMinigameType)SeededRng.Rng?.Next(MiniGameEnums.Length));
        if (RandomizerConfig.Instance.RandomizeDifficulty)
            Traverse.Create(fish).Field("harvestDifficulty")
                .SetValue((HarvestDifficulty)SeededRng.Rng?.Next(DifficultyEnums.Length));
        if (RandomizerConfig.Instance.RandomizeHarvestableType)
            Traverse.Create(fish).Field("harvestableType")
                .SetValue((HarvestableType)SeededRng.Rng?.Next(HarvestableTypeEnums.Length));
    }
}
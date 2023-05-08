namespace Randomizer;

public class RandomizerConfig
{
    public bool RandomizeHarvestPoIs { get; set; } = true;
    public bool RandomizePoiCoordinates { get; set; } = true;
    public bool RandomizeSizes { get; set; } = true;
    public bool RandomizeQuestsRequirements { get; set; } = true;
    public bool RandomizeHarvestMinigamesTypes { get; set; } = true;
    public bool RandomizeHarvestableType { get; set; } = true;
    public bool RandomizeDifficulty { get; set; } = true;
    public Int32? Seed { get; set; } = null;
}
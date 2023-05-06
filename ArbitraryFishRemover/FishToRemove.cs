using Newtonsoft.Json.Linq;
using Winch.Config;

namespace ArbitraryFishRemover;

public class FishToRemove
{
    public static List<string>? Fish => ModConfig.GetProperty("ArbitraryFishRemover","FishToDelete", new JArray()).ToObject<List<string>>();
    public static bool IsEnabled => Fish is { Count: > 0 };
}
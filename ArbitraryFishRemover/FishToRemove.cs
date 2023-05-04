namespace ArbitraryFishRemover;

public class FishToRemove
{
    public static bool IsEnabled = Fish is { Count: > 0 };
    public static List<string> Fish = new() {"sunfish", "sunfish-ab-1", "moonfish", "moonfish-ab-1"};
}
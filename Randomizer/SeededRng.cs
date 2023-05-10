namespace Randomizer;

public static class SeededRng
{
    public static int Seed = Guid.NewGuid().GetHashCode();

    private static Random? _rng;
    public static Random? Rng
    {
        get => _rng ??= new(Seed);
    }

    public static List<T> FisherYatesShuffle<T>(List<T> originalList)
    {
        List<T> list = new(originalList);
        if (Rng == null) return originalList;

        var n = list.Count();
        for (int i = 0; i < (n - 1); i++)
        {
            var swapIdx = i + SeededRng.Rng.Next(n - i);
            (list[i], list[swapIdx]) = (list[swapIdx], list[i]);
        }

        return list;
    }
}
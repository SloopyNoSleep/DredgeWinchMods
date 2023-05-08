using System.Runtime.CompilerServices;

namespace Randomizer;

public static class SeededRng
{
    public static Int32 Seed = Guid.NewGuid().GetHashCode();

    private static Random? _rng;
    public static Random? Rng
    {
        get => _rng ??= new(Seed);
        private set => _rng = value;
    }
}
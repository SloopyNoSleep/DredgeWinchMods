using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Winch.Config;
using Winch.Core;
using Winch.Core.API;
using Winch.Core.API.Events.Addressables;

namespace Randomizer;

public static class Loader
{
    public static void Initialize()
    {
        RandomizerConfig.Initialize();
        SeededRng.Seed = RandomizerConfig.Instance.Seed ?? SeededRng.Seed;
        DredgeEvent.AddressableEvents.ItemsLoaded.On += OnPostItemLoad;
    }

    public static void OnPostItemLoad(object sender, AddressablesLoadedEventArgs<ItemData> args)
    {
        try
        {
            var allItems = (sender as ItemManager)?.allItems;
            var allFish = allItems?.OfType<FishItemData>().ToList();

            WinchCore.Log.Debug($"There are {allFish.Count} fish to randomize");

            List<(float, float)> possibleSizes = (allFish ?? throw new InvalidOperationException())
                .Select(fish => (Traverse.Create(fish).Field("minSizeCentimeters").GetValue<float>(),
                    Traverse.Create(fish).Field("maxSizeCentimeters").GetValue<float>())).ToList();

            foreach (var fish in allFish)
            {
                FishRandomizer.RandomizeFish(RandomizerConfig.Instance, fish, possibleSizes);
            }
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error($"Error in ItemManagerPatcher: {ex}");
        }
    }



}

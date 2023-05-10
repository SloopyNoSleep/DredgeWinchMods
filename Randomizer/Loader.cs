using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Randomizer.Randomizers;
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

            if (allFish == null)
            {
                WinchCore.Log.Error($"Error in {nameof(Loader)}: allFish is null");
                return;
            }

            FishRandomizer.RandomizeAllFish(RandomizerConfig.Instance, allFish);
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error($"Error in ItemManagerPatcher: {ex}");
        }
    }



}

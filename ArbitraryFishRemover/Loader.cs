using HarmonyLib;
using UnityEngine;
using UnityEngine.Localization;
using Winch.Core;
using Winch.Core.API;
using Winch.Core.API.Events.Addressables;

namespace ArbitraryFishRemover;

public class Loader
{
    public static void Initialize()
    {
        DredgeEvent.AddressableEvents.ItemsLoaded.On += OnPostItemLoad;
    }

    public static void OnPostItemLoad(object sender, AddressablesLoadedEventArgs<ItemData> args)
    {
        WinchCore.Log.Info($"ItemManager patch postfix called");
        WinchCore.Log.Debug($"There are {FishToRemove.Fish.Count} fish to remove");
        int dbgline = 0;
        if (!FishToRemove.IsEnabled) return;
        string fishDataList = "{";
        try
        {
            var allItems = (sender as ItemManager)?.allItems;
            allItems?.RemoveAll(s => (FishToRemove.Fish).Any(x => s.id.Contains(x)));
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error($"Error in ItemManagerPatcher: {ex}");
        }
    }
}
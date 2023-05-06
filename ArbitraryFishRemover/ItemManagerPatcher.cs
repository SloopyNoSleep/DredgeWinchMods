using HarmonyLib;
using Winch.Core;

namespace ArbitraryFishRemover;

[HarmonyPatch(typeof(ItemManager))]
[HarmonyPatch("OnItemDataAddressablesLoaded")]
public class ItemManagerPatcher
{
public static void Postfix(ItemManager __instance)
    {
        WinchCore.Log.Info($"ItemManager patch postfix called");
        WinchCore.Log.Debug($"There are {FishToRemove.Fish.Count} fish to remove");

        if (!FishToRemove.IsEnabled) return;
        try
        {
            var allItems = __instance.allItems;
            allItems.RemoveAll(s => (FishToRemove.Fish).Any(x => s.id.Contains(x)));
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error($"Error in ItemManagerPatcher: {ex}");
        }
    }
}
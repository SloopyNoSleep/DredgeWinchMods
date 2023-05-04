using HarmonyLib;
using Winch.Config;
using Winch.Core;

namespace ArbitraryFishRemover;

[HarmonyPatch(typeof(Encyclopedia))]
[HarmonyPatch("Awake")]
public class EncyclopediaPatcher
{
    public static void Prefix(Encyclopedia __instance)
    {
        if (!FishToRemove.IsEnabled) return;
        WinchCore.Log.Debug($"Encyclopedia patch prefix called");
        WinchCore.Log.Debug($"Banned list is {FishToRemove.Fish}");
        var fishList = Traverse.Create(__instance).Field("allFish").GetValue() as List<FishItemData>;
        fishList?.RemoveAll(s => FishToRemove.Fish.Any(x => s.id.Contains(x)));
    }
}
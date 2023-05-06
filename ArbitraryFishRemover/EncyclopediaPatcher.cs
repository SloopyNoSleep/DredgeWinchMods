using HarmonyLib;
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
        var fishList = Traverse.Create(__instance).Field("allFish").GetValue() as List<FishItemData>;
        fishList?.RemoveAll(s => FishToRemove.Fish.Any(x => s.id.Contains(x)));
    }
}
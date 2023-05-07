using HarmonyLib;
using Winch.Core;

namespace ArbitraryFishRemover;

[HarmonyPatch(typeof(HarvestZoneDetector))]
[HarmonyPatch("GetHarvestableItemIds")]
public class HarvestZoneDetectorPatcher
{
    public static void Postfix(HarvestZoneDetector __instance, ref List<string> __result)
    {
        WinchCore.Log.Info($"HarvestZoneDetector patch postfix called");
        if (!FishToRemove.IsEnabled) return;
        WinchCore.Log.Debug($"Removing banned fish from harvest zone detector");
        __result.RemoveAll(s => FishToRemove.Fish!.Any(s.Contains));
        var zones = Traverse.Create(__instance).Field("currentHarvestZones").GetValue() as List<HarvestZone>;

        if (zones == null) return;

        foreach (var zone in zones!)
        {
            var items = Traverse.Create(zone).Field("harvestableItems").GetValue() as HarvestableItemData[];
            if (items == null) continue;
            if (items.Any(s => FishToRemove.Fish!.Any(x => s.id.Contains(x))))
            {
                var sanitizedItems = items?.Where(s=> !FishToRemove.Fish!.Any(x => s.id.Contains(x))).ToArray();
                Traverse.Create(zone).Field("harvestableItems").SetValue(sanitizedItems);
                WinchCore.Log.Debug($"Sanitized harvest zone {zone.name}");
            }


        }

    }
}
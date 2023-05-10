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
        var zones = __instance.currentHarvestZones;

        if (zones == null) return;

        foreach (var zone in zones!)
        {
            var items = zone.harvestableItems;
            if (items == null) continue;
            if (items.Any(s => FishToRemove.Fish!.Any(x => s.id.Contains(x))))
            {
                var sanitizedItems = items?.Where(s=> !FishToRemove.Fish!.Any(x => s.id.Contains(x))).ToArray();
                zone.harvestableItems = sanitizedItems;
                WinchCore.Log.Debug($"Sanitized harvest zone {zone.name}");
            }


        }

    }
}
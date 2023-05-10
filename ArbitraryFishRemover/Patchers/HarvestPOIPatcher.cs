using HarmonyLib;
using UnityEngine;
using Winch.Core;
using Object = UnityEngine.Object;

namespace ArbitraryFishRemover;

[HarmonyPatch(typeof(HarvestPOI))]
[HarmonyPatch("Start")]
public class HarvestPOIPatcher
{
    public static void Prefix(HarvestPOI __instance)
    {

        if (!FishToRemove.IsEnabled) return;
        try
        {
            var poi = __instance;
            var poidata = poi.harvestPOIData;
            if (poidata != null)
            {
                var items = poidata.items;
                var nightItems = poidata.nightItems;
                if (items!.Any(s => FishToRemove.Fish!.Any(x => s.id.Equals(x))) ||
                    nightItems!.Any(s => FishToRemove.Fish!.Any(x => s.id.Equals(x))))
                {
                    WinchCore.Log.Debug($"Found banned item in {poi.name}");
                    Object.Destroy(poi.gameObject);
                    WinchCore.Log.Debug($"Destroyed {poi.name}");
                }
            }
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error($"Error in PlayerPatcher: {ex}");
        }
    }
}
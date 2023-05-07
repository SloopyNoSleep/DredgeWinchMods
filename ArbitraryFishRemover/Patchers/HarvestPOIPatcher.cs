using HarmonyLib;
using UnityEngine;
using Winch.Core;

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
            var poidata = Traverse.Create(poi).Field("harvestPOIData").GetValue() as HarvestPOIDataModel;
            if (poidata != null)
            {
                var items = Traverse.Create(poidata).Field("items").GetValue() as List<HarvestableItemData>;
                var nightItems = Traverse.Create(poidata).Field("nightItems").GetValue() as List<HarvestableItemData>;
                if (items!.Any(s => FishToRemove.Fish!.Any(x => s.id.Equals(x))) ||
                    nightItems!.Any(s => FishToRemove.Fish!.Any(x => s.id.Equals(x))))
                {
                    WinchCore.Log.Debug($"Found banned item in {poi.name}");
                    GameObject.Destroy(poi.gameObject);
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
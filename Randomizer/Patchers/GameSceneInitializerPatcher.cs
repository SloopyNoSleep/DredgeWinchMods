using HarmonyLib;
using Randomizer.Helpers;
using UnityEngine;
using Winch.Core;

namespace Randomizer.Patchers;

[HarmonyPatch(typeof(GameSceneInitializer))]
[HarmonyPatch("Start")]
public class GameSceneInitializerPatcher
{
    // TODO: Add an event to DredgeEvents to replace the need for this patch. GameSceneInitializer.Start is a good canditate for a new event
    public static void Prefix(GameSceneInitializer __instance)
    {
        try
        {
            var harvestPoisContainer = GameObject.Find("HarvestPOIs");
            if (harvestPoisContainer == null)
            {
                WinchCore.Log.Error("HarvestPOIs not found");
                return;
            }

            var poiList = PoiHelpers.GetPoiListFromContainer(harvestPoisContainer);
            PoiHelpers.PermuteHarvestPoiLocations(poiList);
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error($"Error in {nameof(GameSceneInitializerPatcher)}: exception {ex}");
        }
    }
}
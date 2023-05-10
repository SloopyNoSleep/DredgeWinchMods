using UnityEngine;
using Winch.Core;
using Object = UnityEngine.Object;

namespace Randomizer.Helpers;

public class PoiHelpers
{
    // TODO: I'd like a better method than having to destroy and recreate SimpleBuoyantObject and Cullable components but this proved troublesome during testing
    public static void MovePoi(GameObject poi, Vector3 position, CullingBrain cullingBrain)
    {
        if (cullingBrain == null) throw new ArgumentNullException(nameof(cullingBrain));
        if (poi == null) throw new ArgumentNullException(nameof(poi));

        poi.SetActive(false);

        var poiBouyantObj = poi.GetComponent<SimpleBuoyantObject>();
        var poiCullable = poi.GetComponent<Cullable>();

        cullingBrain.RemoveCullable(poiCullable);

        Object.Destroy(poiBouyantObj);
        Object.Destroy(poiCullable);

        poi.transform.position = position;

        poi.GetComponent<SimpleBuoyantObject>();
        poiCullable = poi.GetComponent<Cullable>();

        cullingBrain.AddCullable(poiCullable);

        poi.SetActive(true);
    }

    public static void SwapPoi(GameObject poi, GameObject oPoi, CullingBrain cullingBrain)
    {
        var originalPosition = poi.transform.position;
        MovePoi(poi, oPoi.transform.position, cullingBrain);
        MovePoi(oPoi, originalPosition, cullingBrain);
    }

    public static List<GameObject> GetPoiListFromContainer(GameObject harvestPoisContainer)
    {
        var poisContainerTransform = harvestPoisContainer.transform;
        return (from Transform poiTransform in poisContainerTransform select poiTransform.gameObject).ToList();
    }

    public static void PermuteHarvestPoiLocations(List<GameObject> poiList)
    {
        List<GameObject> permutedPoiList = SeededRng.FisherYatesShuffle(poiList);

        for (var i = 0; i < poiList.Count; i++)
        {
            var poi = poiList[i];
            var permutedPoi = permutedPoiList[i];

            SwapPoi(poi, permutedPoi, GameManager.Instance.CullingBrain);

            WinchCore.Log.Debug($"Swapping {poi.name} at location [{poi.transform.position.x}, {poi.transform.position.y}, {poi.transform.position.z}] with {permutedPoi.name} at location [{permutedPoi.transform.position.x}, {permutedPoi.transform.position.y}, {permutedPoi.transform.position.z}]");

            poi.SetActive(true);
            permutedPoi.SetActive(true);
        }
    }
}
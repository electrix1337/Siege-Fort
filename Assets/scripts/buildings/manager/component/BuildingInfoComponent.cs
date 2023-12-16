using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfoComponent : MonoBehaviour
{
    public List<BuildingSectionSerialized> buildingsSections;

    public int Count { get => buildingsSections.Count; }

    private void Awake()
    {
        GameObjectPath.AddPath("BuildingInfoComponent", gameObject);
    }

    public List<BuildingSectionSerialized> GetActiveBuildingSection()
    {
        List<BuildingSectionSerialized> buildingsUnlocked = new List<BuildingSectionSerialized>();

        for (int i = 0; i < buildingsSections.Count; ++i)
            if (buildingsSections[i].active)
                buildingsUnlocked.Add(buildingsSections[i]);

        return buildingsUnlocked;
    }
}

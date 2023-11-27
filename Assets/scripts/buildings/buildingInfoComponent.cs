using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class buildingInfoComponent : MonoBehaviour
{
    public List<buildingSectionSerialized> buildingsSections;

    public int Count { get => buildingsSections.Count; }

    public List<buildingSectionSerialized> GetActiveBuildingSection()
    {
        List<buildingSectionSerialized> buildingsUnlocked = new List<buildingSectionSerialized>();

        for (int i = 0; i < buildingsSections.Count; ++i)
            if (buildingsSections[i].active)
                buildingsUnlocked.Add(buildingsSections[i]);

        return buildingsUnlocked;
    }
}

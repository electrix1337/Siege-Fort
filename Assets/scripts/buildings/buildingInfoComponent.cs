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
        List<buildingSectionSerialized> sectionsUnlonked = new List<buildingSectionSerialized>();

        for (int i = 0; i < buildingsSections.Count; ++i)
            if (buildingsSections[i].active)
                sectionsUnlonked.Add(buildingsSections[i]);

        return sectionsUnlonked;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class buildingSectionSerialized
{
    public string name;
    public bool active;
    public List<buildingInfoSerialized> buildingsSerialized;

    public List<buildingInfoSerialized> GetUnlockedBuildings()
    {
        List<buildingInfoSerialized> sectionsUnlonked = new List<buildingInfoSerialized>();

        for (int i = 0; i < buildingsSerialized.Count; ++i)
            if (buildingsSerialized[i].unlocked)
                sectionsUnlonked.Add(buildingsSerialized[i]);

        return sectionsUnlonked;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingSectionSerialized
{
    public string name;
    public bool active;
    public List<BuildingSerialized> buildingsSerialized;

    public List<BuildingSerialized> GetUnlockedBuildings()
    {
        List<BuildingSerialized> sectionsUnlonked = new List<BuildingSerialized>();

        for (int i = 0; i < buildingsSerialized.Count; ++i)
            if (buildingsSerialized[i].unlocked)
                sectionsUnlonked.Add(buildingsSerialized[i]);

        return sectionsUnlonked;
    }
}

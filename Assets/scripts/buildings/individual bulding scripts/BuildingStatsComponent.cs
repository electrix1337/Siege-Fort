using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingHealthComponent))]
public class BuildingStatsComponent : MonoBehaviour, IActivateBuilding
{
    public int Hp { get; private set; }
    public void ActivateBuilding(BuildingSerialized buildingInfo)
    {
        Hp = buildingInfo.maxHealth;
    }
}

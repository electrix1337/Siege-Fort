using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingHealthComponent))]
public class BuildingStatsComponent : MonoBehaviour, IActivateBuilding
{
    public void ActivateBuilding(BuildingSerialized buildingInfo)
    {
        BuildingHealthComponent healthComponent = gameObject.GetComponent<BuildingHealthComponent>();
        healthComponent.SetBuildingHp(buildingInfo.maxHealth);
    }
}

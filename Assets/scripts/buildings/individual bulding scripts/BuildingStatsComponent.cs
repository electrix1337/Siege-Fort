using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class BuildingStatsComponent : MonoBehaviour, IActivateBuilding
{
    public void ActivateBuilding(BuildingSerialized buildingInfo)
    {
        HealthComponent healthComponent = gameObject.GetComponent<HealthComponent>();
        healthComponent.SetHp(buildingInfo.maxHealth);
    }
}

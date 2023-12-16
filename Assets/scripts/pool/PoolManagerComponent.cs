using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(loadingComponent))]
public class PoolManagerComponent : MonoBehaviour
{
    [SerializeField] GameObject poolFolder;
    [SerializeField] GameObject buildingManager;

    BuildingInfoComponent buildingInfo;

    private void Awake()
    {
        GameObjectPath.AddPath("PoolManagerComponent", gameObject);
    }
    private void Start()
    {
        buildingInfo = buildingManager.GetComponent<BuildingInfoComponent>();

        for (int i = 0; i < buildingInfo.buildingsSections.Count; ++i)
        {

        }
    }
}

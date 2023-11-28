using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(buildingInfoComponent))]
public class PlacingBuildingComponent : MonoBehaviour
{
    [SerializeField] Camera camera;
    GameObject objectInHand = null;
    buildingInfoComponent buildingInfo;
    Vector3 size;
    private void Start()
    {
        buildingInfo = GetComponent<buildingInfoComponent>();
    }

    /* require 2 arguments:
     * argument 1: building name
     * argument 2: section of the building
     * */
    public void PlacingBuilding(List<string> Building)
    {
        if (Building.Count == 2)
        {
            Destroy(objectInHand);
            buildingSectionSerialized buildingSection = buildingInfo.buildingsSections.Find((obj) => obj.name.Equals(Building[1]));
            buildingInfoSerialized buildingInfoSerialized = buildingSection.buildingsSerialized.Find((obj) => obj.name == Building[0]);

            objectInHand = Instantiate(buildingInfoSerialized.building);
            size = buildingInfoSerialized.size;
            objectInHand.layer = 6;
        }
        else
        {
            Debug.Log("The PlacingBuilding function is not use correctly and does nothing right now to prevent bugs");
        }
    }

    private void Update()
    {
        //cancel button is right click
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Destroy(objectInHand);
            objectInHand = null;
        }
        if (objectInHand != null)
        {
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, 1 << 7))
            {
                Vector3 objectPosition = new Vector3(Mathf.Floor(hitInfo.point.x) + ((size.x % 2) * 0.5f), size.y / 2, Mathf.Floor(hitInfo.point.z) + ((size.z % 2) * 0.5f));
                objectInHand.transform.position = objectPosition;

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    objectInHand = null;
                }
            }
        }
    }
}

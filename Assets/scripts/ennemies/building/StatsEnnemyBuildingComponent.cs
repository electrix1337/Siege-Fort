using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BuildingInfoComponent))]
public class StatsEnnemyBuildingComponent : MonoBehaviour
{
    [SerializeField] GameObject healthCanvas;
    [SerializeField] Transform ennemyBuildingFolder;
    [SerializeField] GameObject camera;
    BuildingInfoComponent buildingInfo;
    CameraControlComponent cameraControlComponent;
    Vector3 size;

    BuildingGrid grid;
    Vector2Int gridSize = new Vector2Int(100, 100);
    // Start is called before the first frame update
    void Start()
    {
        buildingInfo = GetComponent<BuildingInfoComponent>();
        grid = GetComponent<PlacingBuildingComponent>().grid;

        cameraControlComponent = camera.GetComponent<CameraControlComponent>();

        for (int i = 0; i < ennemyBuildingFolder.childCount; ++i)
        {
            Transform building = ennemyBuildingFolder.GetChild(i);
            BuildingSerialized buildingInfoSerialized = new BuildingSerialized();
            for (int v = 0; v < buildingInfo.buildingsSections.Count; v++)
            {
                for (int c = 0; c < buildingInfo.buildingsSections[v].buildingsSerialized.Count; ++c)
                {
                    if (buildingInfo.buildingsSections[v].buildingsSerialized[c].name == building.name)
                    {
                        buildingInfoSerialized = buildingInfo.buildingsSections[v].buildingsSerialized[c];
                        break;
                    }
                }
            }
            Vector3 objectPosition = new Vector3(Mathf.Floor(building.position.x) + ((size.x % 2) * 0.5f), /*size.y / 2*/0, Mathf.Floor(building.position.z) + ((size.z % 2) * 0.5f));

            size = Vector3.one * buildingInfoSerialized.size;

            List<Vector2Int> positions = new List<Vector2Int>();
            for (int x = 0; x < size.x; ++x)
            {
                for (int z = 0; z < size.z; ++z)
                {
                    positions.Add(new Vector2Int(x + Mathf.FloorToInt(objectPosition.x) + (int)size.x % 2 + gridSize.x / 2,
                        z + Mathf.FloorToInt(objectPosition.z) + (int)size.z % 2 + gridSize.y / 2));
                }
            }

            PlaceBuilding(buildingInfoSerialized, building, positions);
        }
    }

    void PlaceBuilding(BuildingSerialized buildingInfoSerialized, Transform building, List<Vector2Int> positions)
    {
        grid.Build(positions);

        //gameObject.transform.localScale = Vector3.one * buildingInfoSerialized.size;

        GameObject hpCanvas = Instantiate(healthCanvas, building);

        /*set the size and the position at a more appropriate location*/
        //hpCanvas.transform.localScale = new Vector3(1 / buildingInfoSerialized.size,
            //1 / buildingInfoSerialized.size, 1 / buildingInfoSerialized.size);
        hpCanvas.transform.position = building.position + new Vector3(0, /*hitbox.localScale.y + 2*/building.GetComponent<BoxCollider>().size.y * building.transform.localScale.y + 0.7f, 0);

        cameraControlComponent.AddRotatingUI(hpCanvas.GetComponent<activateOnRotation>());

        BuildingStatsComponent buildingStat = building.gameObject.AddComponent<BuildingStatsComponent>();

        building.GetComponent<IActivateEnnemy>();
        buildingStat.ActivateBuilding(buildingInfoSerialized);
    }
}

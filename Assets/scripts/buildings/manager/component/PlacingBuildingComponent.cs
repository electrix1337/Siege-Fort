using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.WSA;

[RequireComponent(typeof(BuildingInfoComponent))]
public class PlacingBuildingComponent : MonoBehaviour, ICancel
{
    [SerializeField] InputActionAsset inputAsset;
    [SerializeField] GameObject buildingFolder;
    [SerializeField] GameObject tempFolder;
    [SerializeField] Camera camera;
    [SerializeField] float cancelTime;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject eventSystem;
    [SerializeField] GameObject ressourceManager;
    [SerializeField] Vector2Int gridSize;

    BuildingInfoComponent buildingInfo;
    RessourceManagerComponent ressourceManagerComponent;
    //info for the building in hand
    GameObject objectInHand = null;
    BuildingSerialized buildingInfoSerialized;
    BuildingGrid grid;

    //versatile values
    Vector3 size;
    Vector3 objectPosition;
    float time = 0;
    bool cancelPressed = false;

    bool holdingShift = false;

    GraphicRaycaster m_Raycaster;
    EventSystem m_EventSystem;
    PointerEventData m_PointerEventData;
    private void Start()
    {
        buildingInfo = GetComponent<BuildingInfoComponent>();
        ressourceManagerComponent = ressourceManager.GetComponent<RessourceManagerComponent>();

        InputActionMap actionMap = inputAsset.FindActionMap("Camera");
        InputAction shift = actionMap.FindAction("SpeedCamera");

        grid = new BuildingGrid(gridSize.x, gridSize.y);

        shift.performed += HoldObject;
        shift.canceled += HoldObject;

        //aide de unity: https://docs.unity3d.com/2019.1/Documentation/ScriptReference/UI.GraphicRaycaster.Raycast.html

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = eventSystem.GetComponent<EventSystem>();
    }

    void HoldObject(InputAction.CallbackContext action)
    {
        holdingShift = action.ReadValue<float>() != 0;
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
            BuildingSectionSerialized buildingSection = buildingInfo.buildingsSections.Find((obj) => obj.name.Equals(Building[1]));
            buildingInfoSerialized = buildingSection.buildingsSerialized.Find((obj) => obj.name == Building[0]);


            objectInHand = Instantiate(buildingInfoSerialized.blueprint, tempFolder.transform);
            size = buildingInfoSerialized.size;
            objectInHand.layer = 6;
        }
        else
        {
            Debug.Log("The PlacingBuilding function is not use correctly and does nothing right now to prevent bugs");
        }
    }

    public void Cancel()
    {
        Destroy(objectInHand);
        objectInHand = null;
    }
    //put the buidling in play
    void PlaceBuilding(List<Vector2Int> positions)
    {
        GameObject building = Instantiate(buildingInfoSerialized.building, buildingFolder.transform);
        building.transform.position = objectInHand.transform.position;
        grid.Build(positions);

        IActivate iActivate = building.GetComponent<IActivate>();
        if (iActivate != null)
        {
            for (int i = 0; i < buildingInfoSerialized.activationArguments.Count; ++i)
            {

            }
            iActivate.Activate();
        }
    }

    void Build()
    {
        //check if the space is occupied
        List<Vector2Int> positions = new List<Vector2Int>();
        for (int x = 0; x < size.x; ++x)
        {
            for (int z = 0; z < size.z; ++z)
            {
                positions.Add(new Vector2Int(x + Mathf.FloorToInt(objectPosition.x) + gridSize.x / 2, 
                    z + Mathf.FloorToInt(objectPosition.z) + gridSize.y / 2));
            }
        }
        bool canBuild = grid.CanBuild(positions);

        if (canBuild)
        {

            //check if the player have the current ressources needed
            List<CostSerialized> costSerialized = buildingInfoSerialized.costs;
            List<(string, int)> ressources = new List<(string, int)>();
            for (int i = 0; i < costSerialized.Count; i++)
            {
                ressources.Add((costSerialized[i].name, costSerialized[i].cost));
            }
            canBuild = ressourceManagerComponent.UseRessources(ressources);

            if (canBuild)
            {
                if (holdingShift)
                {
                    //put the building in play and keep one in hand to place more
                    PlaceBuilding(positions);
                }
                else
                {
                    PlaceBuilding(positions);
                    //remove the building from the player mouse
                    Destroy(objectInHand);
                    objectInHand = null;
                }
            }
            else
            {
                Destroy(objectInHand);
                objectInHand = null;
            }
        }
        else
        {
            if (!holdingShift)
            {
                Destroy(objectInHand);
                objectInHand = null;
            }
        }
    }


    private void Update()
    {
        if (objectInHand != null)
        {
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, 1 << 7))
            {
                //code pris de l'aide de unity: https://docs.unity3d.com/2019.1/Documentation/ScriptReference/UI.GraphicRaycaster.Raycast.html
                
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.mousePosition;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();
                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                bool hittingMenu = false;
                foreach (RaycastResult result in results)
                {
                    if (!result.gameObject.name.Equals("buildingSelection"))
                    {
                        hittingMenu = true;
                        break;
                    }
                }
                objectPosition = new Vector3(Mathf.Floor(hitInfo.point.x) + ((size.x % 2) * 0.5f), size.y / 2, Mathf.Floor(hitInfo.point.z) + ((size.z % 2) * 0.5f));
                objectInHand.transform.position = objectPosition;

                if (Mouse.current.leftButton.wasPressedThisFrame && !hittingMenu)
                {
                    Build();
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public class UnitsProductionComponent : MonoBehaviour, IActivateBuilding
{
    [SerializeField] GameObject unit;
    //[SerializeField] GameObject 
    [SerializeField] float timeBetweenSpawn;
    [SerializeField] List<CostSerialized> costPerSpawn;
    RessourceManagerComponent ressourceManager;
    float time = 0;
    LayerMask teamUnitMask;
    public Transform unitFolder;

    private void Start()
    {
        ressourceManager = GameObject.Find(GameObjectPath.GetPath("RessourceManagerComponent")).GetComponent<RessourceManagerComponent>();
        teamUnitMask = LayerMask.NameToLayer("enemie");
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeBetweenSpawn)
        {
            time -= timeBetweenSpawn;
            bool canSpawn = true;
            if (costPerSpawn.Count != 0)
                canSpawn = ressourceManager.UseRessources(costPerSpawn);
            if (canSpawn)
            {
                GameObject clone = Instantiate(unit, unitFolder);
                clone.layer = teamUnitMask;
                clone.transform.position = gameObject.transform.position;
            }
        }
    }

    public void ActivateBuilding(BuildingSerialized buildingInfo)
    {
        teamUnitMask = LayerMask.NameToLayer("teamates");
        unitFolder = GameObject.Find("map/teams/Player/units").transform;
    }
}

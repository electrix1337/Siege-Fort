using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class UnitsProductionComponent : MonoBehaviour, IActivateBuilding
{
    [SerializeField] GameObject unit;
    //[SerializeField] GameObject 
    [SerializeField] float timeBetweenSpawn;
    [SerializeField] List<CostSerialized> costPerSpawn;
    RessourceManagerComponent ressourceManager;
    float time = 0;
    Transform folder;

    private void Start()
    {
        ressourceManager = GameObject.Find(GameObjectPath.GetPath("RessourceManagerComponent")).GetComponent<RessourceManagerComponent>();
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeBetweenSpawn)
        {
            time -= timeBetweenSpawn;
            bool canSpawn = ressourceManager.UseRessources(costPerSpawn);
            if (canSpawn)
            {

                //Instantiate(unit, );
            }
        }
    }

    public void ActivateBuilding(BuildingSerialized buildingInfo, string team)
    {
        throw new System.NotImplementedException();
    }
}

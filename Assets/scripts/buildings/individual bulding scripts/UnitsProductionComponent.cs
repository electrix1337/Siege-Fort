using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class UnitsProductionComponent : MonoBehaviour
{
    [SerializeField] GameObject unit;
    //[SerializeField] GameObject 
    [SerializeField] float timeBetweenSpawn;
    [SerializeField] List<CostSerialized> costPerSpawn;
    RessourceManagerComponent ressourceManager;
    float time;

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

            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class UnitsProductionComponent : MonoBehaviour, IActivate
{
    [SerializeField] GameObject unit;
    [SerializeField] float timeBetweenSpawn;
    [SerializeField] List<CostSerialized> costPerSpawn;
    RessourceManagerComponent ressourceManager;
    float time;
    bool active = false;

    public void Activate(params object[] arguments)
    {
        ressourceManager = ((PlacingBuildingComponent)arguments[0]).ressourceManagerComponent;
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            time += Time.deltaTime;
            if (time > timeBetweenSpawn)
            {
                time -= timeBetweenSpawn;
                ressourceManager.UseRessources(costPerSpawn);
            }
        }
    }
}

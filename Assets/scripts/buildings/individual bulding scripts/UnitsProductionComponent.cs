using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public class UnitsProductionComponent : MonoBehaviour, IActivateBuilding, IActivateEnnemy
{
    [SerializeField] GameObject teamUnit;
    [SerializeField] GameObject ennemyUnit;
    //[SerializeField] GameObject 
    [SerializeField] float timeBetweenSpawn;
    [SerializeField] List<CostSerialized> costPerSpawn;
    RessourceManagerComponent ressourceManager;
    float time = 0;
    LayerMask teamUnitMask;
    Transform unitFolder;
    bool canSpawn = false;

    private void Start()
    {
        ressourceManager = GameObject.Find(GameObjectPath.GetPath("RessourceManager")).GetComponent<RessourceManagerComponent>();
    }
    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            time += Time.deltaTime;
            if (time > timeBetweenSpawn)
            {
                time -= timeBetweenSpawn;
                if (teamUnitMask == LayerMask.NameToLayer("teamates"))
                {
                    bool canSpawn = true;
                    if (costPerSpawn.Count != 0)
                        canSpawn = ressourceManager.UseRessources(costPerSpawn);
                    if (canSpawn)
                    {
                        GameObject clone = Instantiate(teamUnit, unitFolder);
                        clone.layer = teamUnitMask;
                        clone.transform.position = gameObject.transform.position;
                    }

                }
                else
                {
                    GameObject clone = Instantiate(ennemyUnit, unitFolder);
                    clone.layer = teamUnitMask;
                    clone.transform.position = gameObject.transform.position;
                }
            }
        }
    }

    public void ActivateBuilding(BuildingSerialized buildingInfo)
    {
        teamUnitMask = LayerMask.NameToLayer("teamates");
        unitFolder = GameObject.Find("map/teams/Player/units").transform;
        canSpawn = true;
    }

    public void StartSpawning()
    {
        teamUnitMask = LayerMask.NameToLayer("enemie");
        unitFolder = GameObject.Find("map/teams/ennemy/units").transform;
        canSpawn = true;
    }
}

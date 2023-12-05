using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RessouceExtractorComponent : MonoBehaviour, IActivate
{
    //a voir
    RessourceManagerComponent ressourceManager;
    [SerializeField] float timeBetweenExtraction;
    [SerializeField] string ressourceName;
    [SerializeField] int amountToExtract;
    [SerializeField] Vector3 spawnPosition;
    Transform unitFolder;
    float time = 0;
    bool active = false;

    public void Activate(params object[] arguments)
    {
        ressourceManager = ((PlacingBuildingComponent)arguments[0]).ressourceManagerComponent;
        Debug.Log(spawnPosition + gameObject.transform.position);
        active = true;
    }

    private void Update()
    {
        if (active)
        {
            time += Time.deltaTime;
            if (time > timeBetweenExtraction)
            {
                time -= timeBetweenExtraction;
                ressourceManager.AddRessource(ressourceName, amountToExtract);
            }
        }
    }
}

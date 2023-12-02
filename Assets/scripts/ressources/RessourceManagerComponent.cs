using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(UiRessourcesComponent))]
public class RessourceManagerComponent : MonoBehaviour
{
    public List<RessourceSerialized> ressources;
    UiRessourcesComponent uiRessource;


    private void Start()
    {
        uiRessource = GetComponent<UiRessourcesComponent>();
    }

    public bool UseRessources(List<(string, int)> ressourcesToUse)
    {
        bool canUse = true;
        for (int i = 0; i < ressourcesToUse.Count; ++i)
        {
            if (ressources.Find((obj) => obj.name == ressourcesToUse[i].Item1).ressourceAmount < ressourcesToUse[i].Item2)
            {
                canUse = false;
                break;
            }
        }

        if (canUse)
        {
            for (int i = 0; i < ressourcesToUse.Count; ++i)
            {
                RessourceSerialized ressource = ressources.Find((obj) => obj.name == ressourcesToUse[i].Item1);
                ressource.ressourceAmount -= ressourcesToUse[i].Item2;
                uiRessource.UpdateUi(ressourcesToUse[i].Item1);
            }
        }

        return canUse;
    }

    public void AddRessource(string ressourceName, int amount)
    {
        if (amount > 0)
        {
            ressources.Find((obj) => obj.name == ressourceName).ressourceAmount += amount;
        }
        else
        {
            List<(string, int)> ressources = new List<(string, int)> ();
            ressources.Add((ressourceName, amount));
            UseRessources(ressources);
        }
    }
}

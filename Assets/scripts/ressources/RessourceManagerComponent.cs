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
    public bool UseRessources((string, int) ressourcesToUse)
    {
        bool canUse = false;
        if (!(ressources.Find((obj) => obj.name == ressourcesToUse.Item1).ressourceAmount < ressourcesToUse.Item2))
        {
            RessourceSerialized ressource = ressources.Find((obj) => obj.name == ressourcesToUse.Item1);
            ressource.ressourceAmount -= ressourcesToUse.Item2;
            uiRessource.UpdateUi(ressourcesToUse.Item1);
            canUse = true;
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
            UseRessources((ressourceName, amount));
        }
    }
}

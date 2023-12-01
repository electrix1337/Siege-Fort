using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(RessourceManagerComponent))]
public class UiRessourcesComponent : MonoBehaviour
{
    [SerializeField] GameObject ressourcesUI;
    [SerializeField] GameObject ressourcePrefab;
    List<RessourceSerialized> ressourcesInfo;
    RessourceManagerComponent ressourcesComponent;
    private void Start()
    {
        ressourcesComponent = GetComponent<RessourceManagerComponent>();
        ressourcesInfo = ressourcesComponent.ressources;

        SetRessourcesUI();
    }

    void SetRessourcesUI()
    {
        for (int i = 0; i < ressourcesInfo.Count; ++i)
        {
            GameObject clone = Instantiate(ressourcePrefab, ressourcesUI.transform);
            //clone.GetComponent<Image>().rect

        }
    }


    void ChangeUi()
    {

    }
}

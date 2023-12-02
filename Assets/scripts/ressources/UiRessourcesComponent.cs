using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RessourceManagerComponent))]
public class UiRessourcesComponent : MonoBehaviour
{
    [SerializeField] GameObject ressourcesUI;
    [SerializeField] GameObject ressourcePrefab;
    List<RessourceSerialized> ressourcesInfo;
    List<(string, Text)> textUi = new List<(string, Text)> ();
    RessourceManagerComponent ressourcesComponent;
    private void Start()
    {
        ressourcesComponent = GetComponent<RessourceManagerComponent>();
        ressourcesInfo = ressourcesComponent.ressources;

        SetRessourcesUI();
    }

    //set the ressource on in the ui
    void SetRessourcesUI()
    {
        for (int i = 0; i < ressourcesInfo.Count; ++i)
        {
            GameObject clone = Instantiate(ressourcePrefab, ressourcesUI.transform);
            RawImage image = clone.GetComponent<RawImage>();

            float x = 50 + i * 100;
            image.rectTransform.localPosition = new Vector3(x -Screen.width / 2, 0, 0);
            Text textComponent = clone.transform.GetChild(0).GetComponent<Text>();
            textUi.Add((ressourcesInfo[i].name, textComponent));
            textComponent.text = ressourcesInfo[i].ressourceAmount.ToString();
            clone.transform.GetChild(1).GetComponent<RawImage>().texture = ressourcesInfo[i].image;
        }
    }


    //change the ui of the ressource
    public void UpdateUi(string ressource)
    {
        textUi.Find((obj) => obj.Item1 == ressource).Item2.text = ressourcesInfo.Find((obj) => obj.name == ressource).ressourceAmount.ToString();
    }
}

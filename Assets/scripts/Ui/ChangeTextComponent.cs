using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextComponent : MonoBehaviour
{
    Text text;
    private void Start()
    {
        text = GetComponent<Text>();
    }
    public void ChangeText(BuildingSerialized buildingInfo)
    {
        string str = "Building name: " + buildingInfo.name + "­\n\n" +
            "ressources:";

        for (int i = 0; i  < buildingInfo.costs.Count; i++)
        {
            str += "\n-" + buildingInfo.costs[i].cost + " " + buildingInfo.costs[i].name;
        }

        text.text = str;
    }
}

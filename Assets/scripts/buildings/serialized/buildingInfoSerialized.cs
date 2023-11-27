using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class buildingInfoSerialized
{
    public string name;
    public GameObject building;
    public Texture buildingImage;
    public List<costSerialized> costs;
}

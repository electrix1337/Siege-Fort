using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class buildingInfoSerialized
{
    public string name;
    public bool unlocked = false;
    public GameObject building;
    public Vector3 size;
    public Texture buildingImage;
    public List<costSerialized> costs;
}

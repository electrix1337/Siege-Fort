using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingSerialized
{
    public string name;
    public bool unlocked = false;
    public GameObject building;
    public GameObject blueprint;
    public float size;
    public int maxHealth;
    public Texture buildingImage;
    public List<CostSerialized> costs;
}

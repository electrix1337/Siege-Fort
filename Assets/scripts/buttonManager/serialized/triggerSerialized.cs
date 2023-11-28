using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TriggerSerialized
{
    public string name;
    public bool active = false;
    public List<SubTriggerSerialized> subTriggers;
}

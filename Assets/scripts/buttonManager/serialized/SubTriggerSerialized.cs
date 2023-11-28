using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubTriggerSerialized
{
    public string name;
    public GameObject obj;

    public TriggerType type;
    public string componentName;
    public string functionName;
    public List<string> arguments;
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public enum TriggerType { Activation, Function }
public class triggerSerialized
{
    public string subTriggerName;
    public TriggerType type;
    public string componentName;
    public string functionName;
    public List<string> arguments;
    public GameObject obj;
}

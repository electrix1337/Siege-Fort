using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class triggerSerialized
{
    public enum TriggerType { Button, Function, Activate, Desactivate }
    public string name;
    public GameObject obj;

    public TriggerType type;
    public string componentName;
    public string functionName;
    public List<string> arguments;
}

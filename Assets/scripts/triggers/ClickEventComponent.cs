using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickEventComponent : MonoBehaviour
{
    [SerializeField] GameObject triggerManager;
    
    [System.NonSerialized] public TriggerManagerComponent triggerManagerComponent;
    private void Start()
    {
        if (triggerManager != null)
        {
            triggerManagerComponent = triggerManager.GetComponent<TriggerManagerComponent>();
        }
    }
    public void OnClick(string buttonId)
    {
        triggerManagerComponent.ClickEventSection(buttonId);
    }
}

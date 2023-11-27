using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickEventComponent : MonoBehaviour
{
    [SerializeField] GameObject buttonManager;


    triggerManagerComponent buttonComponent;
    private void Start()
    {
        buttonComponent = buttonManager.GetComponent<triggerManagerComponent>();
    }
    public void OnClick(string buttonId)
    {
        buttonComponent.ClickEventSection(buttonId);
    }
}

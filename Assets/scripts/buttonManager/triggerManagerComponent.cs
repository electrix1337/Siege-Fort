using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerManagerComponent : MonoBehaviour
{
    [SerializeField] List<buttonTriggerSerialized> triggers;


    public void ClickEventSection(string triggerName)
    {
        buttonTriggerSerialized button = triggers.Find((obj) => obj.name == triggerName);
        button.active = !button.active;
        List<GameObject> objects = button.objects;

        for (int i = 0; i < objects.Count; i++)
            objects[i].SetActive(button.active);
    }

    public void AddTrigger(List<GameObject> triggersToAdd, string triggerName, bool active)
    {
        buttonTriggerSerialized buttonTrigger = new buttonTriggerSerialized();
        buttonTrigger.active = active;
        buttonTrigger.name = triggerName;

        for (int i = 0; i < triggers.Count; i++)
            triggersToAdd[i].SetActive(active);

        triggers.Add(buttonTrigger);
    }

    public void RemoveTrigger(string triggerName)
    {
        triggers.Remove(triggers.Find((obj) => obj.name == triggerName));
    }

    public void AddSubTriggers(string triggerName, List<GameObject> triggersToAdd)
    {
        buttonTriggerSerialized button = triggers.Find((obj) => obj.name == triggerName);

        for (int i = 0; i < triggersToAdd.Count; ++i)
            button.objects.Add(triggersToAdd[i]);
    }

    public void RemoveSubTriggers(string triggerName, List<GameObject> triggersToRemove)
    {
        buttonTriggerSerialized button = triggers.Find((obj) => obj.name == triggerName);

        for (int i = 0; i < triggersToRemove.Count; ++i)
            button.objects.Remove(triggersToRemove[i]);
    }
}

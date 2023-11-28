using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType { Button, Function, Activate, Desactivate }
public class TriggerManagerComponent : MonoBehaviour
{
    [SerializeField] List<TriggerSerialized> triggers;


    public void ClickEventSection(string triggerName)
    {
        TriggerSerialized button = triggers.Find((obj) => obj.name == triggerName);
        if (button != null)
        {
            button.active = !button.active;

            List<SubTriggerSerialized> subTriggers = button.subTriggers;

            for (int i = 0; i < subTriggers.Count; i++)
            {
                //check the type of trigger triggering
                if (subTriggers[i].type == TriggerType.Button)
                    subTriggers[i].obj.SetActive(button.active);
                else if (subTriggers[i].type == TriggerType.Function)
                {
                    //if the function have any string arguments
                    if (subTriggers[i].arguments.Count > 0)
                        subTriggers[i].obj.GetComponent(subTriggers[i].componentName).SendMessage(subTriggers[i].functionName, subTriggers[i].arguments);
                    else
                        subTriggers[i].obj.GetComponent(subTriggers[i].componentName).SendMessage(subTriggers[i].functionName);
                }
                else if (subTriggers[i].type == TriggerType.Activate)
                    subTriggers[i].obj.SetActive(true);
                else if (subTriggers[i].type == TriggerType.Desactivate)
                    subTriggers[i].obj.SetActive(false);
            }
        }
        else
        {
            Debug.Log("The trigger named " + triggerName + " doesn't exist!");
        }
    }

    public void AddTrigger(string triggerName, List<SubTriggerSerialized> triggersToAdd, bool active)
    {
        TriggerSerialized buttonTrigger = new TriggerSerialized();
        buttonTrigger.active = active;
        buttonTrigger.name = triggerName;
        buttonTrigger.subTriggers = triggersToAdd;

        triggers.Add(buttonTrigger);
    }

    public void RemoveTrigger(string triggerName)
    {
        triggers.Remove(triggers.Find((obj) => obj.name == triggerName));
    }

    public void AddSubTriggers(string triggerName, List<SubTriggerSerialized> subTriggersToAdd)
    {
        TriggerSerialized trigger = triggers.Find((obj) => obj.name == triggerName);

        for (int i = 0; i < subTriggersToAdd.Count; ++i)
            trigger.subTriggers.Add(subTriggersToAdd[i]);
    }
    public void RemoveSubTriggers(string triggerName, List<string> subTriggersToRemove)
    {
        TriggerSerialized trigger = triggers.Find((obj) => obj.name == triggerName);

        for (int i = 0; i < subTriggersToRemove.Count; ++i)
            trigger.subTriggers.Remove(trigger.subTriggers.Find((Obj) => subTriggersToRemove[i] == Obj.name));
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static triggerSerialized;

public class triggerManagerComponent : MonoBehaviour
{
    [SerializeField] List<buttonTriggerSerialized> triggers;


    public void ClickEventSection(string triggerName)
    {
        buttonTriggerSerialized button = triggers.Find((obj) => obj.name == triggerName);
        if (button != null)
        {
            button.active = !button.active;

            List<triggerSerialized> subTriggers = button.triggers;

            for (int i = 0; i < subTriggers.Count; i++)
            {
                //check the type of trigger triggering
                if (subTriggers[i].type == TriggerType.Button)
                    subTriggers[i].obj.SetActive(button.active);
                else if (subTriggers[i].type == TriggerType.Function)
                {
                    //if the function have any string arguments
                    if (subTriggers[i].arguments.Count > 0)
                        subTriggers[i].obj.GetComponent(subTriggers[i].componentName).SendMessage
                            (subTriggers[i].functionName, subTriggers[i].arguments);
                    else
                        subTriggers[i].obj.GetComponent(subTriggers[i].componentName).SendMessage(subTriggers[i].functionName);
                }
                else if (subTriggers[i].type != TriggerType.Activate)
                    subTriggers[i].obj.SetActive(true);
                else if (subTriggers[i].type == TriggerType.Desactivate)
                    subTriggers[i].obj.SetActive(false);
            }
        }
        else
        {
            Debug.Log("The trigger named " +  triggerName + " doesn't exist!");
        }
    }

    public void AddTrigger(List<triggerSerialized> triggersToAdd, string triggerName, bool active)
    {
        buttonTriggerSerialized buttonTrigger = new buttonTriggerSerialized();
        buttonTrigger.active = active;
        buttonTrigger.name = triggerName;

        triggers.Add(buttonTrigger);
    }

    public void RemoveTrigger(string triggerName)
    {
        triggers.Remove(triggers.Find((obj) => obj.name == triggerName));
    }

    public void AddSubTriggers(string triggerName, List<triggerSerialized> triggersToAdd)
    {
        buttonTriggerSerialized button = triggers.Find((obj) => obj.name == triggerName);

        List<triggerSerialized> settedTriggers = new List<triggerSerialized>();
        for (int i = 0; i < triggersToAdd.Count; ++i)
            button.triggers.Add(triggersToAdd[i]);
    }
    public void RemoveSubTriggers(string triggerName, List<string> triggersToRemove)
    {
        buttonTriggerSerialized button = triggers.Find((obj) => obj.name == triggerName);

        for (int i = 0; i < triggersToRemove.Count; ++i)
            button.triggers.Remove(button.triggers.Find((Obj) => triggersToRemove[i] == Obj.name));
    }
}

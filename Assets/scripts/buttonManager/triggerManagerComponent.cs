using System.Collections.Generic;
using UnityEngine;

/* Button: The button trigger type will active the game object one time on 2
 * Function: The function will call a specific function in a component of a game object
 *      you can pass params in form of strings only
 * Activate: The Activate trigger will always activate the object each time it is call
 * Desactivate: The Desactivate trigger will always desactivate the object each time it trigger
 */
public enum TriggerType { Button, Function, Activate, Desactivate }
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
            Debug.Log("The trigger named " +  triggerName + " doesn't exist!");
        }
    }

    public void AddTrigger(List<triggerSerialized> triggersToAdd, string triggerName, bool active)
    {
        buttonTriggerSerialized buttonTrigger = new buttonTriggerSerialized();
        buttonTrigger.active = active;
        buttonTrigger.name = triggerName;
        buttonTrigger.triggers = triggersToAdd;

        triggers.Add(buttonTrigger);
    }

    public void RemoveTrigger(string triggerName)
    {
        triggers.Remove(triggers.Find((obj) => obj.name == triggerName));
    }

    public void AddSubTriggers(string triggerName, List<triggerSerialized> triggersToAdd)
    {
        buttonTriggerSerialized trigger = triggers.Find((obj) => obj.name == triggerName);

        List <triggerSerialized> settedTriggers = new List<triggerSerialized>();
        for (int i = 0; i < triggersToAdd.Count; ++i)
            trigger.triggers.Add(triggersToAdd[i]);
    }
    public void RemoveSubTriggers(string triggerName, List<string> triggersToRemove)
    {
        buttonTriggerSerialized button = triggers.Find((obj) => obj.name == triggerName);

        for (int i = 0; i < triggersToRemove.Count; ++i)
            button.triggers.Remove(button.triggers.Find((Obj) => triggersToRemove[i] == Obj.name));
    }
}

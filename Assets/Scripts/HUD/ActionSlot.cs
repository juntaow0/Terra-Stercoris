using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionSlot : MonoBehaviour
/* Manages the player interaction with the item or ability selected in the action slot
 * 
 * 
 */ 
{
    [SerializeField] private ActionBundle[] actions;

    //make the action array a scriptable object
    private int selectedSlot = 0;
    bool _onCooldown = false;

    //1. Have a container full of possible items or abilities
    //2. Allow the assigning of one such item or ability to this action slot
    //3. Allow a mapping of the keyboard to trigger the ability of the action
    //4. Once an action is used, initiate a cooldown or expenditure of resources as a consequence of using said action
    //This will be used for all action slots

    /*void changeAction(SlotAction newAction)
    //Changes the current action available to use on the action slot
    {
        selectedSlot = actions.Find(newAction);
    }*/

    public void useAction()
    //Called by Input Manager to use the current slotted ability
    {
        if (!_onCooldown) //We can only use an action if it is not on cooldown
        {
            StartCoroutine(HandleCooldown());
            actions[selectedSlot].action?.Invoke();
        }
        else //When we try to use an ability that is on cooldown, we trigger a sound and a visual effect
        {
            Debug.Log("That ability is on cooldown! You need to wait a little longer.");
            //Note: Eventually add sound and a visual effect here
        }
    }

    IEnumerator HandleCooldown() {
        
        _onCooldown = true;
        yield return new WaitForSeconds(actions[selectedSlot].slot.cooldown);
        _onCooldown = false;
    }
}

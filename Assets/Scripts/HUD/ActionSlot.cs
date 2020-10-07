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
    [SerializeField] private List<Ability> abilities;

    //make the action array a scriptable object
    [SerializeField] private int selectedSlot = 0;

    //1. Have a container full of possible items or abilities
    //2. Allow the assigning of one such item or ability to this action slot
    //3. Allow a mapping of the keyboard to trigger the ability of the action
    //4. Once an action is used, initiate a cooldown or expenditure of resources as a consequence of using said action
    //This will be used for all action slots

    void OnEnable() {
        InputManager.OnMouseClickLeft += UseAction;
        InputManager.OnMouseUpLeft += StopAction;
        InputManager.OnScroll += ChangeAbility;
    }

    void OnDisable() {
        InputManager.OnMouseClickLeft -= UseAction;
        InputManager.OnMouseUpLeft -= StopAction;
        InputManager.OnScroll -= ChangeAbility;
    }

    void OnDestroy() {
        OnDisable();
    }

    void ChangeAbility(int direction) {
        abilities[selectedSlot].stopAction?.Invoke();
        selectedSlot += direction;
        if(selectedSlot >= abilities.Count) selectedSlot = 0;
        else if(selectedSlot < 0) selectedSlot = abilities.Count - 1;
    }

    void UseAction()
    //Called by Input Manager to use the current slotted ability
    {
        if (!abilities[selectedSlot].OnCooldown) //We can only use an action if it is not on cooldown
        {
            abilities[selectedSlot].startAction?.Invoke();
        }
        else //When we try to use an ability that is on cooldown, we trigger a sound and a visual effect
        {
            Debug.Log("That ability is on cooldown! You need to wait a little longer.");
            //Note: Eventually add sound and a visual effect here
        }
    }

    void StopAction() {
        abilities[selectedSlot].stopAction?.Invoke();
    }
}

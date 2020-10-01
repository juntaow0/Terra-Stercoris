using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ActionSlot : MonoBehaviour
/* Manages the player interaction with the item or ability selected in the action slot
 * 
 * 
 */ 
{
    //make the action array a scriptable object
    public Action wrench;
    public String currentAction;
    float cooldown;

    //1. Have a container full of possible items or abilities
    //2. Allow the assigning of one such item or ability to this action slot
    //3. Allow a mapping of the keyboard to trigger the ability of the action
    //4. Once an action is used, initiate a cooldown or expenditure of resources as a consequence of using said action
    //This will be used for all action slots

    // Start is called before the first frame update
    void Start()
    {
        currentAction = wrench.name; //for now, we start with the wrench action equipped by default
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0) //Cool down a use of an action
        {
            cooldown -= Time.deltaTime;
            if (cooldown < 0)
            {
                cooldown = 0;
            }
        }
    }

    void changeAction(String newAction)
    //Changes the current action available to use on the action slot
    {
    
    }

    public void useAction()
    //Called by Input Manager to use the current slotted ability
    {
        if (cooldown == 0) //We can only use an action if it is not on cooldown
        {
            if (currentAction == wrench.name)
            {
                //call ability use from player gameobject here
                cooldown = wrench.cooldown;
                //call the NumericalResources class here to expend resources to use an ability
            }
            else if (currentAction == "sampleAction2")
            {
                //
                ;
            }
        }
        else //When we try to use an ability that is on cooldown, we trigger a sound and a visual effect
        {
            Debug.Log("That ability is on cooldown! You need to wait a little longer.")
            //Note: Eventually add sound and a visual effect here
        }
    }
}

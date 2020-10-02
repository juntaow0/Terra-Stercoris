using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAction", menuName = "SlotAction")]
public class SlotAction : ScriptableObject
{
    public new string name; //The the name of the action
    public Sprite icon; //The icon that represents the action in the menu
    public int strength; //How much damage/healing the ability does
    public int powerCost; //How much power the action costs to be used
    public int healthCost; //How much health the action costs to be used
    public float cooldown; //The minimum time interval between uses of the ability
}

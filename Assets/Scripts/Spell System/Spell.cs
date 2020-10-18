using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public string spellName;
    public int spellID;
    public Sprite icon;
    public int damage;
    public int manaCost;
    public int healthCost;
    public float speed;
    public float range;
    public float cooldownTime;
    
    // maybe more
}

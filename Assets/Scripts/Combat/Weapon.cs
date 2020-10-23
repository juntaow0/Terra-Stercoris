using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject {

    public string weaponName;
    public int weaponID;
    public Sprite icon;
    public int damage;
    public int energyCost;
    public float range;
    public float cooldownTime;
    public AudioCue attackSound;
}
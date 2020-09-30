using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class WeaponBase : Item {

    public WeaponType type;
    public int damage;
    public float range;
    public float cooldown;

    [Space(20)]

    public float speed;
    public Sprite projectileSprite;
}
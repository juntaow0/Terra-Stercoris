using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
        MELEE,
        RANGED
}

public class Weapon : Item {

    public WeaponType Type {get; protected set;}
    public int Damage {get; protected set;}
    public float Range {get; protected set;}
    public float Speed {get; protected set;}

    public Weapon(string name, WeaponType type, int damage, float range, float speed) : base(name) {
        Type = type;
        Damage = damage;
        Range = range;
        Speed = speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveWeapon : MonoBehaviour {

    [SerializeField] private WeaponBase weaponBase;
    private Weapon weapon;

    void Start() {
        weapon = new Weapon(weaponBase);
    }

    public void Give() {
        PlayerController.instance.combatController.SetWeapon(weapon);
    }
}

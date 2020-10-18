using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    
    [SerializeField] private GameObject[] _weaponPrefabArray = null;
    private Dictionary<int,GameObject> _weaponPrefabs;
    public static WeaponManager instance {get; private set;}

    void Awake() {
        instance = this;

        _weaponPrefabs = new Dictionary<int,GameObject>();
        foreach(GameObject weapon in _weaponPrefabArray) {
            int id = weapon.GetComponent<WeaponBehavior>().weaponStats.weaponID;
            _weaponPrefabs[id] = weapon;
        }
    }

    public GameObject CreateWeapon(Transform parent, int weaponID) {
        if(_weaponPrefabs.ContainsKey(weaponID)) {
            return Instantiate(_weaponPrefabs[weaponID], parent);
        } else {
            Debug.Log("No weapon with found with given ID!");
            return null;
        }
    }
}

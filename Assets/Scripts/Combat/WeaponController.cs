using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    private Rigidbody2D _body;
    public WeaponBehavior selected {get; private set;} = null;
    private Dictionary<int,WeaponBehavior> _availableWeapons;
    private SpriteRenderer _weaponSprite;
    private CharacterController _characterController;
    [SerializeField] private GameObject _weaponHolder = null;
    [SerializeField] private GameObject[] weaponPrefabs;

    public GameObject weaponHolder {get {return _weaponHolder;}}

    private IEnumerator attackLoop;
    public event Action OnAttack;

    //Sound effects
    /*
    public AudioSource audioPlayer;
    public AudioClip swing1;
    public AudioClip swing2;
    public AudioClip swing3;
    public AudioClip swing4;
    */

    void Awake() {
        _body = GetComponent<Rigidbody2D>();
        _availableWeapons = new Dictionary<int,WeaponBehavior>();
        _characterController = GetComponent<CharacterController>();
    }

    void Start() {
        if (weaponPrefabs.Length > 0) {
            WeaponBehavior weapon = null;
            foreach(GameObject go in weaponPrefabs) {
                GameObject prefab = Instantiate(go, transform);
                weapon = prefab.GetComponent<WeaponBehavior>();
                SetWeapon(weapon);
            }
            selected = weapon;
        }
        attackLoop = AttackLoop();
    }

    public void SetWeapon(WeaponBehavior weapon) {
        if(_availableWeapons.ContainsKey(weapon.weaponStats.weaponID)) {
            _availableWeapons[weapon.weaponStats.weaponID].SetHolder(null);
        }
        weapon.SetHolder((weaponHolder != null) ? weaponHolder : gameObject);
        _availableWeapons[weapon.weaponStats.weaponID] = weapon;
        selected = weapon;
    }

    public void SetWeapon(int weaponID) {
        if(_availableWeapons.ContainsKey(weaponID)) {
            selected = _availableWeapons[weaponID];
        } else {
            GameObject weapon = WeaponManager.instance?.CreateWeapon(transform, weaponID);
            if(weapon != null) {
                SetWeapon(weapon.GetComponent<WeaponBehavior>());
            }
        }
    }

    public void SetWeapons(int[] weaponIDs) {
        foreach(int weaponID in weaponIDs) {
            SetWeapon(weaponID);
        }
    }

    public int[] GetWeapons() {
        int[] weaponIDs = new int[_availableWeapons.Count];
        int i = 0;
        foreach(int id in _availableWeapons.Keys) {
            weaponIDs[i] = id;
            ++i;
        }
        return weaponIDs;
    }

    public WeaponBehavior GetWeapon() {
        return selected;
    }

    public void Attack() {
        if (selected != null) {
            selected.Attack(_characterController);
            OnAttack?.Invoke();
        }
    }

    public void StartAttack() {
        StartCoroutine(attackLoop);
    }

    public void StopAttack() {
        StopCoroutine(attackLoop);
    }

    IEnumerator AttackLoop() {
        yield return null;
        while(true) {
            Attack();
            yield return null;
        }
    }
}

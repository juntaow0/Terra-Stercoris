using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CombatController : MonoBehaviour {

    private Rigidbody2D _body;

    private Weapon _currentWeapon = null;
    public WeaponBase startingWeapon;

    private float spawnDistance;
    private bool onCooldown = false;

    public Weapon currentWeapon {get {return _currentWeapon;}}

    [SerializeField] private SpriteRenderer _weaponSprite;
    private GameObject _weaponObject;

    //Sound effects
    /*
    public AudioSource audioPlayer;
    public AudioClip swing1;
    public AudioClip swing2;
    public AudioClip swing3;
    public AudioClip swing4;
    */

    void Start() {
        _body = GetComponent<Rigidbody2D>();
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if(collider != null) {
            spawnDistance = collider.radius * transform.localScale.x;
        } else {
            spawnDistance = 0.0f;
        }

        _weaponObject = new GameObject("Weapon");
        _weaponObject.transform.SetParent(transform);

        if(startingWeapon != null) {
            SetWeapon(startingWeapon);
        }
    }

    public void SetWeapon(WeaponBase baseWeapon) {
        SetWeapon(new Weapon(baseWeapon));
    }

    public void SetWeapon(Weapon weapon) {
        _currentWeapon = weapon;
        if(_currentWeapon.weaponSprite != null) {
            _weaponSprite.sprite = weapon.weaponBase.weaponSprite;
        }
    }

    public Weapon GetWeapon() {
        return _currentWeapon;
    }

    public void Attack(Vector2 direction) {
        if (_currentWeapon == null || onCooldown) return;
        StartCoroutine(WaitForCooldown());
        switch(_currentWeapon.type) {
            case WeaponType.RANGED:
                Projectile proj = _currentWeapon.GetProjectile();
                proj.gameObject.SetActive(true);
                proj.Fire((Vector2) transform.position + direction.normalized * spawnDistance, direction, _body.velocity, gameObject);
                break;
            case WeaponType.MELEE:
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _currentWeapon.range);
                if(hit.collider != null) {
                    hit.transform.GetComponent<IDamagable>()?.Damage(_currentWeapon.damage);
                }
                _weaponSprite.transform.position = (Vector2) transform.position + direction.normalized * spawnDistance;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                _weaponObject.transform.eulerAngles = new Vector3(0,0,angle);

                StartCoroutine(SwingMelee());
                
                break;
        }
    }

    private IEnumerator SwingMelee() {
        
        _weaponSprite.enabled = true;
        yield return new WaitForSeconds(_currentWeapon.cooldown);
        _weaponSprite.enabled = false;
    }

    private IEnumerator WaitForCooldown() {
        
        onCooldown = true;
        yield return new WaitForSeconds(_currentWeapon.cooldown);
        onCooldown = false;
    }
}

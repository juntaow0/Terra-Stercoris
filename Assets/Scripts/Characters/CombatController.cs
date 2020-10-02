using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    private Rigidbody2D _body;

    [SerializeField] private Weapon _currentWeapon = null;

    private float spawnDistance;
    private bool onCooldown = false;

    public Weapon currentWeapon {get {return _currentWeapon;}}

    private SpriteRenderer _weaponSprite;

    void Start() {
        _body = GetComponent<Rigidbody2D>();
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if(collider != null) {
            spawnDistance = collider.radius * transform.localScale.x;
        } else {
            spawnDistance = 0.0f;
        }

        GameObject weaponObject = new GameObject("Weapon");
        weaponObject.transform.SetParent(transform);
        _weaponSprite = weaponObject.AddComponent<SpriteRenderer>();
        _weaponSprite.sortingOrder = 10;
        _weaponSprite.transform.localScale = new Vector3(1,1,1);
        _weaponSprite.enabled = false;
        if(_currentWeapon.weaponSprite != null) {
            _weaponSprite.sprite = _currentWeapon.weaponSprite;
        }
    }

    public void SetWeapon(WeaponBase baseWeapon) {
        SetWeapon(new Weapon(baseWeapon));
    }

    public void SetWeapon(Weapon weapon) {
        _currentWeapon = weapon;
        _weaponSprite.sprite = weapon.weaponSprite;
    }

    public Weapon GetWeapon() {
        return _currentWeapon;
    }

    public void Attack(Vector2 direction) {
        if(_currentWeapon == null || onCooldown) return;

        StartCoroutine(WaitForCooldown());
        switch(_currentWeapon.type) {
            case WeaponType.RANGED:
                Projectile proj = _currentWeapon.GetProjectile();
                proj.gameObject.SetActive(true);
                proj.Fire((Vector2) transform.position + direction.normalized * spawnDistance, direction, _body.velocity, gameObject);
                break;
            case WeaponType.MELEE:
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _currentWeapon.range);
                if(hit.transform != null) {
                    hit.transform.gameObject.GetComponent<CharacterController>()?.Damage(_currentWeapon.damage);
                }
                _weaponSprite.transform.position = (Vector2) transform.position + direction.normalized * spawnDistance;
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

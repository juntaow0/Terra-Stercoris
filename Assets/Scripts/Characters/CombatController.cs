using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    private Rigidbody2D _body;
    [SerializeField] private Weapon currentWeapon = null;
    [SerializeField] private float spawnDistance = 0.085f;
    private bool onCooldown = false;

    void Start() {
        _body = GetComponent<Rigidbody2D>();
    }

    public void SetWeapon(WeaponBase baseWeapon) {
        SetWeapon(new Weapon(baseWeapon));
    }

    public void SetWeapon(Weapon weapon) {
        currentWeapon = weapon;
    }

    public Weapon GetWeapon() {
        return currentWeapon;
    }

    public void Attack(Vector2 direction) {
        if(currentWeapon == null || onCooldown) return;

        StartCoroutine(WaitForCooldown());
        switch(currentWeapon.type) {
            case WeaponType.RANGED:
                Projectile proj = currentWeapon.GetProjectile();
                proj.gameObject.SetActive(true);
                proj.Fire((Vector2) transform.position + direction.normalized * spawnDistance, direction, _body.velocity, gameObject);
                break;
            case WeaponType.MELEE:
                RaycastHit2D hit = Physics2D.Linecast(transform.position, ((Vector2) transform.position) + direction.normalized * currentWeapon.range);
                if(hit.transform != null) {
                    hit.transform.gameObject.GetComponent<CharacterController>()?.Damage(currentWeapon.damage);
                }
                break;
        }
    }

    private IEnumerator WaitForCooldown() {
        
        onCooldown = true;
        yield return new WaitForSeconds(currentWeapon.cooldown);
        onCooldown = false;
    }
}

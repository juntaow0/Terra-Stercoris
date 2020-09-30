using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    [SerializeField] private WeaponBase baseWeapon;
    private Rigidbody2D _body;
    private Weapon currentWeapon = null;
    private bool onCooldown = false;

    void Start() {
        currentWeapon = new Weapon(baseWeapon);
        _body = GetComponent<Rigidbody2D>();
    }

    public void Attack(Vector2 direction) {
        if(onCooldown) return;

        StartCoroutine(WaitForCooldown());
        switch(currentWeapon.type) {
            case WeaponType.RANGED:
                Projectile proj = currentWeapon.GetProjectile();
                proj.gameObject.SetActive(true);
                proj.Fire(transform.position, direction, _body.velocity, gameObject);
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

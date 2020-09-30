using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    private Weapon currentWeapon = null;
    private bool onCooldown = false;

    void Start() {
        currentWeapon = new Weapon("Test Weapon", WeaponType.MELEE, 5, 1, 1);
    }

    public void Attack(Vector2 direction) {
        if(onCooldown) return;

        StartCoroutine(WaitForCooldown());
        switch(currentWeapon.Type) {
            case WeaponType.RANGED:
                break;
            case WeaponType.MELEE:
                RaycastHit2D hit = Physics2D.Linecast(transform.position, ((Vector2) transform.position) + direction.normalized * currentWeapon.Range);
                if(hit.transform != null) {
                    hit.transform.gameObject.GetComponent<CharacterController>()?.AddHealth(-currentWeapon.Damage);
                }
                break;
        }
    }

    private IEnumerator WaitForCooldown() {
        
        onCooldown = true;

        yield return new WaitForSeconds(currentWeapon.Speed);

        onCooldown = false;
    }
}

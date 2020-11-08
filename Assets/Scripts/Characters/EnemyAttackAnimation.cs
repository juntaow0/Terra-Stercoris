using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAnimation : MonoBehaviour
{
    [SerializeField] private Animator weaponHolderAnimator;
    private CharacterController characterController;
    private WeaponController weaponController;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        weaponController = GetComponent<WeaponController>();
    }

    private void OnEnable() {
        weaponController.OnAttack += AttackAnimation;
    }

    private void OnDestroy() {
        weaponController.OnAttack -= AttackAnimation;
    }

    void AttackAnimation() {
        Quaternion newRot = Quaternion.Euler(0, 0, Mathf.Atan2(characterController.rotation.y, characterController.rotation.x) * Mathf.Rad2Deg);
        weaponController.weaponHolder.transform.rotation = newRot;
        weaponHolderAnimator.SetTrigger("attack");
    }
}

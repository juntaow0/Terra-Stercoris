using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TestEnemyAI : MonoBehaviour
{
    private CharacterController cc;
    private WeaponController wc;
    private Animator animator;
    private AIPath aiPath;
    private AIDestinationSetter AIDest;
    private bool seesTarget = false;

    private void Awake() {
        aiPath = GetComponent<AIPath>();
        AIDest = GetComponent<AIDestinationSetter>();
        cc = GetComponent<CharacterController>();
        wc = GetComponent<WeaponController>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        StartCoroutine(FindTarget());
    }

    public void SetTarget(Transform target) {
        AIDest.target = target;
    }

    private void Update() {
        if (AIDest.target == null) {
            return;
        }
        CalculateHeading();
        float speed = aiPath.desiredVelocity.magnitude;
        animator.SetFloat("speed", speed);
        if (Vector2.Distance(transform.position, AIDest.target.position) <= wc.selected.weaponStats.range) {
            if (seesTarget) {
                wc.Attack();
                animator.SetTrigger("attack");
            }
        }
    }

    void CalculateHeading() {
        Vector2 dir = AIDest.target.position - transform.position;
        cc.rotation = dir;
    }

    void Die() {
        AIDest.target = null;
        StopCoroutine(FindTarget());
    }


    void OnEnable() {
        cc.OnDeath += Die;
    }

    void OnDisable() {
        cc.OnDeath -= Die;
    }

    void OnDestroy() {
        OnDisable();
    }

    IEnumerator FindTarget() {
        while (true) {
            if (AIDest.target == null) {
                yield return null;
            } else {
                RaycastHit2D hit = Physics2D.Linecast(transform.position, AIDest.target.position);
                seesTarget = hit.collider != null && hit.transform == AIDest.target;
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}

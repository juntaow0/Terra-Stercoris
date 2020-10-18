using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TestEnemyAI : MonoBehaviour
{
    private CharacterController cc;
    private Animator animator;
    private WeaponController wc;
    private Transform targetPosition;
    private AIPath aiPath;
    private AIDestinationSetter AIDest;
    private Transform model;
    private bool seesTarget = false;

    private void Awake() {
        aiPath = GetComponent<AIPath>();
        AIDest = GetComponent<AIDestinationSetter>();
        cc = GetComponent<CharacterController>();
        wc = GetComponent<WeaponController>();
        animator = GetComponent<Animator>();
        model = transform.GetChild(0);
    }

    private void Start() {
        StartCoroutine(FindTarget());
    }

    public void SetTarget(Transform target) {
        AIDest.target = target;
        targetPosition = target;
    }

    private void Update() {
        if (AIDest.target == null) {
            return;
        }
        Vector2 dir = Vector3.Normalize(aiPath.desiredVelocity);
        cc.rotation = dir;
        if (dir.x >= 0) {
            model.localScale = new Vector3(5,5,0);
        } else {
            model.localScale = new Vector3(-5, 5,0);
        }
        animator.SetFloat("speed", dir.magnitude);
        if (Vector2.Distance(transform.position, AIDest.target.position) <= wc.selected.weaponStats.range) {
            animator.SetTrigger("attack");
            if (seesTarget) {
                Debug.Log("can see");
                wc.Attack();
                
            }
        }
    }

    void Die() {
        AIDest.target = null;
        targetPosition = null;
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
                seesTarget = hit.collider != null && hit.collider.gameObject == AIDest.target;
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}

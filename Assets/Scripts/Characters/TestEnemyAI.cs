using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TestEnemyAI : MonoBehaviour
{
    public int maxHealth;
    private CharacterController cc;
    private WeaponController wc;
    private Animator animator;
    private AIPath aiPath;
    private AIDestinationSetter AIDest;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private bool seesTarget = false;
    private CharacterController targetCC;
    private bool hostile = true;

    private void Awake() {
        aiPath = GetComponent<AIPath>();
        AIDest = GetComponent<AIDestinationSetter>();
        cc = GetComponent<CharacterController>();
        wc = GetComponent<WeaponController>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (AIDest.target != null) {
            targetCC = AIDest.target.GetComponent<CharacterController>();
        }
    }

    private void Start() {
        cc.health.max = maxHealth;
        cc.health.quantity = maxHealth;
        StartCoroutine(FindTarget());
    }

    public void SetTarget(Transform target) {
        AIDest.target = target;
        targetCC = AIDest.target.GetComponent<CharacterController>();
    }

    public void SetHostile(bool state) {
        hostile = state;
        if (state) {
            aiPath.endReachedDistance = 1.4f;
            aiPath.pickNextWaypointDist = 1.4f;
            aiPath.slowdownDistance = 0.6f;
        } else {
            aiPath.endReachedDistance = 2.2f;
            aiPath.pickNextWaypointDist = 2.2f;
            aiPath.slowdownDistance = 2.2f;
        }
    }

    private void Update() {
        if (AIDest.target == null) {
            return;
        }
        CalculateHeading();
        float speed = aiPath.desiredVelocity.magnitude;
        animator.SetFloat("speed", speed);
        if (hostile) {
            if (Vector2.Distance(transform.position, AIDest.target.position) <= wc.selected.weaponStats.range) {
                if (seesTarget) {
                    wc.Attack();
                    if (targetCC.GetHealth() <= 0) {
                        AIDest.target = null;
                        targetCC = null;
                    }
                }
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
        animator.SetTrigger("die");
        boxCollider.enabled = false;
        StartCoroutine(FadeOut(null));
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

    IEnumerator FadeOut(Action Callback) {
        float fadeTime = 1.5f;
        float spriteAlpha = spriteRenderer.color.a;
        float fadeDelta = spriteAlpha * Time.deltaTime / fadeTime;
        Color tempColor = spriteRenderer.color;
        while (tempColor.a > 0) {
            tempColor.a -= fadeDelta;
            spriteRenderer.color = tempColor;
            yield return null;
        }
        Callback?.Invoke();
    }
}

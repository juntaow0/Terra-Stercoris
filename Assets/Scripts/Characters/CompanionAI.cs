using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CompanionAI : MonoBehaviour
{
    public int maxHealth;
    private CharacterController cc;
    private Animator animator;
    private AIPath aiPath;
    private AIDestinationSetter AIDest;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        aiPath = GetComponent<AIPath>();
        AIDest = GetComponent<AIDestinationSetter>();
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        cc.health.max = maxHealth;
        cc.health.quantity = maxHealth;
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
    }

    void CalculateHeading() {
        Vector2 dir = AIDest.target.position - transform.position;
        cc.rotation = dir;
    }

    void Die() {
        AIDest.target = null;
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

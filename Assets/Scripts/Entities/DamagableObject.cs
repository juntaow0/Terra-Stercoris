using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObject : MonoBehaviour,IDamagable
{
    public int maxHealth;
    public NumericalResource health;
    public Color hitTint;
    
    public bool IsAlive { get; private set; }
    private bool _inAnimation;
    
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;
    private AudioController ac;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        ac = GetComponent<AudioController>();
        health = new NumericalResource(ResourceType.Health,0, maxHealth);
        IsAlive = true;
        _inAnimation = false;
    }
    public void Damage(int amount) {
        if (IsAlive) {
            health.quantity -= amount;
            // Potentially add damage animation or event.
            StartCoroutine(DamageAnimation());
            ac.Play("Rock");
        }
    }

    public void Die() {
        health.OnResourceDepleted -= Die;
        collider.enabled = false;
        StopAllCoroutines();
        StartCoroutine(FadeOut(() => {
            gameObject.SetActive(false);
        }));
    }

    void OnEnable() {
        health.OnResourceDepleted += Die;
    }

    void OnDisable() {
        health.OnResourceDepleted -= Die;
    }

    void OnDestroy() {
        OnDisable();
    }

    IEnumerator DamageAnimation() {
        if (!_inAnimation) {
            _inAnimation = true;

            float blinkSpeed = 0.2f;

            Color previousColor = spriteRenderer.color;

            spriteRenderer.color = hitTint;
            yield return new WaitForSeconds(blinkSpeed);
            if (IsAlive) spriteRenderer.color = previousColor;
            yield return new WaitForSeconds(blinkSpeed);

            _inAnimation = false;
        }
    }
    
    IEnumerator FadeOut(Action Callback) {
        
        float fadeTime = 1f;
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

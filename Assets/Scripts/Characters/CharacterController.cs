﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private const int NUM_ROTATIONS = 8;

    public NumericalResource health = new NumericalResource(ResourceType.Health);
    public NumericalResource energy = new NumericalResource(ResourceType.Energy);

    public event Action OnDeath;

    [SerializeField] private float _movementSpeed = 15.0f;
    [SerializeField] private int _spriteRotation = 0;
    public bool IsAlive {get; private set;} = true;

    [SerializeField] private Rigidbody2D _body = null;
    [SerializeField] private SpriteRenderer _spriteRenderer = null;

    private Color deathTint = Color.red;
    private Color hitTint = Color.red;

    private bool _inAnimation = false;

    // Add property for velocity
    public Vector2 velocity {get {return _body.velocity;} private set {_body.velocity = value;}}

    void Start() {
        if(_body == null) _body = GetComponent<Rigidbody2D>();
        if(_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
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

    public void Move(Vector2 newVelocity) {
        velocity = newVelocity.normalized * _movementSpeed;
    }

    public float GetSpeed() {
        return _movementSpeed;
    }

    public void SetSpeed(float newSpeed) {
        _movementSpeed = newSpeed;
    }

    public int GetSpriteRotation() {
        return _spriteRotation;
    }

    public void SetSpriteRotation(int rotation) {
        _spriteRotation = rotation;
    }

    public void SetSpriteRotation(Vector2 rotation) {
        _spriteRotation = (NUM_ROTATIONS + (int) Mathf.Round(Mathf.Atan2(rotation.y, rotation.x) /
                (2*Mathf.PI) * NUM_ROTATIONS)) % NUM_ROTATIONS;
    }

    public int GetHealth() {
        return health.quantity;
    }

    public void AddHealth(int amount) {
        health.quantity += amount;
    }

    public void Damage(int amount) {
        if(IsAlive) {
            health.quantity -= amount;
            // Potentially add damage animation or event.
            StartCoroutine(DamageAnimation());
        }
    }

    public void Die() {
        _spriteRenderer.color = deathTint;
        IsAlive = false;
        OnDeath?.Invoke();
    }

    public void SetHealth(int newHealth) {
        health.quantity = newHealth;
    }

    public int GetEnergy() {
        return energy.quantity;
    }

    public void AddEnergy(int amount) {
        energy.quantity += amount;
    }

    public void SetEnergy(int newEnergy) {
        energy.quantity = newEnergy;
    }

    public void Kill() {
        health.quantity = 0;
    }

    IEnumerator DamageAnimation() {
        
        if(!_inAnimation) {
            _inAnimation = true;

            float blinkSpeed = 0.1f;

            Color previousColor = _spriteRenderer.color;

            _spriteRenderer.color = hitTint;
            yield return new WaitForSeconds(blinkSpeed);
            if(IsAlive) _spriteRenderer.color = previousColor;
            yield return new WaitForSeconds(blinkSpeed);

            _inAnimation = false;
        }
    }
}

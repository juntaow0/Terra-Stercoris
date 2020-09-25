using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public NumericalResource health = new NumericalResource(ResourceType.Health);
    public NumericalResource energy = new NumericalResource(ResourceType.Energy);

    [SerializeField] private float _movementSpeed = 15.0f;
    [SerializeField] private int _spriteRotation = 0;

    [SerializeField] private Rigidbody2D _body = null;

    // Add property for velocity
    public Vector2 velocity {get {return _body.velocity;} private set {_body.velocity = value;}}

    void Start() {
        if(_body == null) {
            _body = GetComponent<Rigidbody2D>();
        }
    }

    public void Move(Vector2 newVelocity) {
        velocity = newVelocity;
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

    public int GetHealth() {
        return health.quantity;
    }

    public void AddHealth(int amount) {
        health.quantity += amount;
    }

    public void SetHealth(int newHealth) {
        health.quantity = newHealth;
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
}

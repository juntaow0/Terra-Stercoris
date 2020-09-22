using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour {

    // Declare components
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
}

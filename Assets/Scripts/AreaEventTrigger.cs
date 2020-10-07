using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class AreaEventTrigger : MonoBehaviour
{
    public UnityEvent Events;
    private BoxCollider2D boxCollider;
    private void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Events?.Invoke();
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class AreaEventTrigger : MonoBehaviour
{
    public bool selfDestruct;
    public bool requirePlayer;
    public string exclusiveTag;
    public UnityEvent Events;
    public UnityEvent OnLeaveEvents;
    private BoxCollider2D boxCollider;
    private void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (exclusiveTag != "" && exclusiveTag!=null) {
            if (collision.tag == exclusiveTag) {
                Events?.Invoke();
                if (selfDestruct) {
                    gameObject.SetActive(false);
                }
            }
            return;
        }
        if(!requirePlayer || (PlayerController.instance != null && collision.gameObject == PlayerController.instance.gameObject)) {
            Events?.Invoke();
            if (selfDestruct) {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (exclusiveTag != "") {
            if (collision.tag == exclusiveTag) {
                OnLeaveEvents?.Invoke();
                if (selfDestruct) {
                    gameObject.SetActive(false);
                }
            }
            return;
        }
        if (!requirePlayer || (PlayerController.instance != null && collision.gameObject == PlayerController.instance.gameObject)) {
            OnLeaveEvents?.Invoke();
            if (selfDestruct) {
                gameObject.SetActive(false);
            }
        }
    }
}

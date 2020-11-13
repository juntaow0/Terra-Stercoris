using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SiphonScale : MonoBehaviour, ISiphonable {

    [SerializeField] float maxScale;
    [SerializeField] float minScale;
    [SerializeField] float scalePerHealth;

    [SerializeField] UnityEvent onMaxScale;
    [SerializeField] UnityEvent onMinScale;
    [SerializeField] UnityEvent leaveMaxScale;
    [SerializeField] UnityEvent leaveMinScale;

    float scale {get {return transform.localScale.x;} set {
        if(value >= maxScale) {
            value = maxScale;
            onMaxScale?.Invoke();
        } else if(value <= minScale) {
            value = minScale;
            onMinScale?.Invoke();
        } else if(scale == minScale) {
            leaveMinScale?.Invoke();
        } else if(scale == maxScale) {
            leaveMaxScale?.Invoke();
        }
        transform.localScale = new Vector2(value, value);
    }}

    public bool IsSiphonable {get {return scale > minScale;} set {}}
    public bool IsShareable {get {return scale < maxScale;} set{}}

    public void Siphon(int amount) {
        Debug.Log(amount);
        scale -= amount * scalePerHealth;
    }
}

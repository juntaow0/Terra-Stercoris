using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SiphonScale : MonoBehaviour, ISiphonable {

    [SerializeField] float maxScale;
    [SerializeField] float minScale;
    [SerializeField] float scalePerHealth;
    [SerializeField] float scaleSpeed = 3.0f;

    [SerializeField] UnityEvent onMaxScale;
    [SerializeField] UnityEvent onMinScale;
    [SerializeField] UnityEvent leaveMaxScale;
    [SerializeField] UnityEvent leaveMinScale;

    float _scale;
    float visualScale;

    void Start() {
        scale = visualScale = transform.localScale.x;
    }

    void Update() {
        if(scale != visualScale) {
            float difference = (scale - visualScale) * scaleSpeed;
            if(Mathf.Abs(difference) / scale > 0.01) {
                visualScale = visualScale + difference * Time.deltaTime;
            } else {
                visualScale = scale;
            }
            transform.localScale = new Vector2(visualScale, visualScale);
        }
    }

    float scale {get {return _scale;} set {
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
        _scale = value;
    }}

    public bool IsSiphonable {get {return scale > minScale;} set {}}
    public bool IsShareable {get {return scale < maxScale;} set{}}

    public void Siphon(int amount) {
        scale -= amount * scalePerHealth;
    }
}

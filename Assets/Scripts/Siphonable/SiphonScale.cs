using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiphonScale : MonoBehaviour, ISiphonable {

    [SerializeField] float maxScale;
    [SerializeField] float minScale;
    [SerializeField] float scalePerHealth;

    float scale {get {return transform.localScale.x;} set {transform.localScale = new Vector2(value, value);}}

    public bool IsSiphonable {get {return scale > minScale;} set {}}
    public bool IsShareable {get {return scale < maxScale;} set{}}

    public void Siphon(int amount) {
        Debug.Log(amount);
        scale -= amount * scalePerHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SiphonInteraction : MonoBehaviour, ISiphonable {

    [SerializeField] int _health = 10;
    [SerializeField] int maxHealth = 20;

    [SerializeField] UnityEvent onMaxHealth;
    [SerializeField] UnityEvent onMinHealth;
    [SerializeField] UnityEvent onMiddleHealth;

    public int health {get {return _health;} set {
        if(value >= maxHealth) {
            value = maxHealth;
            onMaxHealth?.Invoke();
        } else if(value <= 0) {
            value = 0;
            onMinHealth?.Invoke();
        } else {
            onMiddleHealth?.Invoke();
        }
        _health = value;
    }}    

    public bool IsSiphonable {get {return health > 0;} set {}}
    public bool IsShareable {get {return health < maxHealth;} set{}}

    public void Siphon(int amount) {
        health -= amount;
    }
}

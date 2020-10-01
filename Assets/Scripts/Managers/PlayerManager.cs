using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerController player;
    public static PlayerManager instance {get; private set;}

    void Awake() {
        instance = this;
        player = FindObjectOfType<PlayerController>();
    }
}

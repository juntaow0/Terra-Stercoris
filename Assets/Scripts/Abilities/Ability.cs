using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Ability {

    [SerializeField] private ActionTemplate _template;
    public UnityEvent startAction;
    public UnityEvent stopAction;

    [field: SerializeField] public bool OnCooldown {get; private set;} = false;
}

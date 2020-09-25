using System;
using UnityEngine;

public enum ResourceType {
    Health,
    Energy
}

public class NumericalResource {
    public event Action<int> OnResourceUpdated;

    public ResourceType resourcetype {get; private set;}
    public int min {get; set;} // Just in case we need to put constraints on this
    public int max {get; set;}

    private int _quantity;
    public int quantity {
        get {return _quantity;}
        set {
            if(value > max) {
                _quantity = max;
            } else if(value < min) {
                _quantity = min;
            } else {
                _quantity = value;
            }
            OnResourceUpdated?.Invoke(_quantity);
        }
    }

    public NumericalResource(ResourceType type, int min = 0, int max = 100) {
        this.resourcetype = type;
        this.min = min;
        this.max = max;
        this._quantity = max;
    }
}

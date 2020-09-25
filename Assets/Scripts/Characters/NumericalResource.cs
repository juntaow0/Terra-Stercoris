using UnityEngine;

public enum ResourceType {
    Health,
    Energy
}

public class NumericalResource {

    public delegate void OnResourceUpdatedHandler(int currentResource);

    public event OnResourceUpdatedHandler OnResourceUpdated;

    public ResourceType resourcetype {get; private set;}
    public int min {get; set;} // Just in case we need to put constraints on this
    public int max {get; set;}

    public int quantity {
        get {return quantity;}
        set {
            if(value > max) {
                quantity = max;
            } else if(value < min) {
                quantity = min;
            } else {
                quantity = value;
            }
            OnResourceUpdated(quantity);
        }
    }

    public NumericalResource(ResourceType type, int min = 0, int max = 100) {
        this.resourcetype = type;
        this.min = min;
        this.max = max;
    }
}

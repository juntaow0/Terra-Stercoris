using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public string name {get; protected set;}

    public Item(string name) {
        this.name = name;
    }
}

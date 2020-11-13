using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    
    private Dictionary<int,List<IItem>> inventories;

    public static InventoryManager instance {get; private set;}

    void Awake() {
        instance = this;

        inventories = new Dictionary<int,List<IItem>>();
    }

    public void Bind(int id) {
        if(!inventories.ContainsKey(id)) {
            inventories[id] = new List<IItem>();
        }
    }

    public List<IItem> GetItems(int id) {
        return inventories[id];
    }
}

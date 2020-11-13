using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    [SerializeField] int inventoryID;
    public List<IItem> inventory {get {return InventoryManager.instance.GetItems(inventoryID);}}

    void Start() {
        InventoryManager.instance.Bind(inventoryID);
    }

    public void Take(IItem item) {
        inventory.Add(item);
    }

    public void Drop(IItem item) {
        inventory.Remove(item);
    }

    public void DropByName(string name) {
        for(int i = 0; i < inventory.Count; ++i) {
            if(inventory[i].ItemName == name) {
                inventory.RemoveAt(i);
                break;
            }
        }
    }

    public bool HasItem(IItem item) {
        return inventory.Contains(item);
    }

    public bool HasItemByName(string name) {
        foreach(IItem item in inventory) {
            if(item.ItemName == name) {
                return true;
            }
        }
        return false;
    }
}

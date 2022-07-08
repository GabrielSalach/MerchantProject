using UnityEngine;
using System;
using System.Collections.Generic;



[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Inventory/Item", order = 1)]
public class Item : ScriptableObject
{
    public UInt16 id;
    public string itemName;
    public Sprite sprite;
    public int tier;
    public int basePrice;

    // Used to sort a list of items based on id
    public static int CompareId(Item x, Item y) {
        int returnValue = 0;
        if(x == null) {
            Debug.Log("Item " + x.itemName + "has no ID !");
        } else if(y == null) {
            Debug.Log("Item " + y.itemName + "has no ID !");
        } else if(x.id > y.id) {
            returnValue = 1;
        } else if(x.id < y.id) {
            returnValue = -1;
        }
        return returnValue;
    }

}
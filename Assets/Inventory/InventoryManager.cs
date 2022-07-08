using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    public static List<Item> allItems;
    public static int maxItemSlots = 100;
    static InventoryWindow window;

    void Awake() {
        UnityEngine.Object[] loadedItems = (UnityEngine.Object[]) Resources.LoadAll("Items", typeof (Item)); 
        allItems = new List<Item>();
        foreach(UnityEngine.Object loadedItem in loadedItems) {
            allItems.Add((Item) loadedItem);
        }
        allItems.Sort(Item.CompareId);
    }

    void Start() {
        window = InventoryWindow.instance;
    }

    /* Adds the specified amount of items in the inventory and returns a value based on the error
        error code 0 : everything went fine !
        error code 1 : not enough space in the inventory
    */
    public static int AddItem(Item item, UInt16 amount) {
        int errorCode = 0;
        int itemIndex = GetItemIndex(item);
        // Item already exists
        if(itemIndex > -1) {
            UInt16 newCount = GetItemCount(item);
            newCount += amount;
            if(newCount <= UInt16.MaxValue) {
                SavefileManager.WriteSavefile(BitConverter.GetBytes(newCount), itemIndex * 4 + 2, 2);
            } else {
                errorCode = -1;
            }
        } else {
            // Checks for the first empty slot
            int emptySlot = GetEmptySlot();
            if(emptySlot == -1) {
                errorCode = -1;
            } else {
                Byte[] itemIDBytes = BitConverter.GetBytes(item.id);
                Byte[] amountBytes = BitConverter.GetBytes(amount);
                Byte[] bytesToWrite = itemIDBytes.Concat(amountBytes).ToArray();

                SavefileManager.WriteSavefile(bytesToWrite, emptySlot * 4, 4);
            }
        }
        window.UpdateInventory();
        return errorCode;
    }

    /* Removes the specified amount of items in the inventory and returns a value based on the error
        error code 0 : everything went fine !
        error code 1 : the amount of items to be removed is too big ! Nothing changed
    */
    public static int RemoveItem(Item item, UInt16 amount) {
        int errorCode = 0;
        UInt16 currentAmount = GetItemCount(item);
        // If there isn't enough items
        if(currentAmount < amount) {
            errorCode = -1;
        } else {
            currentAmount -= amount;
            SavefileManager.WriteSavefile(BitConverter.GetBytes(currentAmount), GetItemIndex(item) * 4 + 2, 2);
        }
        window.UpdateInventory();
        return errorCode;
    }

    // Returns the index of the first empty slot available or -1 if the inventory is full (0 < index < maxItemSlots)
    static int GetEmptySlot() {
        int firstEmptySlot = 0;
        for(int i = 0; i < maxItemSlots; i++) {
            //Checks if the slots count value is > 0
            if(BitConverter.ToUInt16(SavefileManager.ReadSavefile(i*4 + 2, 2), 0) > 0)
                firstEmptySlot++;
            else
                break;
        }
        if(firstEmptySlot == maxItemSlots)
            return -1;
        else 
            return firstEmptySlot;
    }


    // Returns the index of the first occurence of the item in the inventory, -1 if it doesn't exist (0 < index < maxItemSlots)
    public static int GetItemIndex(Item item) {
        int index = -1;
        for(int i = 0; i < maxItemSlots; i++) {
            // Checks if the slots id value is == item.id
            if(BitConverter.ToUInt16(SavefileManager.ReadSavefile(i*4, 2), 0) == item.id)
                index = i;
        }
        return index;
    }

    // Returns the amount of a specific item in the inventory
    public static UInt16 GetItemCount(Item item) {
        int itemIndex = GetItemIndex(item);
        UInt16 itemCount = 0;
        if(itemIndex > -1) {
            Byte[] bytes = new Byte[2];
            bytes = SavefileManager.ReadSavefile(itemIndex*4+2, 2);
            itemCount = BitConverter.ToUInt16(bytes, 0);
        }
        return itemCount;
    }

    // Returns all items present in the inventory
    public static Dictionary<Item, UInt16> GetAllItems() {
        Dictionary<Item, UInt16> items = new Dictionary<Item, UInt16>();
        for(int i = 0; i < maxItemSlots; i++) {
            Byte[] currentItemSlot = SavefileManager.ReadSavefile(i * 4, 4);
            UInt16 itemCount = BitConverter.ToUInt16(currentItemSlot, 2);
            if(itemCount > 0) {
                UInt16 itemId = BitConverter.ToUInt16(currentItemSlot, 0);
                Item item = allItems[itemId];
                items.Add(item, itemCount);
            }
        }
        return items;
    }

    public static int MaxCraftableItems(Recipe recipe) {
        int count = 999;
        // List containing all ingredients items present in the inventory
        List<int> itemCount = new List<int>();
        // Fill itemCount with all possible ingredients
        foreach(Recipe.ItemCount item in recipe.ingredients) {
            itemCount.Add(GetItemCount(item.item));
        }
        for(int i = 0; i < itemCount.Count; i++){
            int divider = itemCount[i] / recipe.ingredients[i].count;
            if (divider < count) {
                count = divider;
            }
        }
        return count;
    }


    public static void AddOneItem(Item item) {
        AddItem(item, 1);
    }

    public static void RemoveOneItem(Item item) {
        RemoveItem(item, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class InventoryWindow : Window
{    
    // Singleton instance;
    public static InventoryWindow instance;
    // All item slots;
    public List<ItemSlot> itemSlots;

    // Item Slot Prefab
    [SerializeField] 
    GameObject itemSlotPrefab;

    // Selected Item slot index
    ItemSlot selectedItemSlot;
    // Sell button
    [SerializeField]
    Button sellButton;

    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }


    new void Start() {
        // Instantiates all item slots
        itemSlots = new List<ItemSlot>();
        Transform itemSlotsParent = transform.Find("Scroll").Find("Viewport").Find("Content");
        for(int i = 0; i < InventoryManager.maxItemSlots; i++) {
            int index = i;
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemSlotsParent);
            Button btn = itemSlot.GetComponent<Button>();
            btn.enabled = true;
            btn.onClick.AddListener(delegate {SelectItemSlot(itemSlot.GetComponent<ItemSlot>());});
            itemSlots.Add(itemSlot.GetComponent<ItemSlot>());
        }
        

        UpdateInventory();
        base.Start();
    }

    new void Update() {
        base.Update();
        if(EventSystem.current.currentSelectedGameObject == null) {
            selectedItemSlot = null;
            sellButton.gameObject.SetActive(false);
        }
    }

    public void UpdateInventory() {
        int currentItemSlot = 0; 

        // Clear les cases de l'inventaire
        foreach(ItemSlot itemSlot in itemSlots) {
            itemSlot.SetItemSlot(null, 0);
        }
        // Pour chaque item, remplit les cases
        foreach(KeyValuePair<Item, UInt16> item in InventoryManager.GetAllItems()) {
            //Assigne le sprite et le texte.
            itemSlots[currentItemSlot].SetItemSlot(item.Key, item.Value);
            currentItemSlot++;
        }
    }   

    void SelectItemSlot(ItemSlot itemSlot) {
        selectedItemSlot = itemSlot;
        if(selectedItemSlot.GetItem() != null) {
            sellButton.gameObject.SetActive(true);
            sellButton.onClick.RemoveAllListeners();
            sellButton.onClick.AddListener(delegate {TriggerSellingPopup();});
        } else {
            sellButton.gameObject.SetActive(false);
        }
    }

    // Returns the selected item
    public ItemSlot GetSelectedItem() {
        return selectedItemSlot;
    } 

    void TriggerSellingPopup() {
        if(selectedItemSlot.GetItem() != null) {
            SellingWindowPopup.instance.SetItem(selectedItemSlot.GetItem());
            OpenPopup(SellingWindowPopup.instance);
        }
    }
}

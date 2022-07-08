using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class HeroEquipmentSelectionWindow : Window
{    
    // Singleton instance;
    public static HeroEquipmentSelectionWindow instance;
    // All item slots;
    public List<ItemSlot> itemSlots;

    // Item Slot Prefab
    [SerializeField] 
    GameObject itemSlotPrefab;

    // Selected Item slot index
    ItemSlot selectedItemSlot;
    
    [SerializeField] HeroEquipmentSlot selectedEquipmentSlot;
    [SerializeField] Button equipButton;
    HeroEquipment.EquipSlot equipSlotFilter;
    int equipmentSlotIndex;

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
        }
    }

    public override void OpenWindow() {
        UpdateInventory();
        base.OpenWindow();
    }

    public void UpdateInventory() {
        int currentItemSlot = 0; 
        selectedEquipmentSlot.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        // Clear les cases de l'inventaire
        foreach(ItemSlot itemSlot in itemSlots) {
            itemSlot.SetItemSlot(null, 0);
        }
        // Pour chaque item, remplit les cases
        if(equipSlotFilter != HeroEquipment.EquipSlot.None)  {
            foreach(KeyValuePair<Item, UInt16> item in InventoryManager.GetAllItems()) {
                //Assigne le sprite et le texte.
                if(item.Key.GetType() == typeof(HeroEquipment)) {
                    HeroEquipment equipment = (HeroEquipment) item.Key;
                    if(equipment.equipSlot == equipSlotFilter) {
                        itemSlots[currentItemSlot].SetItemSlot(item.Key, item.Value);
                        currentItemSlot++;
                    }
                }
            }
        }
    }   

    void SelectItemSlot(ItemSlot itemSlot) {
        selectedItemSlot = itemSlot;
        if(selectedItemSlot.GetItem() != null) {
            selectedEquipmentSlot.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(true);
            selectedEquipmentSlot.SetItem((HeroEquipment) selectedItemSlot.GetItem());
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(delegate {HeroInventoryWindow.instance.SetItem(selectedEquipmentSlot.GetEquipment(), equipmentSlotIndex); this.CloseWindow();});
        } else {
            selectedEquipmentSlot.SetRawData(null, "", "");
            selectedEquipmentSlot.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
        }
    }

    // Returns the selected item
    public ItemSlot GetSelectedItem() {
        return selectedItemSlot;
    } 

    public void SetEquipmentSlot(int slot) {
        equipmentSlotIndex = slot;
        switch(slot) {
            case 0:
                equipSlotFilter = HeroEquipment.EquipSlot.RightHand;
                break;
            case 1:
                equipSlotFilter = HeroEquipment.EquipSlot.LeftHand;
                break;
            case 2:
                equipSlotFilter = HeroEquipment.EquipSlot.Helmet;
                break;
            case 3:
                equipSlotFilter = HeroEquipment.EquipSlot.Armor;
                break;
            case 4:
                equipSlotFilter = HeroEquipment.EquipSlot.Gloves;
                break;
            case 5:
                equipSlotFilter = HeroEquipment.EquipSlot.Boots;
                break;
            case 6:
                equipSlotFilter = HeroEquipment.EquipSlot.Trinket;
                break;
            case 7:
                equipSlotFilter = HeroEquipment.EquipSlot.Trinket;
                break;
            default:
                break;
        }
    }

}


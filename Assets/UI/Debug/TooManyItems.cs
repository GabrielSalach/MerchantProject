using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooManyItems : Window
{
    [SerializeField]
    GameObject TMIItemSlot;


    new void Start()
    {
        base.Start();
        foreach(Item item in InventoryManager.allItems) {
            GameObject slot = Instantiate(TMIItemSlot, transform.Find("Scroll").Find("Viewport").Find("Content"));
            slot.transform.Find("itemSprite").GetComponent<Image>().sprite = item.sprite;
            slot.transform.Find("Add").GetComponent<Button>().onClick.AddListener(delegate {InventoryManager.AddItem(item, 1);});
            slot.transform.Find("Remove").GetComponent<Button>().onClick.AddListener(delegate {InventoryManager.RemoveItem(item, 1);});
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SellingWindowPopup : Window
{
    // Singleton instance
    public static SellingWindowPopup instance;

    // Current item amount to sell
    UInt16 count;
    // Max amount of item
    int maxCount;
    // Current item
    Item selectedItem;
    // Reference to count text 
    Text countText;
    // Reference to item image
    Image itemImage;

    // Singleton logic
    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    new void Start() {
        base.Start();
        count = 1;
        transform.Find("Add").GetComponent<Button>().onClick.AddListener(delegate {AddCount();});
        transform.Find("Remove").GetComponent<Button>().onClick.AddListener(delegate {RemoveCount();});
        countText = transform.Find("Count").Find("Text").GetComponent<Text>();

        itemImage = transform.Find("ItemSlot").Find("Item Sprite").GetComponent<Image>();
        transform.Find("ItemSlot").GetComponentInChildren<Text>().text = "";
    }


    // Sets recipe graphic and craft button behavior
    public void SetItem(Item item) {
        selectedItem = item;
        maxCount = InventoryManager.GetItemCount(item);
        count = 1;
        transform.Find("Sell").GetComponent<Button>().onClick.RemoveAllListeners();
        transform.Find("Sell").GetComponent<Button>().onClick.AddListener(delegate{SellItem(selectedItem, count); CloseWindow();});
        SetCountText();
        SetItemImage();
    }

	void SellItem(Item item, int count) {
		MoneyBag.instance.AddMoney(item.basePrice * count);
		InventoryManager.RemoveItem(item, Convert.ToUInt16(count));
	}

    // Adds 1 to the count until the maximum item craftable
    void AddCount() {
        if(count < maxCount) {
            count ++;
        }
        SetCountText();
    }

    // Removes 1 to the count or jump to max item craftable
    void RemoveCount() {
        count--;
        if(count == 0) {
            count = Convert.ToUInt16(maxCount);
        }
        SetCountText();
    }

    void SetItemImage() {
        itemImage.sprite = selectedItem.sprite;
    }

    void SetCountText() {
        countText.text = count.ToString();
    } 
}

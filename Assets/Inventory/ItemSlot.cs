using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    // Item reference
    Item item;
    // Count
    UInt16 count;

    // Reference to the item sprite
    [SerializeField] Image itemSprite;
    // Reference to the count text
    [SerializeField] Text countText;

    void Awake() {
        //itemSprite = transform.Find("Item Sprite").GetComponent<Image>();
        //countText = transform.GetComponentInChildren<Text>();
        UpdateGraphics();
    }

    public void SetItemSlot(Item item, UInt16 count) {
        if(item == null) {
            itemSprite.color = Color.clear;
        } else {
            itemSprite.color = Color.white;
        }
        this.item = item;
        this.count = count;
        UpdateGraphics();
    }

    public void UpdateGraphics() {
        if(item != null)
            itemSprite.sprite = item.sprite;
        else
            itemSprite.sprite = null;
        if(count != 0)
            countText.text = count.ToString();
        else 
            countText.text = "";
    }

    public Item GetItem() {
        return item;
    }


}

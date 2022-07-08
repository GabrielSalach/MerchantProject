using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CraftingWindowPopup : Window
{
    // Singleton instance
    public static CraftingWindowPopup instance;

    // Current item amount to craft
    UInt16 count;
    // Max craftable item
    int maxCount;
    // Current recipe
    Recipe selectedRecipe;
    // Reference to count text 
    Text countText;
    // Reference to product image
    Image productImage;
    // Reference to product text
    Text productText;

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

        productImage = transform.Find("Item Slot").GetComponent<Image>();
        productText = transform.Find("Item Slot").GetComponentInChildren<Text>();
    }


    // Sets recipe graphic and craft button behavior
    public void SetCrafterAndRecipe(Crafter crafter, Recipe recipe) {
        selectedRecipe = recipe;
        maxCount = InventoryManager.MaxCraftableItems(recipe);
        count = 1;
        transform.Find("Craft").GetComponent<Button>().onClick.RemoveAllListeners();
        transform.Find("Craft").GetComponent<Button>().onClick.AddListener(delegate{crafter.CraftItem(recipe, count); CloseWindow();});
        SetCountText();
        SetItemImage();
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
        productImage.sprite = selectedRecipe.product.item.sprite;
        productText.text = selectedRecipe.product.count.ToString();

    }

    void SetCountText() {
        countText.text = count.ToString();
    } 


}

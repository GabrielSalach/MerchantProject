using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Crafter : CharacterUnit
{

    // Crafter class instance
    public CrafterClass crafterClass;
    // Is the crafter busy
    bool isBusyCrafting;
    // Is the crafter crafting (arrived at crafting station)
    bool isCrafting;
    // Remaining time crafting
    float craftingTimer;
    // Selected recipe
    Recipe currentRecipe = null;
    // Time to craft x items
    float maxCraftingTime;
    // Number of items to craft
    int count;
    // Progress Bar
    [SerializeField] GameObject progressBarPrefab;
    GameObject progressBar;
    Material progressBarMaterial;
    // Crafting station reference
    public HubElement craftingStation;

    // Level
    int level = 0;
    // Known recipes at this level
    List<Recipe> knownRecipes;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        knownRecipes = new List<Recipe>();
        UpdateKnownRecipes();

        isBusyCrafting = false;
        isCrafting = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        // If the crafter got to his destination, triggers the actual crafting
        if(isBusyCrafting && arrivedAtDestination && !isCrafting) {
            TriggerCrafting();
        }
        if(isBusyCrafting && isCrafting) {
            craftingTimer -= Time.deltaTime;
            progressBarMaterial.SetFloat("_PercentageComplete", (maxCraftingTime - craftingTimer) / maxCraftingTime);
            if(craftingTimer <= 0) {
                //Done
                isBusyCrafting = false;
                isCrafting = false;
                InventoryManager.AddItem(currentRecipe.product.item, Convert.ToUInt16(currentRecipe.product.count * count));
                NotificationsManager.TriggerNotification("Done crafting " + count + "x " + currentRecipe.product.item.itemName);
                ToggleProgressBarOff();
                craftingTimer = 999;
            }
        }
    }

    // Opens crafting window
    public override void OnClick() {
        base.OnClick();
        CrafterInfoWindow.instance.SetTitle(unitName);
        CrafterInfoWindow.instance.actionButton.onClick.RemoveAllListeners();
        CrafterInfoWindow.instance.actionButton.onClick.AddListener(delegate {CraftingWindow.instance.SetCrafter(this); CraftingWindow.instance.OpenWindow();});
        CrafterInfoWindow.instance.actionButtonText.SetText("Open Crafting menu");
        CrafterInfoWindow.instance.actionButton.gameObject.SetActive(true);
        CrafterInfoWindow.instance.OpenWindow();
    }

    public void CraftItem(Recipe recipe, UInt16 count) {
        if(isBusyCrafting == false) {
            MoveToElement(craftingStation);
            currentRecipe = recipe;
            this.count = count;
            // Gets ingredients
            foreach(Recipe.ItemCount item in recipe.ingredients) {
                InventoryManager.RemoveItem(item.item, Convert.ToUInt16(item.count * count));
            }
            isBusyCrafting = true;  
            CraftingWindow.instance.CloseWindow();
        } else
            NotificationsManager.TriggerNotification("Crafter is busy.");
    }

    void TriggerCrafting() {
        isCrafting = true;
        // Sets craftingTimer
        maxCraftingTime = currentRecipe.craftingTime * count;
        craftingTimer = maxCraftingTime;
        ToggleProgressBarOn();
    }

    // Need to call on LevelUp, Updates the list of known recipes
    void UpdateKnownRecipes() {
        knownRecipes.Clear();
        foreach(CrafterClass.RecipeByLevel recipeByLevel in crafterClass.availableRecipes) {
            if(recipeByLevel.level == level)
                knownRecipes.Add(recipeByLevel.recipe);
        }
    }

    void ToggleProgressBarOn() {
        if(progressBar == null) {
            progressBar = Instantiate(progressBarPrefab, transform);
            progressBarMaterial = progressBar.transform.Find("ProgressBarFiller").GetComponent<SpriteRenderer>().material;
        } else {
            progressBar.SetActive(true);
        }
    }

    void ToggleProgressBarOff() {
        progressBar.SetActive(false);
    }

    public List<Recipe> GetKnownRecipes() {
        return knownRecipes;
    }

    public bool GetIsBusyCrafting() {
        return isBusyCrafting;
    }

    public float GetMaxCraftingTime() {
        return maxCraftingTime;
    }

    public float GetCraftingTimer() {
        return craftingTimer;
    }
}

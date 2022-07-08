using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingWindow : Window
{

    public static CraftingWindow instance;

    // Contains crafter's known recipes
    List<Recipe> recipes;
    // Prefab for recipe
    [SerializeField]
    GameObject recipePrefab;
    // Prefab for Item slot;
    [SerializeField]
    GameObject itemSlotPrefab;


    // Red arrow used to indicate time left for this recipe
    Image timerArrow;
    // Crafter linked to the window
    Crafter crafter;

    // Singleton
    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    new void Update()
    {
        base.Update();
        if(crafter != null) {
            if(crafter.GetIsBusyCrafting() == true) {
                // Fill the arrow 
                timerArrow.fillAmount = (crafter.GetMaxCraftingTime() - crafter.GetCraftingTimer())/crafter.GetMaxCraftingTime();
            } else {
                if(timerArrow != null)
                    timerArrow.fillAmount = 0;
            }
        }

    }

    public void SetCrafter(Crafter crafter) {
        this.crafter = crafter;
        SetTitle(crafter.unitName);
        SetRecipes(crafter.GetKnownRecipes());
    }

    void TriggerPopup(Recipe recipe, Image arrowImage) {
        if(InventoryManager.MaxCraftableItems(recipe) == 0) {
            NotificationsManager.TriggerNotification("Not enough items");
        } else {
            if(crafter.GetIsBusyCrafting() == false)
                timerArrow = arrowImage;
            CraftingWindowPopup.instance.SetCrafterAndRecipe(crafter, recipe);
            OpenPopup(CraftingWindowPopup.instance);
        }
    }


    // Add to set crafter and set it to private
    void SetRecipes(List<Recipe> crafterRecipes) {
        recipes = crafterRecipes;
        // Generates recipes display
        foreach(Transform children in transform.Find("Scroll").Find("Viewport").Find("Content").transform) {
            Destroy(children.gameObject);
        }
        if(recipes != null)
        {
            foreach(Recipe recipe in recipes)
            {
                // Instantiates a new recipe entry from prefab and set ingredient and product variables
                GameObject currentRecipeEntry = Instantiate(recipePrefab, transform.Find("Scroll").Find("Viewport").Find("Content"));
                Transform ingredients = currentRecipeEntry.transform.Find("Ingredients");
                Transform product = currentRecipeEntry.transform.Find("Products").Find("Item Slot");
                Button button = currentRecipeEntry.GetComponent<Button>();

                // Sets the right sprite and count for each items in the recipe
                foreach(Recipe.ItemCount item in recipe.ingredients) {
                    GameObject slot = Instantiate(itemSlotPrefab, currentRecipeEntry.transform.Find("Ingredients"));
                    slot.transform.Find("Item Sprite").GetComponent<Image>().sprite = item.item.sprite;
                    slot.GetComponentInChildren<Text>().text = item.count.ToString();
                }
                product.GetComponent<Image>().sprite = recipe.product.item.sprite;
                product.GetComponentInChildren<Text>().text = recipe.product.count.ToString();

                // Sets onClick to call the right method 
                button.onClick.AddListener(delegate {TriggerPopup(recipe, currentRecipeEntry.transform.Find("Arrow").Find("Timer").GetComponent<Image>()); });
            }
        }
    }
}

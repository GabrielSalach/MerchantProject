using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Crafting/CrafterClass", order = 1)]
public class CrafterClass : ScriptableObject
{
    [System.Serializable]
    public class RecipeByLevel {
        public int level;
        public Recipe recipe;
    }
    public string profession;
    public List<RecipeByLevel> availableRecipes;

    // Generation of animator override controller
    /* [SerializeField, HideInInspector] private bool _hasBeenInitialised = false;
 
    private void OnValidate()
    {
        if (!_hasBeenInitialised)
        {
            AnimatorOverrideController controller = new AnimatorOverrideController();
            AssetDatabase.CreateAsset(controller, "Assets/Crafting/Animators/" + AssetDatabase.FindAssets() + ".overrideController");
            _hasBeenInitialised = true;
        }
    }*/
}

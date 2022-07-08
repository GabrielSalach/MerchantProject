using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Crafting/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    [System.Serializable]
    public class ItemCount
    {
        public Item item;
        public UInt16 count;

        public ItemCount(Item item, UInt16 count) {
            this.item = item;
            this.count = count;
        }
    }
    public string recipeName;
    public ItemCount[] ingredients;
    public ItemCount product;
    public float craftingTime;
    
}
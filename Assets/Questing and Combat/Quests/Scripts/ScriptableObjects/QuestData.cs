using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData : ScriptableObject
{

    [System.Serializable]
    public class DropChance {
        public int count;
        public float dropRate;
    }

    [System.Serializable] 
    public class DropTableElement {
        public Item item;
        public List<DropChance> dropChances;
    }

    // Name of the quest
    public string questName;
    // Description of the quest
    public string questDescription;
    // Item drops
    public List<DropTableElement> dropTable;
    // How much gold does it costs to run the quest
    public int goldCost;
    // Full screen Art for the quest
    public Sprite questArt;
    // Icon displayed on the map
    public Sprite questIcon;
    // Maximum number of heroes allowed on the quest
    public int maxPartySize;
    // XP gained when doing this quest
    public int xpGained;
     
}

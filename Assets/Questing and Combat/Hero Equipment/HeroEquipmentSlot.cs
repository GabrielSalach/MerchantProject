using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroEquipmentSlot : MonoBehaviour
{
    HeroEquipment item;


    [SerializeField] Image itemSprite;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemStats;

    public void SetItem(HeroEquipment item) {
        this.item = item;
        itemSprite.sprite = item.sprite;
        itemName.SetText(item.itemName);
        string itemStatsString = "";
        if(item.bonusStats.HP > 0)
            itemStatsString += item.bonusStats.HP.ToString() + " Max HP   ";
        if(item.bonusStats.Atk > 0)
            itemStatsString += item.bonusStats.Atk.ToString() + " Atk   ";
        if(item.bonusStats.MAtk > 0)
            itemStatsString += item.bonusStats.MAtk.ToString() + " M. Atk   ";
        if(item.bonusStats.Def > 0)
            itemStatsString += item.bonusStats.Def.ToString() + " Def   ";
        if(item.bonusStats.MDef > 0)
            itemStatsString += item.bonusStats.MDef.ToString() + " M. Def   ";
        if(item.bonusStats.Acc > 0)
            itemStatsString += item.bonusStats.Acc.ToString() + " Acc   ";
        if(item.bonusStats.CritChance > 0)
            itemStatsString += item.bonusStats.CritChance.ToString() + " Crit. Chance   ";
        if(item.bonusStats.CritDmg > 0)
            itemStatsString += item.bonusStats.CritDmg.ToString() + " Crit. Dmg   ";
        if(item.bonusStats.Eva > 0)
            itemStatsString += item.bonusStats.Eva.ToString() + " Eva   ";
        if(item.bonusStats.Luck > 0)
            itemStatsString += item.bonusStats.Luck.ToString() + " Luck   ";
        
        itemStats.SetText(itemStatsString);
    } 

    public HeroEquipment GetEquipment() {
        return item;
    }

    public void SetRawData(Sprite itemSprite, string itemName, string itemDescription) {
        this.itemSprite.sprite = itemSprite;
        this.itemName.SetText(itemName);
        this.itemStats.SetText(itemDescription);
    }
}

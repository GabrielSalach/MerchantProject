using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] TextMeshProUGUI itemNameVisual;
    [SerializeField] TextMeshProUGUI dropChanceVisual;


    public void SetItemDrop(Item item, int count, float dropChance) {
        itemSlot.SetItemSlot(item, (ushort) count);
        itemNameVisual.SetText(item.itemName);
        dropChanceVisual.SetText((dropChance * 100).ToString()+"%");
    }
}

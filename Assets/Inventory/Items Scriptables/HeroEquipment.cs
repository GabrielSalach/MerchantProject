using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Inventory/HeroEquipment", order = 1)]
public class HeroEquipment : Item
{
    public ItemSet itemSet;
    public enum EquipSlot {None, RightHand, LeftHand, Helmet, Armor, Gloves, Boots, Trinket};
    public EquipSlot equipSlot;
	public CombatStats bonusStats;
    public Skill skill;
}

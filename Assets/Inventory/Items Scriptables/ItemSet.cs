using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Inventory/ItemSet", order = 1)]
public class ItemSet : ScriptableObject
{
    public string itemSetName;
    public string itemSetDescription;
}

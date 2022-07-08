using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Questing/MaterialQuestData", order = 1)]
public class MaterialQuestData : QuestData
{
    public float baseTimer;
    public int maxRunLimit;
}

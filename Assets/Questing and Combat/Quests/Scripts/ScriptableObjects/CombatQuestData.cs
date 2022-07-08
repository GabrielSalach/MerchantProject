using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Questing/CombatQuestData", order = 1)]
public class CombatQuestData : QuestData
{
    public EnemyData enemy;
    public float secondsPerTurn;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossQuestData", menuName = "ScriptableObjects/Questing/BossQuestData", order = 1)]
public class BossQuestData : QuestData 
{
	public EnemyData boss;
}

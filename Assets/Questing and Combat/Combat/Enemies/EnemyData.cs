using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Combat/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public Sprite enemyArt;
    public Sprite enemyIcon;
    public CombatStats enemyStats;
    public Skill basicAttack;
    public List<Skill> skills;
}

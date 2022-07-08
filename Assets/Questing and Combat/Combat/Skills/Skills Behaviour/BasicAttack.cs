using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack", menuName = "ScriptableObjects/Combat/Skills/BasicAttack", order = 1)]
public class BasicAttack : Skill
{
	public float DamageMultiplier = 1f;

	public override void Apply(CombatEntity user, CombatEntity[] targets)
	{
		float physicalDmg = user.currentStats.Atk * CombatEntity.DamageReductionCalculation(user.currentStats.Atk, targets[0].currentStats.Def);
        
		float magicalDmg = user.currentStats.MAtk * CombatEntity.DamageReductionCalculation(user.currentStats.MAtk, targets[0].currentStats.MDef);
  		int totalDmg = (int) (physicalDmg + magicalDmg);
		totalDmg = (int) (totalDmg * DamageMultiplier);
        targets[0].currentStats.HP -= totalDmg;
	}
}

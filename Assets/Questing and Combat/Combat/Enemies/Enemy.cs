using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CombatEntity
{
    public void InitializeEnemy(EnemyData enemy) {
        // Assign a copy of the combatStats present in the enemy data to baseStats and currentStats
        this.baseStats = new CombatStats(enemy.enemyStats);
        this.currentStats = new CombatStats();
        // Get the basic attack and skill from enemyData
        this.basicAttack = enemy.basicAttack;
        this.skills = enemy.skills;
    }

    public void ResetEnemy() {
        this.currentStats.SetStats(baseStats);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CombatEntity : MonoBehaviour {

    // Default base stats
    public CombatStats baseStats;
    // Current stats after modification (i.e. loss of hp)
    public CombatStats currentStats;
    // List of all the known skills
    [HideInInspector] public List<Skill> skills;
    // Basic attack, used in combat quests
    [HideInInspector] public Skill basicAttack;

    // Method used to compute the damage reduction factor, based on resistances like defense of m.defense. Need to multiply this number by the raw damage dealt.
    // damage is the base damage of an attack, resistance is the value used to reduce the incoming attack.
    public static float DamageReductionCalculation(int damage, int resistance) {
        float reduction = 0;
        float _damage = damage;
        float _resistance = resistance;
        if(damage > 0) {
            reduction = _damage/(_damage+_resistance);
        }
        return reduction;
    }

    public virtual void ReadFromSavefile() {}
    public virtual void WriteToSavefile() {}
}
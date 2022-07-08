using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Skill : ScriptableObject{
    public int skillId;
    public string skillName;
    public Sprite skillIcon;
    public string skillDescription;
    
    public virtual void Enter(CombatEntity user, CombatEntity[] targets) {}
    public virtual void Do(CombatEntity user, CombatEntity[] targets) {}
    public virtual void Exit(CombatEntity user, CombatEntity[] targets) {}
    public virtual void Apply(CombatEntity user, CombatEntity[] targets) {}

    public static Skill GetSkillByID(UInt16 id) {
        Skill skill = null;
        UnityEngine.Object[] loadedItems = (UnityEngine.Object[]) Resources.LoadAll("Skills", typeof (Skill)); 
        foreach(UnityEngine.Object loadedItem in loadedItems) {
            if(((Skill)loadedItem).skillId == id) {
                skill = (Skill)loadedItem;
            }
        }
        return skill;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Combat/HeroClass", order = 1)]
public class HeroClass : ScriptableObject
{
	public UInt16 classID;
    public string className;
    public Sprite classSprite;

	public CombatStats baseStats;	
	public CombatStats statsScaling;

	[System.Serializable]
	public class SkillList {
		public Skill skill;
		public int level;
	}
	public List<SkillList> skillList; 

	public static HeroClass GetClassFromID(UInt16 id) {
		HeroClass returnClass = null;
        UnityEngine.Object[] loadedItems = (UnityEngine.Object[]) Resources.LoadAll("Hero Classes", typeof (HeroClass)); 
        foreach(UnityEngine.Object item in loadedItems) {
			HeroClass loadedClass = (HeroClass) item;
			if(loadedClass.classID == id) {
				returnClass = loadedClass;
			}
		}
		return returnClass;
	}

}

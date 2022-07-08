using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class Hero : CombatEntity
{
	public string heroName;
	public HeroClass heroClass;

	public int level = 0;
	public int currentXP;
	public int XPtoNextLevel {get; protected set;}


	public HeroEquipment[] equipment = new HeroEquipment[8];

	void Start() {
		ReadFromSavefile();
		heroName = heroClass.className;
	}

	public void InitializeHero() {
		baseStats = heroClass.baseStats;
		// Level Scaling
		baseStats += heroClass.statsScaling * level;
		// Equipment Stats
		foreach(HeroEquipment item in equipment) {
			if(item != null) {
				baseStats += item.bonusStats;
			}
		}
		currentStats = new CombatStats(baseStats);
		SetSkills();
	}

	public void UpdateHero() {
		CombatStats statsOffset = new CombatStats();
		statsOffset = baseStats - currentStats;
		baseStats = heroClass.baseStats;
		// Level Scaling
		baseStats += heroClass.statsScaling * level;
		// Equipment Stats
		foreach(HeroEquipment item in equipment) {
			if(item != null) {
				baseStats += item.bonusStats;
			}
		}
		currentStats = baseStats - statsOffset;
		SetSkills();
	}

	void SetSkills() {
		if(skills == null) {
			skills = new List<Skill>();
		} else {
			skills.Clear();
		}
		basicAttack = Skill.GetSkillByID(0);
		skills.Add(basicAttack);
		foreach(HeroClass.SkillList skill in heroClass.skillList) {
			if(level >= skill.level) {
				skills.Add(skill.skill);
			}
		}
		foreach(HeroEquipment item in equipment) {
			if(item != null && item.skill != null) {
				skills.Add(item.skill);
			}
		}
	}

	public void EquipItem(HeroEquipment item, int slot) {
		if(equipment[slot] != null) {
			InventoryManager.AddItem(equipment[slot], 1);
		}
		equipment[slot] = item;
		UpdateHero();
		InventoryManager.RemoveItem(item, 1);
		WriteToSavefile();
	}

	public void AddXP(int amount) {
		currentXP += amount;
		if(currentXP >= XPtoNextLevel) {
			LevelUp();
			currentXP -= XPtoNextLevel;
		}
	}

	public void LevelUp() {
		level += 1;
		UpdateHero();
	}

	public struct HeroData {
		public string heroName;
		public int classId;
	}


	public override void ReadFromSavefile() {
		int offset = SavefileManager.HeroOffSet;
		UInt16 classID = BitConverter.ToUInt16(SavefileManager.ReadSavefile(offset, 2), 0);
		offset += 2;
		heroClass = HeroClass.GetClassFromID(classID);

		Byte[] levelBytes = SavefileManager.ReadSavefile(offset, 4);
		offset += 4;
		if(BitConverter.IsLittleEndian) {
			Array.Reverse(levelBytes);
		}
		this.level = BitConverter.ToInt32(levelBytes, 0);

		Byte[] xpBytes = SavefileManager.ReadSavefile(offset, 4);
		offset += 4;
		if(BitConverter.IsLittleEndian) {
			Array.Reverse(xpBytes);
		}
		this.currentXP = BitConverter.ToInt32(xpBytes, 0);

		Byte[] equipmentBytes = SavefileManager.ReadSavefile(offset, 16);
		for(int i = 0; i < 8; i++) {
			UInt16 itemId = BitConverter.ToUInt16(equipmentBytes, i * 2);
			if(itemId != UInt16.MaxValue) {
				equipment[i] = (HeroEquipment) InventoryManager.allItems[itemId];
			} else {
				equipment[i] = null;
			}
		}
		offset += 16;

		currentStats = new CombatStats(SavefileManager.ReadSavefile(offset, 40));
		InitializeHero();
	}

	// Gets bytes from all the data needed to save a hero and writes them into the savefile
	public override void WriteToSavefile() {
		// List of all the bytes to save
		List<Byte> bytesToWrite = new List<Byte>();

		// Class ID 2 bytes
		Byte[] classBytes = BitConverter.GetBytes(heroClass.classID);
		bytesToWrite.AddRange(classBytes);

		// Level 4 bytes
		Byte[] levelBytes = BitConverter.GetBytes(level);
		if(BitConverter.IsLittleEndian) {
			Array.Reverse(levelBytes);
		}
		bytesToWrite.AddRange(levelBytes);

		// current XP 4 bytes 
		Byte[] xpBytes = BitConverter.GetBytes(currentXP);
		if(BitConverter.IsLittleEndian) {
			Array.Reverse(xpBytes);
		}
		bytesToWrite.AddRange(xpBytes);

		// Equipment ID 8x 2 bytes
		List<Byte> equipmentBytesList = new List<Byte>();
		for(int i = 0; i < 8; i++) {
			Byte[] itemBytes;
			if(equipment[i] != null) {
				itemBytes = BitConverter.GetBytes(equipment[i].id);
			} else {
				itemBytes = BitConverter.GetBytes(UInt16.MaxValue);
			}
			equipmentBytesList.AddRange(itemBytes);
		}
		bytesToWrite.AddRange(equipmentBytesList);

		// Current stats 10x 4 bytes
		Byte[] currentStatsBytes = currentStats.GetBytes();
		bytesToWrite.AddRange(currentStatsBytes);

		// Transforms the bytes list to an array and writes it into the savefile 
		Byte[] byteArray = bytesToWrite.ToArray();
		SavefileManager.WriteSavefile(byteArray, SavefileManager.HeroOffSet, bytesToWrite.Count);
	}
}

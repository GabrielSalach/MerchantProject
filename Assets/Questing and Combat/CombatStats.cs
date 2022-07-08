using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class NamedArrayAttribute : PropertyAttribute
{
    public readonly string[] names;
    public NamedArrayAttribute(string[] names) { this.names = names; }
}

[System.Serializable]
public class CombatStats 
{
	const int statsCount = 10;
	[NamedArrayAttribute(new string[] {"HP", "Atk", "M.Atk", "Def", "M.Def", "Acc", "Crit Chance", "Crit Damage", "Eva", "Luck"})]
	public int[] stats = new int[statsCount];
	
    public int HP {
		get => stats[0];
		set {
			stats[0] = value;
		}
	}
    public int Atk{
		get => stats[1];
		set {
			stats[1] = value;
		}
	}
	public int MAtk{
		get => stats[2];
		set {
			stats[2] = value;
		}
	}
	public int Def{
		get => stats[3];
		set {
			stats[3] = value;
		}
	}
	public int MDef{
		get => stats[4];
		set {
			stats[4] = value;
		}
	}
	public int Acc{
		get => stats[5];
		set {
			stats[5] = value;
		}
	}
	public int CritChance{
		get => stats[6];
		set {
			stats[6] = value;
		}
	}
	public int CritDmg{
		get => stats[7];
		set {
			stats[7] = value;
		}
	}
	public int Eva{
		get => stats[8];
		set {
			stats[8] = value;
		}
	}
	public int Luck{
		get => stats[9];
		set {
			stats[9] = value;
		}
	}

	public CombatStats() {
		for(int i = 0; i < stats.Length; i++) {
			stats[i] = 0;
		}
	}

	public CombatStats(CombatStats copiedStats) {
		for(int i = 0; i < stats.Length; i++) {
			stats[i] = copiedStats.stats[i];
		}
	}

	public CombatStats(Byte[] bytes) {
		for(int i = 0; i < statsCount; i++) {
			Byte[] bytesSegment = new byte[4];
			for(int j = 0; j < 4; j++) {
				bytesSegment[j] = bytes[i * 4 + j];
			}
			if(BitConverter.IsLittleEndian) {
				Array.Reverse(bytesSegment);
			}
			stats[i] = BitConverter.ToInt32(bytesSegment, 0);
		}
	}

	public void SetStats(CombatStats targetStats) {
		for(int i = 0; i < stats.Length; i++) {
			stats[i] = targetStats.stats[i];
		}
	} 

	public static CombatStats operator +(CombatStats a, CombatStats b) {
		CombatStats returnStats = new CombatStats();
		for(int i = 0; i < returnStats.stats.Length; i++) {
			returnStats.stats[i] = a.stats[i] + b.stats[i];
		} 
		return returnStats;
	}

	public static CombatStats operator *(CombatStats a, int b) {
		CombatStats returnStats = new CombatStats();
		for(int i = 0; i < returnStats.stats.Length; i++) {
			returnStats.stats[i] = a.stats[i] * b;
		} 
		return returnStats;
	}

	public static CombatStats operator -(CombatStats a, CombatStats b) {
		CombatStats returnStats = new CombatStats();
		for(int i = 0; i < returnStats.stats.Length; i++) {
			returnStats.stats[i] = a.stats[i] - b.stats[i];
		} 
		return returnStats;
	}

	public bool Empty() {
		bool empty = true;
		if(stats.Max() > 0) {
			empty = false;
		}
		return empty;
	}

	public Byte[] GetBytes() {
		List<Byte> bytes = new List<Byte>();
		foreach(int stat in stats) {
			Byte[] intBytes = BitConverter.GetBytes(stat);
			if(BitConverter.IsLittleEndian) {
				Array.Reverse(intBytes);
			}
			bytes.AddRange(intBytes);
		}
		return bytes.ToArray();
	}
}

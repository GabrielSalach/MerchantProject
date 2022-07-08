using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatStatsSerializationTester : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        CombatStats stats = new CombatStats();
        stats.Atk = 10;
        stats.MDef = 25;
        Byte[] statsBytes = stats.GetBytes();
        CombatStats stats2 = new CombatStats(statsBytes);
        Debug.Log(stats2.Atk);
        Debug.Log(stats2.MDef);
    }

}

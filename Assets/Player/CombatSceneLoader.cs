using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatSceneLoader : MonoBehaviour {
    public static CombatSceneLoader instance;

    public List<Hero> heroParty;
    public BossQuestData questData;

    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void LoadCombatScene(List<Hero> heroParty, BossQuestData questData) {
        this.heroParty = heroParty;
        this.questData = questData;
        SceneManager.LoadScene(1);
    }
}

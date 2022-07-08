using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public List<Quest> quests;


    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    void Start() {
        foreach(MapRegion region in MapBehavior.instance.regions) {
            foreach(QuestMarker marker in region.questMarkers) {
                Quest quest = QuestFactory.GetQuest(marker);
                quest.ResetQuest();
                quests.Add(quest);
            }
        }
    }

    void Update() {
        // Rework this to add ongoing quests to a list or something
        foreach(Quest quest in quests) {
            if(quest.questState == Quest.QuestState.Ongoing) {
                System.Type questType = quest.GetType();
                if(questType == typeof (CombatQuest)) {
                    ((CombatQuest) quest).TickTimer(Time.deltaTime);
                } else if(questType == typeof (MaterialQuest)) {
                    ((MaterialQuest) quest).TickTimer(Time.deltaTime);
                }
            }
        }
    }
}

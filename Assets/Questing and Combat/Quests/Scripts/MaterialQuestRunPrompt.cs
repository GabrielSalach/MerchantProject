using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialQuestRunPrompt : Window
{
    public static MaterialQuestRunPrompt instance;


    [SerializeField] Button addCount, removeCount, startQuest;
    [SerializeField] TextMeshProUGUI timeText;
    int currentRunAmount;
    MaterialQuest quest;
    List<Hero> heroParty;

    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    public override void Start() {
        base.Start();
        addCount.onClick.AddListener(AddCount);
        removeCount.onClick.AddListener(RemoveCount);
    }


    public void SetQuest(MaterialQuest quest, List<Hero> heroParty) {
        this.quest = quest;
        this.heroParty = heroParty;
        currentRunAmount = 1;
        SetTimeText();
        startQuest.onClick.RemoveAllListeners();
        startQuest.onClick.AddListener(StartQuest);
    }

    public void StartQuest() {
        quest.StartQuest(heroParty, currentRunAmount);
        this.CloseWindow();
    }


    public void AddCount() {
        if(currentRunAmount < ((MaterialQuestData) quest.linkedMarker.linkedQuestData).maxRunLimit) {
            currentRunAmount++;
            SetTimeText();
        }
    }

    public void RemoveCount() {
        currentRunAmount--;
        if(currentRunAmount == 0) {
            currentRunAmount = ((MaterialQuestData) quest.linkedMarker.linkedQuestData).maxRunLimit;
        }
        SetTimeText();
    }

    void SetTimeText() {
        timeText.SetText(MerchantUtilities.TimeFormat(currentRunAmount * ((MaterialQuestData) quest.linkedMarker.linkedQuestData).baseTimer));
    }


}

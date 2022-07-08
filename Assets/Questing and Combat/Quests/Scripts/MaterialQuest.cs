using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialQuest : Quest
{
    Hero hero;
    public float questTimer = 0;
    float secondsPerRun;
    int runToDo;

    public MaterialQuest(QuestMarker linkedMarker) : base(linkedMarker) {
        MaterialQuestData materialQuestData = (MaterialQuestData) linkedMarker.linkedQuestData;
        secondsPerRun = materialQuestData.baseTimer;
    }

	public void StartQuest(List<Hero> heroParty, int runToDo)
	{
		base.StartQuest(heroParty);
        hero = heroParty[0];
        this.runToDo = runToDo;
        questTimer = secondsPerRun * runToDo;
	}

    public void TickTimer(float deltaTime) {
        questTimer -= deltaTime;
        if(questTimer <= 0) {
            this.EndQuest();
        }
        linkedMarker.timer.SetText(MerchantUtilities.TimeFormat(questTimer));
    }

    public override Dictionary<Item, int> GetQuestRewards() {
        Dictionary<Item, int> rewards = base.GetQuestRewards();
        if(questFailed == false) {
            for(int i = 1; i < runToDo; i++) {
                rewards.MergeDictionaries<Item>(base.GetQuestRewards());
            }
        }
        return rewards;
    }
}

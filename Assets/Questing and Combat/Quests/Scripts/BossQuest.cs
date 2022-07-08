using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuest : Quest
{
    

    public BossQuest(QuestMarker linkedMarker) : base(linkedMarker) {
        
	}

	public override void StartQuest(List<Hero> heroParty)
	{
		base.StartQuest(heroParty);
        CombatSceneLoader.instance.LoadCombatScene(heroParty, (BossQuestData) linkedMarker.linkedQuestData);
	}
}

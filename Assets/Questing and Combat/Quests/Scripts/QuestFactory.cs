using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFactory 
{

    public static Quest GetQuest(QuestMarker linkedMarker) {
        switch(linkedMarker.linkedQuestData) {
            case MaterialQuestData data:
                return new MaterialQuest(linkedMarker);
            case CombatQuestData data:
                return new CombatQuest(linkedMarker);
            case BossQuestData data:
                return new BossQuest(linkedMarker);
            default:
                break;
        }
        return null;
    }
}

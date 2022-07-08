using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateSwitchEvent : UnityEvent<Quest.QuestState> {}

[System.Serializable] 
public class Quest {
    // Serializable Object containing all the quest's data
    public QuestData questData;
    // State of the quest. Affects the display and the available interactions of the linked marker on the map.
    public enum QuestState {Disabled, Unavailable, Available, Ongoing, RewardsReady};
    public QuestState questState;
    // Quest marker
    public QuestMarker linkedMarker;
    // Event for the state switching thing
    StateSwitchEvent onStateSwitch;
    // List of heroes partaking in the quest
    public List<Hero> heroParty {get; protected set;}   
    // Flag used to test if the quest has failed or not
    public bool questFailed {get; protected set;}

    // Resets the quest's state and hero party
    public virtual void ResetQuest() {
        questState = QuestState.Available;
        onStateSwitch.Invoke(questState);
        questFailed = false;
        if(heroParty != null) {
            heroParty.Clear();
        }
    }

    // Constructor. Creates an instance based on the data contained in the quest marker and links the marker to it.
    public Quest(QuestMarker linkedMarker) {
        this.linkedMarker = linkedMarker;
        this.linkedMarker.linkedQuest = this;
        this.questData = linkedMarker.linkedQuestData;
        this.questState = QuestState.Available;
        onStateSwitch = new StateSwitchEvent();
        onStateSwitch.AddListener(linkedMarker.SwitchState);
    }

    // Initializes the quest and sets the state. This method is overriden by children of this class for quest specific behaviors.
    public virtual void StartQuest(List<Hero> heroParty) {
        this.heroParty = heroParty;
        QuestPreviewWindow.instance.CloseWindow();
        questState = QuestState.Ongoing;
        onStateSwitch.Invoke(questState);
    }

    // Virtual method overriden by children of this class. Equivalent to Update()
    public virtual void RunQuest() {

    }

    // Ends the quest and sets the state to RewardsReady. Is overriden by children of this class for quest specific behaviors
    public virtual void EndQuest() {
        questState = QuestState.RewardsReady;
        onStateSwitch.Invoke(questState);
    }

    // Generates a dictionary containing the quest rewards based on the droptable contained in the quest data.
    public virtual Dictionary<Item, int> GetQuestRewards() {
        Dictionary<Item, int> rewards = new Dictionary<Item, int>();
        
        // Only generates rewards if the quest has not failed
        if(questFailed == false) { // Loops through the drop table and generates a random number between 0 and 1. If the generated number is below the droprate, count is updated to match the rng value.
            foreach(QuestData.DropTableElement element in questData.dropTable) {
                Item droppedItem = element.item;
                int count = 0;
                float rng = Random.Range(0f, 1f);
                foreach(QuestData.DropChance dropChance in element.dropChances) {
                    if(rng <= dropChance.dropRate) {
                        count = dropChance.count;
                    }
                }
                rewards.Add(droppedItem, count);
            }
        }
        return rewards;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatQuest : Quest
{   
    // Combat units involved in the quest
    Enemy enemy;
    Hero hero;

    // Temporary object spawned at the start of the quest to hold enemy data.
    GameObject enemyObject;

    // current quest timer
    public float questTimer = 0;
    // how many seconds a turn takes
    float secondsPerTurn;
    // if the battle reaches this turn limit, the quest fails 
    int maxTurnLimit;

    // Contructor. Creates an instance of combat quest based on the linked marker and the quest data contained within it.
    public CombatQuest(QuestMarker linkedMarker) : base(linkedMarker) {
        // Holds the quest data in a temporary CombatQuestData variable
        CombatQuestData combatQuestData = (CombatQuestData) linkedMarker.linkedQuestData;
        // Initializes seconds per turn from quest data
        secondsPerTurn = combatQuestData.secondsPerTurn;

        // Spawns an enemy object to hold the data
        enemyObject = new GameObject(combatQuestData.enemy.enemyName);
        enemyObject.transform.parent = linkedMarker.transform;
        // Adds an Enemy instance to the created GameObject and assigns it to the enemy field 
        enemy = enemyObject.AddComponent<Enemy>();
        // Initializes the enemy instance
        enemy.InitializeEnemy(combatQuestData.enemy);
    }

	public override void StartQuest(List<Hero> heroParty) {
		base.StartQuest(heroParty);
        // Assigns the hero to the heroParty.
        hero = heroParty[0];
        // Resets the enemy stats.
        enemy.ResetEnemy();
        // Sets the questDone flag to false
        bool questDone = false;
        // Resets turn counter
        int turns = 0;
        // Loops until the quest is over.
        while(questDone == false) {
            turns++;
            // Applies the hero's basic attack on the enemy and checks if the enemy died.
            hero.basicAttack.Apply(hero, new CombatEntity[] {enemy});
            if(enemy.currentStats.HP <= 0) {
                questDone = true;
                break;
            }
            
            // Applies the enemy's basic attack on the hero and checks if the hero died.
            enemy.basicAttack.Apply(enemy, new CombatEntity[] {hero});
            if(hero.currentStats.HP <= 0) {
                questFailed = true;
                questDone = true;
                break;
            }

            // if the max turn limit is reached, the quest fails.
            if(turns == maxTurnLimit) {
                questFailed = true;
                hero.currentStats.HP = 0; 
                questDone = true;
                break;
            }
        }
        // Computes the time it takes to finish the quest based on the number of turns.
        questTimer = turns * secondsPerTurn;
	}

    // Resets the quest.
    public override void ResetQuest() {
        base.ResetQuest();
        enemy.ResetEnemy();
    }

    // Makes the timer tick down. Needs to be called each frame.
    public void TickTimer(float deltaTime) {
        questTimer -= deltaTime;
        if(questTimer <= 0) {
            this.EndQuest();
        }
        linkedMarker.timer.SetText(MerchantUtilities.TimeFormat(questTimer));
    }
}

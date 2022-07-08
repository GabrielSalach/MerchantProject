using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultsWindow : Window
{
    public static QuestResultsWindow instance;

    [SerializeField] Transform heroStatusSection;
    [SerializeField] Transform rewardsSection;
    [SerializeField] GameObject heroStatusPrefab;
    [SerializeField] GameObject itemDropPrefab;
    Quest targetQuest;
    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    public void SetQuest(Quest quest) {
        targetQuest = quest;
        foreach(Transform child in rewardsSection) {
            Destroy(child.gameObject);
        }
        foreach(Transform child in heroStatusSection) {
            Destroy(child.gameObject);
        }
        if(quest.questFailed == false) {

            foreach(Hero hero in quest.heroParty) {
                HeroStatusHolder heroStatus = Instantiate(heroStatusPrefab, heroStatusSection).GetComponent<HeroStatusHolder>();
                heroStatus.SetHero(hero, quest.questData.xpGained);
            }

            foreach(KeyValuePair<Item, int> item in quest.GetQuestRewards()) {
                ItemSlot itemSlot = Instantiate(itemDropPrefab, rewardsSection).GetComponent<ItemSlot>();
                itemSlot.SetItemSlot(item.Key, (ushort) item.Value);
                InventoryManager.AddItem(item.Key, (ushort) item.Value);
            }

        } else {
            foreach(Hero hero in quest.heroParty) {
                HeroStatusHolder heroStatus = Instantiate(heroStatusPrefab, heroStatusSection).GetComponent<HeroStatusHolder>();
                heroStatus.SetHero(hero, 0);
            }
        }
    }

	public override void CloseWindow()
	{
		base.CloseWindow();
        targetQuest.ResetQuest();
	}
}

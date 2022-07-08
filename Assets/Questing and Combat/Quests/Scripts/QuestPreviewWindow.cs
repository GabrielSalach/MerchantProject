using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestPreviewWindow : Window
{
	public static QuestPreviewWindow instance;

	public QuestData questData;

	// Simple View
	[SerializeField] Image questImage;
	[SerializeField] TextMeshProUGUI questDescription;
	[SerializeField] TextMeshProUGUI goldCostText;

	// Information Toggle
	[SerializeField] Button informationToggleButton;
	[SerializeField] GameObject infoToggleOpen;
	[SerializeField] GameObject infoToggleClose;
	[SerializeField] GameObject simpleView;
	[SerializeField] GameObject infoView;
	bool isInfoViewOpen = false;

	// Enemy Info 
	[SerializeField] GameObject enemyInfoTab;
	[SerializeField] TextMeshProUGUI[] enemyStats;
	string[] statsLabel = {"MaxHP", "Atk", "M.Atk", "Def", "M.Def", "Acc", "Crit. Chance", "Crit. Dmg", "Eva", "Luck"};

	// Rewards Panel
	[SerializeField] GameObject rewardsPanel;
	[SerializeField] GameObject itemDropPrefab;

	// Hero Selection 
	[SerializeField] Button heroSelectionButton;
	[SerializeField] TextMeshProUGUI partySizeText;

	void Awake() {
		if(instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
		}
	}

	public override void Start()
	{
		base.Start();
		SetQuestData(questData);
		informationToggleButton.onClick.AddListener(delegate {InformationViewToggle();});
		heroSelectionButton.onClick.AddListener(delegate {
			UnitFinderWindow.instance.SetFilter(typeof (HeroUnit));
			UnitFinderWindow.instance.selectionModeActive = true;
			UnitFinderWindow.instance.maxPartySize = questData.maxPartySize;
			this.OpenPopup(UnitFinderWindow.instance);
		});
	}

	public void SetQuestData(QuestData newQuestData) {
		questData = newQuestData;
		SetTitle(questData.questName);
		questImage.sprite = questData.questArt;
		questDescription.SetText(questData.questDescription);
		partySizeText.SetText("Party Size Limit : " + questData.maxPartySize);
		goldCostText.SetText("Gold Cost : " + questData.goldCost);
		foreach(Transform child in rewardsPanel.transform) {
			Destroy(child.gameObject);
		} 
		if(questData.GetType() == typeof(MaterialQuestData)) {
			enemyInfoTab.SetActive(false);
		} else {
			enemyInfoTab.SetActive(true);
			GenerateEnemyInfo();
		}
		GenerateRewardsPanel();
	}

	void GenerateRewardsPanel() {
		foreach(QuestData.DropTableElement itemDrop in questData.dropTable) {
			Item item = itemDrop.item;
			foreach(QuestData.DropChance dropChance in itemDrop.dropChances) {
				ItemDrop instantiatedItemDrop = Instantiate(itemDropPrefab, rewardsPanel.transform).GetComponent<ItemDrop>();
				instantiatedItemDrop.SetItemDrop(item, dropChance.count, dropChance.dropRate);
			}
		}
	}

	void GenerateEnemyInfo() {
		CombatStats combatStats = null;
		if(questData.GetType() == typeof(CombatQuestData)) {
			combatStats = ((CombatQuestData) questData).enemy.enemyStats; 
		}
		if(combatStats != null) {
			for(int i = 0; i < 10; i++) {
				enemyStats[i].SetText(statsLabel[i] + " : " + combatStats.stats[i]);
			}
		}
	}


	public void InformationViewToggle() {
		if(isInfoViewOpen == false) {
			infoToggleClose.SetActive(true);
			infoToggleOpen.SetActive(false);
			infoView.SetActive(true);
			simpleView.SetActive(false);
			isInfoViewOpen = true;
			partySizeText.transform.parent.gameObject.SetActive(false);
			goldCostText.transform.parent.gameObject.SetActive(false);
		} else {
			infoToggleClose.SetActive(false);
			infoToggleOpen.SetActive(true);
			infoView.SetActive(false);
			simpleView.SetActive(true);
			isInfoViewOpen = false;
			partySizeText.transform.parent.gameObject.SetActive(true);
			goldCostText.transform.parent.gameObject.SetActive(true);
		}
	}
}

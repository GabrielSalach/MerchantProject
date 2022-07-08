using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInventoryWindow : Window
{
	// Singleton instance
	public static HeroInventoryWindow instance;
	// Reference to hero 
	Hero hero;

	// Reference to hero's big sprite
	Image heroPortrait;
	// Reference to hero's inventory slots
	[SerializeField] Button[] equipmentButtons = new Button[8];	
	
	[SerializeField] Sprite[] defaultIcons = new Sprite[8];

	
	

	// Singleton Logic
	void Awake() {
		if(instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
		}
	}

	public override void Start() {
		base.Start();
		DefaultWindow(); 
		equipmentButtons[0].onClick.AddListener(delegate {HeroEquipmentSelectionWindow.instance.SetEquipmentSlot(0); this.OpenPopup(HeroEquipmentSelectionWindow.instance);});
		equipmentButtons[1].onClick.AddListener(delegate {HeroEquipmentSelectionWindow.instance.SetEquipmentSlot(1); this.OpenPopup(HeroEquipmentSelectionWindow.instance);});
		equipmentButtons[2].onClick.AddListener(delegate {HeroEquipmentSelectionWindow.instance.SetEquipmentSlot(2); this.OpenPopup(HeroEquipmentSelectionWindow.instance);});
		equipmentButtons[3].onClick.AddListener(delegate {HeroEquipmentSelectionWindow.instance.SetEquipmentSlot(3); this.OpenPopup(HeroEquipmentSelectionWindow.instance);});
		equipmentButtons[4].onClick.AddListener(delegate {HeroEquipmentSelectionWindow.instance.SetEquipmentSlot(4); this.OpenPopup(HeroEquipmentSelectionWindow.instance);});
		equipmentButtons[5].onClick.AddListener(delegate {HeroEquipmentSelectionWindow.instance.SetEquipmentSlot(5); this.OpenPopup(HeroEquipmentSelectionWindow.instance);});
		equipmentButtons[6].onClick.AddListener(delegate {HeroEquipmentSelectionWindow.instance.SetEquipmentSlot(6); this.OpenPopup(HeroEquipmentSelectionWindow.instance);});
		equipmentButtons[7].onClick.AddListener(delegate {HeroEquipmentSelectionWindow.instance.SetEquipmentSlot(7); this.OpenPopup(HeroEquipmentSelectionWindow.instance);});

	}


	void UpdateWindow() {
		SetTitle(hero.heroName+"'s Equipment");
		for(int i = 0; i < 8; i++) {
			if(hero.equipment[i] != null) {
				equipmentButtons[i].transform.GetComponent<HeroEquipmentSlot>().SetItem(hero.equipment[i]);
			}
		}
	}

	public void SetHero(Hero hero) {
		this.hero = hero;
		DefaultWindow();
		UpdateWindow();
	}

	public void SetItem(HeroEquipment item, int slot) {
		hero.EquipItem(item, slot);
		this.UpdateWindow();
		HeroInfoWindow.instance.UpdateWindow();
	}

	public void DefaultWindow() {
		equipmentButtons[0].GetComponent<HeroEquipmentSlot>().SetRawData(defaultIcons[0], "Right Hand", "");
		equipmentButtons[1].GetComponent<HeroEquipmentSlot>().SetRawData(defaultIcons[1], "Left Hand", "");
		equipmentButtons[2].GetComponent<HeroEquipmentSlot>().SetRawData(defaultIcons[2], "Helmet", "");
		equipmentButtons[3].GetComponent<HeroEquipmentSlot>().SetRawData(defaultIcons[3], "Armor", "");
		equipmentButtons[4].GetComponent<HeroEquipmentSlot>().SetRawData(defaultIcons[4], "Gloves", "");
		equipmentButtons[5].GetComponent<HeroEquipmentSlot>().SetRawData(defaultIcons[5], "Boots", "");
		equipmentButtons[6].GetComponent<HeroEquipmentSlot>().SetRawData(defaultIcons[6], "Trinket 1", "");
		equipmentButtons[7].GetComponent<HeroEquipmentSlot>().SetRawData(defaultIcons[7], "Trinket 2", "");
	} 

}

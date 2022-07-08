using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitFinderWindow : Window
{
    public static UnitFinderWindow instance;
    System.Type filter;

    public List<CharacterUnit> selectedUnits;

    [SerializeField] Transform contentSection;
    [SerializeField] GameObject unitPreviewPrefab;
    public Button confirmSelectionButton;
    public bool selectionModeActive = false;
    public int maxPartySize;

    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    public List<CharacterUnit> GetUnitByFilter() {
        List<CharacterUnit> returnList = new List<CharacterUnit>();
        foreach(CharacterUnit unit in GameData.instance.GetAllCharacters()) {
            if(filter != null) {
                if(unit.GetType() == filter) {
                    returnList.Add(unit);
                }
            } else {
                returnList.Add(unit);
            }
        }        
        return returnList;
    }

    public void SetFilter(System.Type filter) {
        this.filter = filter;
    }

    public override void OpenWindow() {
        base.OpenWindow();
        GenerateDisplay();
    }

    public void GenerateDisplay() {
        foreach(Transform child in contentSection) {
            Destroy(child.gameObject);
        }
        foreach(CharacterUnit unit in GetUnitByFilter()) {
            UnitPreviewGenerator unitPreview = Instantiate(unitPreviewPrefab, contentSection).GetComponent<UnitPreviewGenerator>();
            unitPreview.SetUnit(unit);
            Button unitPreviewButton = unitPreview.transform.GetComponent<Button>();
            unitPreviewButton.onClick.RemoveAllListeners();
            if(selectionModeActive == false) {
                unitPreviewButton.onClick.AddListener(delegate {unit.OnClick();});
            } else {
                unitPreviewButton.onClick.AddListener(delegate {ToggleSelection(unitPreview);});
            }
        }
        confirmSelectionButton.gameObject.SetActive(selectionModeActive);
    }

    void ToggleSelection(UnitPreviewGenerator unitPreview) {
        CharacterUnit unit = unitPreview.GetUnit();
        if(selectedUnits.Contains(unit)) {
            unitPreview.GetComponent<Image>().color = Color.white;
            selectedUnits.Remove(unit);
        } else {
            if(selectedUnits.Count < maxPartySize) {
                unitPreview.GetComponent<Image>().color = Color.yellow;
                selectedUnits.Add(unit);
            } else {
                NotificationsManager.TriggerNotification("You can only select " + maxPartySize + " heroes.");
            }
        }
    }

    public List<Hero> GetSelectedHeroes() {
        List<Hero> selectedHeroes = new List<Hero>();
        foreach(CharacterUnit hero in selectedUnits) {
            if(hero.GetType() == typeof(HeroUnit)) {
                HeroUnit heroUnit = (HeroUnit) hero;
                selectedHeroes.Add(heroUnit.hero);
            }
        }
        if(selectedHeroes.Count == 0) {
            NotificationsManager.TriggerNotification("No heroes selected.");
            return null;
        } else {
            return selectedHeroes;
        }
    }

	public override void CloseWindow()
	{
		base.CloseWindow();
        selectedUnits.Clear();
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroInfoWindow : Window
{
    // Singleton instance
    public static HeroInfoWindow instance;

    // Action Button
    public Button actionButton;
    public TextMeshProUGUI actionButtonText;

    // Hero information displayed in the window
    Hero hero;

    // Stats
	public TextMeshProUGUI maxHP;
    public TextMeshProUGUI atk;
	public TextMeshProUGUI mAtk;
	public TextMeshProUGUI def;
	public TextMeshProUGUI mDef;
	public TextMeshProUGUI acc;
	public TextMeshProUGUI critChance;
	public TextMeshProUGUI critDmg;
	public TextMeshProUGUI eva;
	public TextMeshProUGUI luck;

    // Singleton logic
    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    new void Start() {
        base.Start();
        // Get actionButton and disables it
        actionButton = transform.Find("Action").GetComponent<Button>();
    }

    // Sets the camera back to freecam when closing the window
    public override void CloseWindow() {
        base.CloseWindow();
        CameraController.instance.UnselectTarget();
    }

    public void SetHero(Hero hero) {
        this.hero = hero;
        SetTitle(hero.heroName);
        UpdateWindow();
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(delegate {HeroInventoryWindow.instance.SetHero(hero); OpenPopup(HeroInventoryWindow.instance);});
    }

    public void UpdateWindow() {
        this.maxHP.SetText("Max HP : " + hero.currentStats.HP.ToString());
        this.atk.SetText("Atk : " + hero.currentStats.Atk.ToString());
        this.mAtk.SetText("M. Atk : " + hero.currentStats.MAtk.ToString());
        this.def.SetText("Def : " + hero.currentStats.Def.ToString());
        this.mDef.SetText("M. Def : " + hero.currentStats.MDef.ToString());
        this.acc.SetText("Acc : " + hero.currentStats.Acc.ToString());
        this.critChance.SetText("Crit. Chance : " + hero.currentStats.CritChance.ToString());
        this.critDmg.SetText("Crit. Dmg : " + hero.currentStats.CritDmg.ToString());
        this.eva.SetText("Eva : " + hero.currentStats.Eva.ToString());
        this.luck.SetText("Luck : " + hero.currentStats.Luck.ToString());
    }
}

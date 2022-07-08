using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroStatusHolder : MonoBehaviour
{
    [SerializeField] Image heroPortrait;
    [SerializeField] TextMeshProUGUI heroName;
    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI hpLostText;
    [SerializeField] Image xpBar;
    [SerializeField] TextMeshProUGUI xpText;
    [SerializeField] TextMeshProUGUI xpGainedText;


    public void SetHero(Hero hero, int xpGained) {
        heroPortrait.sprite = hero.heroClass.classSprite;
        heroName.SetText(hero.heroName);
        ProgressBarCalculation(hpBar, hero.baseStats.HP, hero.baseStats.HP, hero.currentStats.HP);
        hpText.SetText(hero.currentStats.HP + "/" + hero.baseStats.HP);
        hpLostText.SetText("");
        ProgressBarCalculation(xpBar, hero.XPtoNextLevel, hero.currentXP, hero.currentXP+xpGained);
        if(hero.currentXP + xpGained >= hero.XPtoNextLevel) {
            xpText.SetText("Level Up !");
        } else {
            xpText.SetText(hero.currentXP+xpGained + "/" + hero.XPtoNextLevel);
        }
        hero.AddXP(xpGained);
        xpGainedText.SetText("+" + xpGained + " XP");
    }

    void ProgressBarCalculation(Image progressBar, int maxValue, int startValue, int currentValue) {
        float currentValuePercentage = (float) startValue / (float) maxValue;
        float targetValuePercentage = (float) currentValue / (float) maxValue;
        progressBar.fillAmount = currentValuePercentage;
        LeanTween.value(this.gameObject, currentValuePercentage, targetValuePercentage, 0.5f).setDelay(1f).setOnUpdate((float value) => progressBar.fillAmount = value);
    }
}

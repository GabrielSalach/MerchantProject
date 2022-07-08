using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUnit : CharacterUnit
{
	[HideInInspector] public Hero hero;

	public override void Start() {
		base.Start();
		hero = GetComponent<Hero>();
		unitName = hero.heroName;
	}

	public override void OnClick() {
		base.OnClick();
		HeroInfoWindow.instance.SetHero(hero);
		HeroInfoWindow.instance.OpenWindow();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBuilder : MonoBehaviour {
    public void GenerateSkillDisplay(Skill skill) {
        Text text = gameObject.GetComponentInChildren<Text>();
        text.text = skill.skillName;
        Image image = gameObject.GetComponentInChildren<Image>();
        image.sprite = skill.skillIcon;
    }
}

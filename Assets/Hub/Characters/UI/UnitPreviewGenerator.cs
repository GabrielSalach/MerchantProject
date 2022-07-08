using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitPreviewGenerator : MonoBehaviour
{
    CharacterUnit unit;
    [SerializeField] Image characterSprite;
    [SerializeField] TextMeshProUGUI characterName;
    public Button button;

    public CharacterUnit GetUnit() {
        return unit;
    }

    public void SetUnit(CharacterUnit unit) {
        this.unit = unit;
        characterSprite.sprite = unit.GetComponent<SpriteRenderer>().sprite;
        characterName.SetText(unit.unitName);
    }
}

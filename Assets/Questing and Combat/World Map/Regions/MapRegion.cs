using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MapRegion : MonoBehaviour, Interactable
{
    public string regionName;
    public List<QuestMarker> questMarkers;
    public bool locked = true;
    bool selected;
    [SerializeField] TextMeshProUGUI regionNameText;


    Material spriteMaterial;

    void Start() {
        spriteMaterial = GetComponent<SpriteRenderer>().material;
        selected = false;
        regionNameText.SetText(regionName);
    }

    public void UpdateOpacity(float value) {
        spriteMaterial.SetFloat("_Opacity", value);
    }

    public void SetTextOpacity(float value) {
        regionNameText.CrossFadeAlpha(value, 0f, false);
    }

    public void OnClick() {
        SelectRegion();
    }

    public void SelectRegion() {
        if(selected == false) {
            MapBehavior.instance.FocusRegion(this);
            LeanTween.value(this.gameObject, SetTextOpacity, 1f, 0f, 0.1f);
            foreach(QuestMarker marker in questMarkers) {
                marker.LoadCloseLOD();
            }
            selected = true;
        }
    }

    public void UnselectRegion() {
        foreach(QuestMarker marker in questMarkers) {
            marker.LoadFarLOD();
        }
        selected = false;
        LeanTween.value(this.gameObject, SetTextOpacity, 0f, 1f, 0.1f);
    }

    public void HoverOn() {
        if(selected == false) {
            LeanTween.value(this.gameObject, UpdateOpacity, 0f, 1f, 0.1f);
        }
    }

    public void HoverOff() {
        if(selected == false) {
           LeanTween.value(this.gameObject, UpdateOpacity, 1f, 0f, 0.1f);
        }
    }

    public void UnlockRegion() {

    }     
}

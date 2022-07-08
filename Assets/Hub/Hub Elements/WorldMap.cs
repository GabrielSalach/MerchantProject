using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (SpriteOutliner))]
public class WorldMap : HubElement, Interactable
{

    SpriteOutliner spriteOutliner;
    [SerializeField] GameObject closeButton;


    void Start() {
        spriteOutliner = GetComponent<SpriteOutliner>();
    }

    public void CloseMap() {
        CameraController.instance.SwitchToHubView();
        MapBehavior.instance.UnfocusRegion();
        closeButton.SetActive(false);
    }

    public void OnClick() {
        CameraController.instance.SwitchToMapCamera();
        closeButton.SetActive(true);
    }

    public void HoverOn() {
        spriteOutliner.OutlineOn();
    }

    public void HoverOff() {
        spriteOutliner.OutlineOff();
    }
}

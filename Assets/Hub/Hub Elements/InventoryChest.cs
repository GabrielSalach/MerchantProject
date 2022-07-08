using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (SpriteOutliner))]
public class InventoryChest : HubElement, Interactable
{
    SpriteOutliner spriteOutliner;

    void Start() {
        spriteOutliner = GetComponent<SpriteOutliner>();
    }

    public void OnClick() {
        InventoryWindow.instance.OpenWindow();
    }

    public void HoverOn() {
        spriteOutliner.OutlineOn();
    }

    public void HoverOff() {
        spriteOutliner.OutlineOff();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehavior : MonoBehaviour, Interactable
{
    public static MapBehavior instance;
    MapRegion focusedRegion;

    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    public void OnClick() {
        UnfocusRegion();
    }

    public void HoverOn() {
    }

    public void HoverOff() {
    }

    public List<MapRegion> regions;

    public void FocusRegion(MapRegion region) {
        focusedRegion = region;
        CameraController.instance.SelectTarget(region.transform);
    }

    public void UnfocusRegion() {
        if(focusedRegion != null) {
            CameraController.instance.UnselectTarget();
            focusedRegion.UnselectRegion();
            focusedRegion = null;
        }
    }
}

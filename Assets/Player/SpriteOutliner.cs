using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (SpriteRenderer))]
public class SpriteOutliner : MonoBehaviour
{
    Material outlineMaterial;

    void Start() {
        outlineMaterial = GetComponent<SpriteRenderer>().material;
    }

    public void OutlineOn() {
        outlineMaterial.SetFloat("_OutlineOpacity", 1);
    }

    public void OutlineOff() {
        outlineMaterial.SetFloat("_OutlineOpacity", 0);
    }

}

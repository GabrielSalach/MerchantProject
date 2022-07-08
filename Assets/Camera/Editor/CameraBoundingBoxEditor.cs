using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (CameraBoundingBox))]
public class CameraBoundingBoxEditor : Editor
{
    public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		CameraBoundingBox boundingBox = (CameraBoundingBox)target;
        if(GUILayout.Button("Bake Bounding Box")) {
			boundingBox.BakeBoundingBox();
        }
	}
}

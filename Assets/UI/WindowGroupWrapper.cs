using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowGroupWrapper : MonoBehaviour
{
    [SerializeField] WindowGroup group;

    void Awake() {
        Debug.Log("Awake");
        if(group == null) {
            Debug.Log("New Group");
            group = new WindowGroup();
        } else {
            Debug.Log(group);
        }
    }

    void Start() {
        foreach(Window window in group) {
            Debug.Log(window);
        }
    }

    public void Clear() {
        WindowGroup.allGroups.Clear();
        WindowGroup.allGroups = null;
        foreach(WindowGroup group in WindowGroup.allGroups) {
            Debug.Log(group);
        }
    }
}

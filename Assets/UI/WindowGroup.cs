using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WindowGroup : List<Window>
{  
    public static List<WindowGroup> allGroups;


    public WindowGroup() : base() {        
        if(allGroups == null) {
            allGroups = new List<WindowGroup>();
        }
        allGroups.Add(this);

    }
}

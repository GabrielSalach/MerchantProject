using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubElement : MonoBehaviour 
{
    public enum ElementType{Miscellaneous, StairsTop, StairsBottom};

    public ElementType elementType;
    public int floor;

    public static int CompareElementByPosition(HubElement x, HubElement y) {
        int returnValue = 0;
        if(x == null) {
        } else if(y == null) {
        } else if(x.transform.position.x > y.transform.position.x) {
            returnValue = 1;
        } else if(x.transform.position.x < y.transform.position.x) {
            returnValue = -1;
        }
        return returnValue;
    }
}

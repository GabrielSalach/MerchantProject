using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Hub/RoomData", order = 1)]
public class RoomData : ScriptableObject
{
    public enum RoomType {Empty, Entrance, Shop, Blacksmith, Woodworker, Tavern, Brewery, Barracks, Stairs};
    public RoomType roomType;
}

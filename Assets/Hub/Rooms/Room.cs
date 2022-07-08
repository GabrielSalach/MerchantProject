using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{   
    [HideInInspector] public static List<Room> allRooms;

    public RoomsManager.Floor floor;
    public RoomData roomData;
    public bool leftNeighbor;
    public bool rightNeighbor;
    public int roomSize;

    // Room elements references
    [HideInInspector] public Transform backGroundWall;
    [HideInInspector] public Transform leftWall;
    [HideInInspector] public Transform rightWall;
    [HideInInspector] public Transform ground;
    [HideInInspector] public Transform ceiling;
    [HideInInspector] public Transform tool1;
    [HideInInspector] public Transform tool2;
    [HideInInspector] public Transform stairs;


    void Awake() {
        // Adds this room to the list containing all rooms 
        if(allRooms == null) {
            allRooms = new List<Room>();
        }
        allRooms.Add(this);
    }

    void Start() {
        // Assign transforms
        backGroundWall = transform.Find("Background Wall");
        leftWall = transform.Find("Left Wall");
        rightWall = transform.Find("Right Wall");
        ground = transform.Find("Ground");
        ceiling = transform.Find("Ceiling");
        tool1 = transform.Find("Tool 1");
        tool2 = transform.Find("Tool 2");
        stairs = transform.Find("Stairs");
        // Builds the room
        RoomsManager.instance.BuildRoom(this);
    }

    // Returns the room index in allRooms
    public int GetRoomIndex() {
        return allRooms.IndexOf(this);
    }

}

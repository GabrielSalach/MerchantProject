using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour {
    public enum Floor{Basement, GroundFloor};

    // All possible room sprites
    [Header("Room textures")]
    [SerializeField] Sprite basementWall;
    [SerializeField] Sprite woodenWall;
    [SerializeField] Sprite leftWall;
    [SerializeField] Sprite rightWall;
    [SerializeField] Sprite leftWallDoor;
    [SerializeField] Sprite rightWallDoor;
    [SerializeField] Sprite stoneFloor;
    [SerializeField] Sprite stoneCeiling;
    [SerializeField] Sprite woodenCeiling;
    [SerializeField] Sprite stairs;

    [Header("Tools textures")]
    [SerializeField] Sprite anvil;
    [SerializeField] Sprite forge;
    [SerializeField] Sprite workbench;
    [SerializeField] Sprite woodLog;
    

    // Calculates this automatically maybe, so I can change sprite shape without affecting this
    /* float backgroundWallHeight = 3;
    float backgroundWallWidth = 5.5f;
    float borderOffset = 0.078125f; */

    // Singleton instance
    public static RoomsManager instance;

    // Singleton logic
    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    // Sets the right assets in the room
    public void BuildRoom(Room room) {

        // Used to generate collisions
        List<Vector2> customColliderPoints = new List<Vector2>();

        // Selects the right type of material depending on the floor
        switch (room.floor) {
            case Floor.Basement: 
                room.backGroundWall.GetComponent<SpriteRenderer>().sprite = basementWall;
                room.ground.GetComponent<SpriteRenderer>().sprite = stoneFloor;
                room.ceiling.GetComponent<SpriteRenderer>().sprite = stoneCeiling;
                break;
            case Floor.GroundFloor: 
                room.backGroundWall.GetComponent<SpriteRenderer>().sprite = woodenWall;
                room.ground.GetComponent<SpriteRenderer>().sprite = stoneFloor;
                room.ceiling.GetComponent<SpriteRenderer>().sprite = woodenCeiling;
                break;
            default:
                break;
        }
        // Generates left wall
        if(room.leftNeighbor == true) {
            // Sets sprite
            room.leftWall.GetComponent<SpriteRenderer>().sprite = leftWallDoor;
            // Generates collisions
            leftWallDoor.GetPhysicsShape(0, customColliderPoints);
            room.leftWall.GetComponent<PolygonCollider2D>().SetPath(0, customColliderPoints);
        } else {
            // Sets sprite
            room.leftWall.GetComponent<SpriteRenderer>().sprite = leftWall;
            // Generates collisions
            leftWall.GetPhysicsShape(0, customColliderPoints);
            room.leftWall.GetComponent<PolygonCollider2D>().SetPath(0, customColliderPoints);
        }
        
        // Generates right wall
        if(room.rightNeighbor == true) {
            // Sets sprite
            room.rightWall.GetComponent<SpriteRenderer>().sprite = rightWallDoor;
            // Generates collision
            rightWallDoor.GetPhysicsShape(0, customColliderPoints);
            room.rightWall.GetComponent<PolygonCollider2D>().SetPath(0, customColliderPoints);
        } else {
            // Sets sprite
            room.rightWall.GetComponent<SpriteRenderer>().sprite = rightWall;
            // Generates collision
            rightWall.GetPhysicsShape(0, customColliderPoints);
            room.rightWall.GetComponent<PolygonCollider2D>().SetPath(0, customColliderPoints);
        }

        // Generates tools
        switch(room.roomData.roomType) {
            case RoomData.RoomType.Blacksmith:
                room.tool1.GetComponent<SpriteRenderer>().sprite = forge;
                room.tool2.GetComponent<SpriteRenderer>().sprite = anvil;
                break;
            case RoomData.RoomType.Woodworker:
                room.tool1.GetComponent<SpriteRenderer>().sprite = woodLog;
                room.tool2.GetComponent<SpriteRenderer>().sprite = workbench;
                break;
            default: 
                room.tool1.GetComponent<SpriteRenderer>().sprite = null;
                room.tool2.GetComponent<SpriteRenderer>().sprite = null;
                break;
        }

        // Generates stairs
        if(room.roomData.roomType == RoomData.RoomType.Stairs) {
            room.stairs.GetComponent<SpriteRenderer>().sprite = stairs;
            stairs.GetPhysicsShape(0, customColliderPoints);
            room.stairs.GetComponent<PolygonCollider2D>().SetPath(0, customColliderPoints);
        } else {
            room.stairs.GetComponent<PolygonCollider2D>().enabled = false;
            room.stairs.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}


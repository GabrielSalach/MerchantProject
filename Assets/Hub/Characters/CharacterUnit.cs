using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (SpriteOutliner))]
public class CharacterUnit : MonoBehaviour, Interactable
{
    /* ###### GENERAL ###### */
    public string unitName;
    /* ###### MOVEMENT ###### */
    // Is the character moving
    public bool isMoving;
    // Speed of the unit
    public float speed;

    // Reference to rigidbody
    public Rigidbody2D rb;
    // Minimum distance required between the unit and the object for the unit to stop 
    public float stopRadius = 0.0016f;

    // Floor the unit is on currently
    public int currentFloor;

    // Movement commands queue
    Queue<Command> commandQueue;
    Command currentCommand;

    /* ###### PATHFINDING ###### */
    // Reference to collider, used to calculate raycast origin 
    public Collider2D unitCollider;
    public bool arrivedAtDestination;

    /* ###### SPRITE OUTLINE ###### */
    SpriteOutliner spriteOutliner;


    public virtual void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        commandQueue = new Queue<Command>();
        unitCollider = GetComponent<Collider2D>();
        arrivedAtDestination = false;
        spriteOutliner = GetComponent<SpriteOutliner>();
    }

    public virtual void Update() {
        ProcessCommands();
    }

    public virtual void OnClick() {
        CameraController.instance.SelectTarget(this.transform);
    }

    public virtual void HoverOn() {
        spriteOutliner.OutlineOn();
    }

    public virtual void HoverOff() {
        spriteOutliner.OutlineOff();
    }

    void FixedUpdate() {
        if(currentCommand != null) {
            currentCommand.Execute();
        }
    }

    void ProcessCommands() {
        if(currentCommand == null || currentCommand.Done == true) {
            if(commandQueue.Count > 0) {
                currentCommand = commandQueue.Dequeue();
            } else {
                currentCommand = null;
            }
        }
    }

    public void MoveToElement(HubElement element) {
        arrivedAtDestination = false;
        if(currentFloor == element.floor) {
            commandQueue.Enqueue(new MovementCommand(this, element.transform.position));
            commandQueue.Peek().SetDoneCallback(delegate {arrivedAtDestination = true;});
        } else if(currentFloor > element.floor) {
            StairElement stairsTop = (StairElement) FindNearestElementOfType(HubElement.ElementType.StairsTop);
            if(stairsTop != null) {
                commandQueue.Enqueue(new MovementCommand(this, stairsTop.transform.position));
                StairsCommand stairsCommand = new StairsCommand(this, stairsTop);
                stairsCommand.SetDoneCallback(delegate {MoveToElement(element);});
                commandQueue.Enqueue(stairsCommand);

            } else {
                NotificationsManager.TriggerNotification("Unit can't reach the target object !");
            }
        } else {
            StairElement stairsBottom = (StairElement) FindNearestElementOfType(HubElement.ElementType.StairsBottom);
            if(stairsBottom != null) {
                commandQueue.Enqueue(new MovementCommand(this, stairsBottom.transform.position));
                StairsCommand stairsCommand = new StairsCommand(this, stairsBottom);
                stairsCommand.SetDoneCallback(delegate {MoveToElement(element);});
                commandQueue.Enqueue(stairsCommand);
            } else {
                NotificationsManager.TriggerNotification("Unit can't reach the target object !");
            }
        }
    }

    // Returns the HubElement that is the closest to this CharacterUnit with the matching type, or null if none are found
    public HubElement FindNearestElementOfType(HubElement.ElementType type) {
        // HubElement returned by the method
        HubElement returnElement = null;
        // Origin of the raycast
        Vector2 raycastOrigin = new Vector2();
        raycastOrigin.x = transform.position.x;
        raycastOrigin.y = unitCollider.bounds.min.y;
        // Layer that contains the HubElements, the raycast only hit objects on this layer
        int layerMask = LayerMask.GetMask("HubElement");
        // Cast Raycasts in both directions
        RaycastHit2D[] leftSideHits = Physics2D.RaycastAll(raycastOrigin, Vector2.left, 50, layerMask/* Replace by floor width/2 afterwards */); 
        RaycastHit2D[] rightSideHits = Physics2D.RaycastAll(raycastOrigin, Vector2.right, 50, layerMask/* Replace by floor width/2 afterwards */); 

        HubElement firstFoundLeft = null;
        HubElement firstFoundRight = null;
        HubElement buffer = null;
        foreach(RaycastHit2D hit in leftSideHits) {
            buffer = hit.transform.GetComponent<HubElement>();
            if(firstFoundLeft == null && buffer != null) {
                if(buffer.elementType == type) {
                    firstFoundLeft = buffer;
                }
            }
        }

        foreach(RaycastHit2D hit in rightSideHits) {
            buffer = hit.transform.GetComponent<HubElement>();
            if(buffer != null) {
                if(firstFoundRight == null && buffer.elementType == type) {
                    firstFoundRight = buffer;
                }
            }
        }

        // Gets the distance for the nearest element on each side 
        float distanceLeft = Mathf.Infinity;
        if(firstFoundLeft != null) {
            distanceLeft = Mathf.Abs(transform.position.x - firstFoundLeft.transform.position.x);
        }
        float distanceRight = Mathf.Infinity;
        if(firstFoundRight != null) {
            distanceRight = Mathf.Abs(transform.position.x - firstFoundRight.transform.position.x);
        }

        // Compares the distance and returns the closest HubElement
        if(distanceLeft > distanceRight) {
            returnElement = firstFoundRight;
        } else if(distanceLeft < distanceRight) {
            returnElement = firstFoundLeft;
        }
        return returnElement;
    }

}

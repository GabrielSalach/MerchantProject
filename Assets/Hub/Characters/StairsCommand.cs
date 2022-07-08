using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsCommand : Command
{
    CharacterUnit unit;
    StairElement stair;
    Vector2 movementVector;
    bool slopeLogicActivated;


    public StairsCommand(CharacterUnit unit, StairElement stairElement) {
        this.unit = unit;
        this.stair = stairElement;
        movementVector = new Vector2();
        slopeLogicActivated = false;
    }

    public override void SetDoneCallback(OnDone onDoneDelegate) {
        this.onDone = onDoneDelegate;
    }

    public override void Execute() {
        if(slopeLogicActivated == false) {
            ToggleSlopeLogic();
        }

        movementVector.x = Mathf.Sign(stair.otherEnd.transform.position.x - unit.transform.position.x) * unit.speed;
        movementVector.y = unit.rb.velocity.y;
        unit.rb.velocity = movementVector;
        if(Mathf.Abs(unit.transform.position.x - stair.otherEnd.transform.position.x) < unit.stopRadius) {
            unit.rb.velocity = Vector2.zero;
            unit.currentFloor = stair.otherEnd.floor;
            ToggleSlopeLogic();
            if(onDone != null) 
                onDone();
            Done = true;
        }

    }

    void ToggleSlopeLogic() {
        if(slopeLogicActivated == true) {
            unit.gameObject.layer = 6;
            slopeLogicActivated = false;
        } else {
            unit.gameObject.layer = 8;
            slopeLogicActivated = true;
        }
    }
}

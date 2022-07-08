using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCommand : Command
{
    CharacterUnit unit;
    Vector2 targetPosition;
    Vector2 movementVector;

    public MovementCommand(CharacterUnit unit, Vector2 position) {
        this.unit = unit;
        this.targetPosition = position;
        this.movementVector = new Vector2();
    }

    public override void SetDoneCallback(OnDone onDoneDelegate) {
        this.onDone = onDoneDelegate;
    }

    public override void Execute() {
        movementVector.x = Mathf.Sign(targetPosition.x - unit.transform.position.x) * unit.speed;
        movementVector.y = unit.rb.velocity.y;
        unit.rb.velocity = movementVector;
        if(Mathf.Abs(unit.transform.position.x - targetPosition.x) < unit.stopRadius) {
            unit.rb.velocity = Vector2.zero;
            if(onDone != null) 
                onDone();
            Done = true;
        }
    }
}

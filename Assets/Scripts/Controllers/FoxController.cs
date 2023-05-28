using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : AnimalController
{
    public float dashDistance = 4;
    private Vector3 heading;

    public override void Move(Vector3 vector, float speed){
        base.Move(vector, speed);
        heading = vector;
    }

    public override void Interact()
    {
        DoDash();
    }
    void DoDash(){
        movementController.Move(heading * speed * dashDistance * Time.deltaTime);
    }
    IEnumerator Dash()
    {
        yield return
    }
}

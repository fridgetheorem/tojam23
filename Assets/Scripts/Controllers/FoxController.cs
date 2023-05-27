using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : AnimalController
{
    public float dashDistance = 2;

    public override void Interact(GameObject target)
    {
        base.Interact(target);
    }
    void DoDash(){
        // Wait for movement info
    }
}

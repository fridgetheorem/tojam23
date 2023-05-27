using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : AnimalController
{
    public float dashDistance = 2;

    public override void Interact()
    {
        DoDash();
    }
    void DoDash(){
        // Wait for movement info
    }
}

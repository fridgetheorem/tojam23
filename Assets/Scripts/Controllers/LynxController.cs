using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LynxController : AnimalController
{
    public float screamRadius = 5;
    // Start is called before the first frame update

    public override void Interact()
    {
#if DEBUG
        Debug.Log("Screech!");
#endif

        DoScream();
    }

    void DoScream(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, screamRadius);
        if(hitColliders.Length == 0) return;

        // Probably wanna play some sound

        // We hit some collider
        // Do something with this information
        foreach ( Collider collider in hitColliders ){
            if (!collider.GetComponent<CharacterController>()) return;
            // Move all characters back to the edge of the circle
        }


    }
}

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
            // If the layer is Fire, remove it
            if (collider.gameObject.layer == LayerMask.NameToLayer("Fire")){
                collider.gameObject.GetComponentInParent<Fire>().DestroyFire();
            }
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : AnimalController
{
    public float hitDamage = 1;
    public float attackDistance = 1;
    public float attackRadius = 1;
    // Start is called before the first frame update
    public override void Interact()
    {
        DoAttack();
    }

    void DoAttack(){
# if DEBUG
        Debug.Log("Attacking");
# endif
        // Do a sphere cast?
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackRadius, transform.forward, attackDistance);

        // Damage each enemy 
        foreach (RaycastHit hit in hits){
# if DEBUG
            Debug.Log("Hit", hit.collider.gameObject);
# endif
            IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();
            damageable.BeDamaged(hitDamage);
        }
    }
}

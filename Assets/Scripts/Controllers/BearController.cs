#undef DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : AnimalController
{
    public float hitDamage = 1;
    public float attackDistance = 1;
    public float attackRadius = 1;

    [SerializeField] GameObject attackVisual;
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
        
        //Destroy(Instantiate(attackVisual), .2f);

        // Damage each enemy 
        foreach (RaycastHit hit in hits){
# if DEBUG
            Debug.Log("Hit", hit.collider.gameObject);
# endif
            IDamageable animal = hit.collider.GetComponent<IDamageable>();
            if(
                animal is AnimalController 
                && PartyController.playerParty.IsInParty((AnimalController)animal)
                )
            {
                // Maybe we want a better solution for this in the future, but for now it works
                return;
            } 
            animal?.BeDamaged(hitDamage);
        }
    }
}

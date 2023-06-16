#undef DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : AnimalController
{
    public float hitDamage = 1;
    public float attackDistance = 1;
    public float attackRadius;

    [SerializeField]
    GameObject attackVisual;
    
    [SerializeField]
    private float _attackCooldown = 1f;
    private bool _canAttack = true;

    public override void Interact()
    {

# if DEBUG
        Debug.Log("Attacking");
# endif
        DoAttack();
    }

    void DoAttack(){

        if (!_canAttack) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius, LayerMask.GetMask("Enemy"));
        if(hitColliders.Length > 0) {
            // Probably wanna play some sound;

            // We hit some collider
            // Do something with this information
            foreach ( Collider collider in hitColliders ){
                // If the layer is Enemy, damage it
                EnemyDamage ed = collider.gameObject.GetComponentInChildren<EnemyDamage>();
                if (ed != null && ed.transform.parent.GetComponent<EnemyController>() != null) {
                    ed.transform.parent.GetComponent<EnemyController>().BeDamaged(hitDamage);
                }
            }
        }
        _canAttack = false;
        StartCoroutine(AttackCooldown(_attackCooldown));  
    }

    IEnumerator AttackCooldown(float length)
    {
        float elapsedTime = 0;
        while (elapsedTime < length){
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _canAttack = true;
        yield return null;
    }

    void DoAttackOld() {
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

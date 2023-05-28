using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyController : AnimalController
{

    SphereCollider trigger;
    [SerializeField] private EnemyBehaviour behaviour = EnemyBehaviour.Aggressive;
    [SerializeField] private float aggroRange = 5f;
    [SerializeField] private float contactDamage = 1f;
    // Enemies are animals with their logic defined


    void Awake(){
        base.Awake();

        trigger = GetComponent<SphereCollider>();
        
        trigger.radius = aggroRange;
        movementController.radius = size;
    }

    void LookForPlayer(){
        // Collision bounding box(?)
    }

    void OnTriggerStay(Collider collider){
        PartyController party;
        if(!(party = collider.gameObject.GetComponent<PartyController>())) return;
        TrackPlayerRoutine(party);
    }

    void OnControllerColliderHit(ControllerColliderHit collision){
        AnimalController damageable = collision.gameObject.GetComponent<AnimalController>();
        if(damageable == null) return;
        damageable.BeDamaged(contactDamage);
    }

    void TrackPlayerRoutine(PartyController party){
        switch ( behaviour ){
            case EnemyBehaviour.Aggressive:
                Move(party.transform.position - transform.position, speed);
                break;
            case EnemyBehaviour.Passive:
                // Just do nothing
                break;
            default:
                break;

        }

    }
}

enum EnemyBehaviour 
{
    Aggressive,
    Passive
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyController : AnimalController
{

    SphereCollider trigger;
    CapsuleCollider model;
    [SerializeField] private EnemyBehaviour behaviour = EnemyBehaviour.Aggressive;
    [SerializeField] private float aggroRange = 5f;
    [SerializeField] private float contactDamage = 1f;
    // Enemies are animals with their logic defined

    private PartyController player;

    void Awake(){
        base.Awake();

        trigger = GetComponent<SphereCollider>();
        model = GetComponent<CapsuleCollider>();
        
        trigger.radius = aggroRange;
        model.radius = size;
    }

    void LookForPlayer(){
        // Collision bounding box(?)
    }

    void OnTriggerEnter(Collider collider){
        Debug.Log("TriggerEnter");
        if(!(player = collider.gameObject.GetComponent<PartyController>())) return;
        Debug.Log("The player has been found");
        // We have found the player...
    }
    public new void FixedUpdate () {}

    void OnCollisionEnter(Collision collision){
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if(damageable == null) return;

        // We have collided with something that can take damage, 
        //      attempt to damage them.

        damageable.BeDamaged(contactDamage);
    }

    void TrackPlayerRoutine(PartyController player){
        switch ( behaviour ){
            case EnemyBehaviour.Aggressive:
                break;
            case EnemyBehaviour.Cowardly:
                break;
            case EnemyBehaviour.Smart:
                break;

        }

    }
    void AggressiveAction(){

    }
}

enum EnemyBehaviour 
{
    Aggressive,
    Cowardly,
    Smart
}
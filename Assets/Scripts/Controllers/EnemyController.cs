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
    private float deathFade = .8f;


    void Awake(){
        base.Awake();

        trigger = GetComponent<SphereCollider>();
        
        trigger.radius = aggroRange;
        movementController.radius = size;
        Death += DeathBehaviour;
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

    public void DeathBehaviour(){
        Collider[] colliders = GetComponents<Collider>();
        foreach(var collider in colliders){
            Destroy(collider);
        }
        StartCoroutine(
            FadeSelf(deathFade)
        );
    }

    IEnumerator FadeSelf(float fadeDuration)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Color initialColor = spriteRenderer.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        float elapsedTime = 0;
        while (elapsedTime < fadeDuration){
            elapsedTime += Time.deltaTime;
            spriteRenderer.material.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
}

enum EnemyBehaviour 
{
    Aggressive,
    Passive
}
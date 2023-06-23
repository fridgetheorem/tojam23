using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyController : AnimalController
{
    SphereCollider trigger;

    [Header("Enemy Properties")]
    [SerializeField]
    private EnemyBehaviour behaviour = EnemyBehaviour.Passive;

    [SerializeField]
    private float aggroRange = 5f;

    [SerializeField]
    private float contactDamage = 1f;

    // Enemies are animals with their logic defined
    private float deathFade = .8f;

    private EnemyDamage ed;

    public SpriteRenderer spriteRenderer;

    new void Awake()
    {
        base.Awake();

        trigger = GetComponent<SphereCollider>();

        trigger.radius = aggroRange;
        movementController.radius = size;
        Death += DeathBehaviour;
        HealthChanged += DamageBehaviour;

        ed = GetComponentInChildren<EnemyDamage>();
        ed.setDamage(contactDamage);
    }

    void LookForPlayer()
    {
        // Collision bounding box(?)
    }

    void OnTriggerStay(Collider collider)
    {
        PartyController party;
        if (!(party = collider.gameObject.GetComponentInChildren<PartyController>()))
            return;

        behaviour = EnemyBehaviour.Aggressive;
        TrackPlayerRoutine(party);
    }

    void OnTriggerExit(Collider other)
    {
        AnimalController animal = other.gameObject.GetComponent<AnimalController>();
        if (animal != null)
        {
            behaviour = EnemyBehaviour.Passive;
            UpdateAnimator("Moving", false);
        }
    }

    void TrackPlayerRoutine(PartyController party)
    {
        switch (behaviour)
        {
            case EnemyBehaviour.Aggressive:
                Vector3 movementVec = party.transform.position - transform.position;
                if (!ed.IsHurting())
                    Move(movementVec, speed);
                AnimateEnemy(new Vector2(movementVec.x, movementVec.z));
                break;
            case EnemyBehaviour.Passive:
                // Just do nothing
                break;
            default:
                break;
        }
    }

    void AnimateEnemy(Vector2 movement2D)
    {
        Animate(movement2D);

        // The value 1.35 is the distance where the enemy starts hugging the player (no need to make them move then).
        UpdateAnimator(
            "Moving",
            movement2D.magnitude > 1.35f && behaviour == EnemyBehaviour.Aggressive
        );
        FlipSprite(movement2D.x < 0);
        UpdateAnimator("Down", movement2D.y >= 0);
        // PlayFootsteps(movement2D); TODO: Enemy local audio footsteps
    }

    public void DamageBehaviour(float garbage1, float garbage2)
    {
        if (health > 0)
        {
            StopCoroutine(TakeDamage());
            StartCoroutine(TakeDamage());
        }
    }

    IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }

    public void DeathBehaviour()
    {
        speed = 0;
        Collider[] colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            //Destroy(collider);
        }
        GameObject.FindGameObjectWithTag("FireSFX").GetComponent<AudioSource>().Play();
        Destroy(ed);
        StartCoroutine(FadeSelf(deathFade));
    }

    IEnumerator FadeSelf(float fadeDuration)
    {
        Color initialColor = spriteRenderer.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            spriteRenderer.material.color = Color.Lerp(
                initialColor,
                targetColor,
                elapsedTime / fadeDuration
            );
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

#undef DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
// Damageable dudes
public abstract class AnimalController : Health
{
    [Header("Animal Properties")]
    [SerializeField]
    protected CharacterController movementController;

    //[SerializeField] private float idleRadius = 2f;

    // should be properties not variables but... fuck it we ball
    [SerializeField]
    public float speed = 10;

    [HideInInspector]
    public float originalSpeed = 10f;

    [SerializeField]
    public float size = 1;

    [SerializeField]
    private Transform model;

    public CinemachineVirtualCamera virtualCamera;

    private float originalYPos;

    private float invulnerabilityTimer = 1f;
    private bool invulnerable = false;

    protected PartyController party;

    public Vector3 idleScale = Vector3.zero;
    public Vector3 movingScale = Vector3.zero;

    public AudioClip specialAbilitySFX;
    public AudioClip runningSFX;

    public void Start()
    {
        originalYPos = transform.position.y;
        if (idleScale == Vector3.zero)
            idleScale = GetComponentInChildren<SpriteRenderer>().transform.localScale;
        if (movingScale == Vector3.zero)
            movingScale = GetComponentInChildren<SpriteRenderer>().transform.localScale;
    }

    public void SetPartyAffiliation(PartyController party)
    {
        this.party = party;
    }

    public override void BeDamaged(float amount)
    {
        if (invulnerable)
            return;

        health -= amount;
        if (health <= 0)
            return;

        StartCoroutine(BecomeInvincible(invulnerabilityTimer));
    }

    protected IEnumerator BecomeInvincible(float time)
    {
        invulnerable = true;
        yield return new WaitForSeconds(time);
        invulnerable = false;
    }

#if DEBUG
    void Update(){
        bool interacting = Input.GetButtonDown("Fire1");
        // Interaction
        if (interacting) Interact();
    }
    protected void FixedUpdate(){
        // Gather Inputs
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        // Movements
        Vector3 motion = new Vector3(horizontalInput, 0, verticalInput).normalized;
        Move(motion, speed);

    }
#endif

    protected void Awake()
    {
        movementController = GetComponent<CharacterController>();
        originalSpeed = speed;
    }

    public void LockY()
    {
        transform.position = new Vector3(transform.position.x, originalYPos, transform.position.z);
    }

    public virtual void Move(Vector3 vector, float speed)
    {
        Vector3 movementVector = vector.normalized * speed * Time.deltaTime;
        movementController?.Move(movementVector);
        LockY();

        Vector3 worldLookAt = new Vector3(
            vector.normalized.x + transform.position.x,
            0,
            vector.normalized.z + transform.position.z
        );
    }

    public void PlaySFX()
    {
        if (specialAbilitySFX != null)
        {
            AudioSource ad = GameObject
                .FindGameObjectWithTag("SpecialAbilitySFX")
                .GetComponent<AudioSource>();
            ad.clip = specialAbilitySFX;
            ad.Play();
        }
    }

    public void Animate(Vector2 movementInput)
    {
        // Note this gets the sprite renderer because its the first object in the hierarchy.
        GetComponentInChildren<Animator>()
            .SetBool("Moving", movementInput.magnitude > 0);
        GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale =
            (movementInput.magnitude > 0)
                ? movingScale // Enlarge sprite when running.
                : idleScale; // Shrink sprite back to original size.

        ExtraUpdateAnimatons(movementInput); // Animal-specific animations.
    }

    public void PlayFootsteps(Vector2 movementInput)
    {
        AudioSource ad = GameObject.FindGameObjectWithTag("RunningSFX").GetComponent<AudioSource>();
        if (movementInput.magnitude > 0)
        {
            if (!ad.isPlaying || ad.clip != runningSFX)
            {
                ad.clip = runningSFX;
                ad.Play();
            }
        }
        else
            ad.Pause();
    }

    public void UpdateAnimator(string name, bool value)
    {
        // Note this gets the sprite renderer because its the first object in the hierarchy.
        GetComponentInChildren<Animator>()
            .SetBool(name, value);
    }

    public void FlipSprite(bool value)
    {
        // Note this gets the sprite renderer because its the first object in the hierarchy.
        GetComponentInChildren<SpriteRenderer>().flipX = value;
    }

    // For animal-specific animations like bear and fox.
    public virtual void ExtraUpdateAnimatons(Vector2 movementInput)
    {
        return;
    }

    // For animal-specific animations like bear and fox.
    public virtual void ExtraMoveAnimatons(Vector2 movementInput)
    {
        return;
    }

    // INTERACT
    // The special ability implemented by each animal
    // To be overridden by each animal type
    public virtual void Interact()
    {
        throw new NotImplementedException();
    }
}

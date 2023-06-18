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

    private void Start()
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

    // INTERACT
    // The special ability implemented by each animal
    // To be overridden by each animal type
    public virtual void Interact()
    {
        throw new NotImplementedException();
    }
}

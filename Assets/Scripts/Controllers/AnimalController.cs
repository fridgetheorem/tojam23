#undef DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]

// Damageable dudes
public abstract class AnimalController : Health
{
    [SerializeField] protected CharacterController movementController;

    [SerializeField] private float idleRadius = 2f;

    // should be properties not variables but... fuck it we ball
    [SerializeField] public float speed = 10;
    public float originalSpeed = 10f;
    [SerializeField] public float size = 1;
    [SerializeField] private Transform model;

    private float invulnerabilityTimer = 1f;

    protected override void BeDamaged(float amount){
        // Want there to be some level of invincibility
        // Flash for 1 second type beat
        health -= amount;
        if (health <= 0) return;

        StartCoroutine(BecomeInvincible(invulnerabilityTimer));
    }

    protected IEnumerator BecomeInvincible(float time){
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        if(!collider) yield break;
        yield return new WaitForSeconds(time);

        collider.enabled = true;
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

    public virtual void Move(Vector3 vector, float speed){
        Vector3 movementVector = vector.normalized * speed * Time.deltaTime;
        movementController?.Move(movementVector);

        Vector3 worldLookAt = new Vector3(
            vector.normalized.x + transform.position.x, 
            0, 
            vector.normalized.z + transform.position.z
        );

    }

    // INTERACT
    // The special ability implemented by each animal
    // To be overridden by each animal type
    public virtual void Interact (){
        throw new NotImplementedException();
    }

}

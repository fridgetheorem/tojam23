using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]

// Damageable dudes
public abstract class AnimalController : Health
{
    public CharacterController movementController;

    [SerializeField] private float idleRadius = 2f;

    [SerializeField] protected float speed = 10;
    [SerializeField] protected float size = 1;

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

    protected void Start()
    {
        movementController = GetComponent<CharacterController>();
    }

    public void Move(Vector3 vector, float speed){
        movementController.Move(vector * speed);
    }

    // INTERACT
    // The special ability implemented by each animal
    // To be overridden by each animal type
    public virtual void Interact (){
        throw new NotImplementedException();
    }

}

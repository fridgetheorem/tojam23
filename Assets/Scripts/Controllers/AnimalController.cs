using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

// Damageable dudes
public abstract class AnimalController : Health
{
    public CharacterController movementController;

    [SerializeField] protected float speed = 10;
    [SerializeField] protected float size = 1;

    // Start is called before the first frame update
    #if DEBUG
    void FixedUpdate(){
        // Gather Inputs
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        bool interacting = Input.GetButtonDown("Fire1");
        //---------------

        // Movements
        Vector3 motion = new Vector3(horizontalInput, 0, verticalInput).normalized;
        Move(motion, speed);

        // Interaction
        if (interacting) Interact();
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
    public virtual void Interact (GameObject target){
        throw new NotImplementedException();
    }

}

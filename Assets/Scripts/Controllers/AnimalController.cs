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
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 motion = new Vector3(horizontalInput, 0, verticalInput).normalized;
        Move(motion, speed);
    }
    #endif    

    void Start()
    {
        movementController = GetComponent<CharacterController>();
    }

    public void Move(Vector3 vector, float speed){
        movementController.Move(vector * speed);
    }

    // To be overridden by each animal type
    public virtual void Interact (GameObject target){
        // Don't call this, call the inheritors
        throw new NotImplementedException();
    }

}

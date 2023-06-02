#undef DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

// Damageable dudes
public abstract class AnimalController : Health
{
    [SerializeField] protected CharacterController movementController;

    //[SerializeField] private float idleRadius = 2f;

    // should be properties not variables but... fuck it we ball
    [SerializeField] public float speed = 10;
    public float originalSpeed = 10f;
    [SerializeField] public float size = 1;
    [SerializeField] private Transform model;

    private float originalYPos;


    public Guid id{
        get;
        private set;
    }

    private float invulnerabilityTimer = 1f;
    private bool invulnerable = false;

    protected PartyController party;

    private void Start() {
        originalYPos = transform.position.y;
    }
    
    public void SetPartyAffiliation(PartyController party){
        this.party = party;
    }

    public override void BeDamaged(float amount){
        if (invulnerable) return;

        health -= amount;
        if (health <= 0) return;

        StartCoroutine(BecomeInvincible(invulnerabilityTimer));
    }

    protected IEnumerator BecomeInvincible(float time){
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
        id = new Guid();
    }

    public void LockY() {
        transform.position = new Vector3(transform.position.x, originalYPos, transform.position.z);
    }

    public virtual void Move(Vector3 vector, float speed){
        Vector3 movementVector = vector.normalized * speed * Time.deltaTime;
        movementController?.Move(movementVector);
        LockY();

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
    

    #region Operators
    //
    public static bool operator ==(AnimalController leftHand, AnimalController rightHand){
        // https://stackoverflow.com/questions/25007374/c-sharp-check-if-class-is-null-for-class-with-custom-operator
        if(ReferenceEquals(leftHand, rightHand)) return true;
        if(ReferenceEquals(leftHand, null) || ReferenceEquals(rightHand, null)) return false;
        return leftHand.id == rightHand.id;  
    }
    public static bool operator !=(AnimalController leftHand, AnimalController rightHand){
        if(!ReferenceEquals(leftHand, rightHand)) return true;
        if(ReferenceEquals(leftHand, null) || ReferenceEquals(rightHand, null)) return false;
        return leftHand.id != rightHand.id;  
    }
    #endregion

}

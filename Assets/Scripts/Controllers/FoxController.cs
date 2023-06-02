using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : AnimalController
{
    public float dashDistance = 4;

    [SerializeField]
    private float _dashDuration = 0.5f;

    [SerializeField]
    private float _dashCooldown = 1f;
    private bool _canDash = true;

    //private bool _dashing = false;
    private Vector3 heading;
    private float _dashPartyTeleportDistance = 3f;

    public override void Move(Vector3 vector, float speed){
        base.Move(vector, speed);
        heading = vector;
    }

    public override void Interact()
    {
        DoDash();
    }
    void DoDash(){
        if (heading.magnitude > 0 && !_canDash) return;
        _canDash = false;
        InputController._inputEnabled = false;
        StartCoroutine(
            Dash(heading.normalized*dashDistance, _dashDuration)
        );
    }
    IEnumerator Dash(Vector3 direction, float length)
    {
        float elapsedTime = 0;
        while (elapsedTime < length){
            Move(direction * (Time.deltaTime * length), dashDistance);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        OnDashEnd();
        yield return null;
    }

    private void OnDashEnd  (){
        //party.TeleportPartyMembersToLeader(_dashPartyTeleportDistance);
        InputController._inputEnabled = true;
        _canDash = false;
        party.followSpeed = dashDistance; 
        StartCoroutine(DashCooldown(_dashCooldown));    
    }   

    IEnumerator DashCooldown(float length)
    {
        float elapsedTime = 0;
        while (elapsedTime < length){
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _canDash = true;
        yield return null;
    }
}

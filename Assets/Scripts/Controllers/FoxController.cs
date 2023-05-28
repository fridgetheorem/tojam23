using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : AnimalController
{
    public float dashDistance = 4;
    private Vector3 heading;

    public override void Move(Vector3 vector, float speed){
        base.Move(vector, speed);
        heading = vector;
    }

    public override void Interact()
    {
        DoDash();
    }
    void DoDash(){
        StartCoroutine(
            Dash(heading.normalized*dashDistance, 0.8f)
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
        yield return null;
    }
}

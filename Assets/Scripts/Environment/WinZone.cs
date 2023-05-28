using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{

    public void winGame(){
        Debug.Log("Win");
    }
    //Upon collision with another an animal object, win is triggered
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<AnimalController>() != null)
        {
           winGame();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{

    [SerializeField]
    private float _slowSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Upon collision with another an animal object, this GameObject will be slowed
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Triggered");
        if (other.gameObject.GetComponent<AnimalController>() != null)
        {
            other.gameObject.GetComponent<AnimalController>().speed = _slowSpeed;
        }
    }

    //Upon collision with another an animal object, this GameObject will be slowed
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<AnimalController>() != null)
        {
            other.gameObject.GetComponent<AnimalController>().speed = other.gameObject.GetComponent<AnimalController>().originalSpeed;
        }
    }
}

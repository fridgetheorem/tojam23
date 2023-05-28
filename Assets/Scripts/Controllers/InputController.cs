using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The semimonolithic input manager
// Run it back baby :^)
[RequireComponent(typeof(PartyController))]
public class InputController : MonoBehaviour
{
    PartyController party;
    void Awake(){
        party = GetComponent<PartyController>();
    }
    void Update(){
        Vector2 keyboardInput = new Vector2( 
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        party?.Move(keyboardInput);

        bool input = Input.GetButtonDown("Action");
        if (input){
            party?.Interact();
        }

    }
}
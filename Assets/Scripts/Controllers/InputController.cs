using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The semimonolithic input manager
// Run it back baby :^)
[RequireComponent(typeof(PartyController))]
public class InputController : MonoBehaviour
{
    PartyController party;

    public static bool _inputEnabled = true;

    void Awake(){
        party = GetComponent<PartyController>();
    }
    void Update(){
        // Inverting inputes as a hacky fix for the camera
        if (!_inputEnabled) return;

        Vector2 keyboardInput = new Vector2( 
            -Input.GetAxis("Horizontal"),
            -Input.GetAxis("Vertical")
        );
        
        if (keyboardInput.magnitude > 0) party?.Move(keyboardInput);

        bool input = Input.GetButtonDown("Action");
        if (input){
            party?.Interact();
        }

        bool changeMember = Input.GetButtonDown("Action");
        if (Input.GetButtonDown("Swap"))
        {
            party.CycleLeader();
        }

    }
}

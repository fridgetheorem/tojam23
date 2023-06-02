using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PartySyncZone : MonoBehaviour
{
    List<AnimalController> animalsToSync;
    public void Start(){
        animalsToSync = new List<AnimalController>();
    }

    public void OnTriggerEnter(Collider collider){
        AnimalController controller = collider.GetComponent<AnimalController>();
        if(controller == null) return;
        else if(!PartyController.playerParty.IsInParty(controller)) return; // Animal isn't in party
        else if (animalsToSync.Contains(controller)) return; // Already in sync zone
        //--------------
        animalsToSync.Add(controller);
        //--------------
        if(animalsToSync.Count == PartyController.playerParty.members.Length){
            PartyController.playerParty.behaviour = PartyBehaviour.Follow;
        }
        animalsToSync.Clear();
    }
    public void OnTriggerExit(Collider collider){
        AnimalController controller = collider.GetComponent<AnimalController>();
        if(controller == null) return;
        //----------------
        animalsToSync.Remove(controller);
    }
}

using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PartySyncZone : MonoBehaviour
{
    List<AnimalController> animalsToSync;

    [SerializeField]
    bool disableMovementWhileSyncing = true;

    [SerializeField]
    bool desync = false; // For if the player wants to create a desync zone

    public delegate void OnPartySync();
    public event OnPartySync PartySynced;

    public void Start()
    {
        animalsToSync = new List<AnimalController>();
    }

    public void OnTriggerEnter(Collider collider)
    {
        AnimalController controller = collider.GetComponent<AnimalController>();
        if (controller == null)
            return;
        else if (PartyController.playerParty.behaviour != PartyBehaviour.Independent)
            return; // Animal isn't in party
        else if (!PartyController.playerParty.IsInParty(controller))
            return; // Animal isn't in party
        else if (animalsToSync.Contains(controller))
            return; // Already in sync zone
        //--------------
        if (desync)
        {
            PartyController.playerParty.behaviour = PartyBehaviour.Independent;
            return;
        }
        else
        {
            animalsToSync.Add(controller);
            controller.speed = disableMovementWhileSyncing ? 0 : controller.speed;
        }
        //--------------
        if (animalsToSync.Count == PartyController.playerParty.members.Length)
        {
            CompleteSync();
        }
    }

    //public void OnTriggerExit(Collider collider){
    //    if(desync) return;

    //    AnimalController controller = collider.GetComponent<AnimalController>();
    //    if(controller == null) return;
    //    //----------------
    //    animalsToSync.Remove(controller);
    //}

    public void CompleteSync()
    {
        PartyController.playerParty.behaviour = PartyBehaviour.Follow;

        // we can forego this step if there movement wasn't disabled
        if (disableMovementWhileSyncing)
        {
            animalsToSync.ForEach(
                (animal) =>
                {
                    animal.speed = animal.originalSpeed; // allow movement
                }
            );
        }

        animalsToSync.Clear();

        PartySynced?.Invoke();
    }
}

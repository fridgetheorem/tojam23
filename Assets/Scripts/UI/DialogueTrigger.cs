using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Dialogue Implementation -
Credits to Darren Tran
*/


public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool triggered = false;

    public delegate void OnDialogueFinish();
    public event OnDialogueFinish DialogueFinished;

    void Start()
    {
        if (dialogue.type == DialogueType.Starting)
        {
            TriggerDialogue();
        }

        if (dialogue.type == DialogueType.PartySync)
        {
            PartySyncZone _partySyncZone = FindObjectOfType<PartySyncZone>();
            if (_partySyncZone)
                _partySyncZone.PartySynced += TriggerDialogue;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
        if (dialogue.type == DialogueType.Ending)
        {
            // Transition to credits.
            // FindObjectOfType<PauseMenuUIManager>().StartFinalCutscene();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (other.gameObject.GetComponent<AnimalController>() == null)
        {
            return;
        }

        TriggerDialogue();
        triggered = true;
    }

    public void TriggerDialogueFinish()
    {
        DialogueFinished?.Invoke();
    }

    void Destory()
    {
        if (dialogue.type == DialogueType.PartySync)
        {
            PartySyncZone _partySyncZone = FindObjectOfType<PartySyncZone>();
            if (_partySyncZone)
                _partySyncZone.PartySynced -= TriggerDialogue;
        }
    }
}

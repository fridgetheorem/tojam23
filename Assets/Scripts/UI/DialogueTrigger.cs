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

    void Start()
    {
        if (dialogue.type == DialogueType.Starting)
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
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
}

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
}

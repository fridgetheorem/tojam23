using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueZone : MonoBehaviour
{
    public DialogueTrigger trigger = null;
    private bool triggered = false;

    void OnTriggerEnter()
    {
        if (trigger != null && !triggered)
        {
            trigger.TriggerDialogue();
            triggered = true;
        }
    }
}

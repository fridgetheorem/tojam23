using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum Completion
{
    KeyInput,
    PartySync
}

public class TutorialText : MonoBehaviour
{
    public DialogueTrigger trigger;

    public string tutorialText;

    public Completion completeOn;

    public List<KeyCode> keys;

    private Animator text;

    private bool completed = false;

    private bool writing = true;

    // Start is called before the first frame update
    void Start()
    {
        if (trigger == null)
        {
            Debug.Log("No dialogue trigger associated with this tutorial.");
            return;
        }

        text = GetComponentInChildren<Animator>();

        if (completeOn == Completion.PartySync)
        {
            PartySyncZone _partySyncZone = FindObjectOfType<PartySyncZone>();
            if (_partySyncZone)
                _partySyncZone.PartySynced += CompleteTutorial;
        }

        trigger.DialogueFinished += ShowTutorial;
    }

    // Update is called once per frame
    void Update()
    {
        if (!completed && !writing && completeOn == Completion.KeyInput)
        {
            foreach (KeyCode key in keys)
            {
                if (Input.GetKeyDown(key))
                {
                    CompleteTutorial();
                    completed = true;
                }
            }
        }
    }

    void ShowTutorial()
    {
        text.SetTrigger("Triggered");
        StartCoroutine(TypeTutorial(tutorialText));
    }

    IEnumerator TypeTutorial(string text)
    {
        List<TextMeshPro> textBoxes = new List<TextMeshPro>();
        foreach (TextMeshPro textBox in GetComponentsInChildren<TextMeshPro>())
        {
            textBoxes.Add(textBox);
            textBox.text = "";
        }

        foreach (char letter in text)
        {
            foreach (TextMeshPro textBox in textBoxes)
            {
                textBox.text += letter;
            }

            yield return new WaitForSeconds(0.03f);
        }

        writing = false;
    }

    void CompleteTutorial()
    {
        StartCoroutine(EndTutorial());
    }

    IEnumerator EndTutorial()
    {
        text.SetTrigger("Completed");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    void Destroy()
    {
        PartySyncZone _partySyncZone = FindObjectOfType<PartySyncZone>();
        if (_partySyncZone)
            _partySyncZone.PartySynced -= CompleteTutorial;

        trigger.DialogueFinished -= ShowTutorial;
    }
}

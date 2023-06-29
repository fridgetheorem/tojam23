using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Dialogue Implementation -
Credits to Darren Tran
*/
public enum DialogueType // your custom enumeration
{
    Regular,
    Starting,
    Ending,
    PartySync,
    EnemyClear
};

[System.Serializable]
public class Dialogue
{
    public DialogueType type;

    public List<Sentence> sentences;
}

[System.Serializable]
public class Sentence
{
    public string name;

    [TextArea(3, 18)]
    public string text;
}

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
    Ending
};

[System.Serializable]
public class Dialogue
{
    public DialogueType type;
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}

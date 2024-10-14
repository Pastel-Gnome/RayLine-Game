using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class DialogueSegment
{
    public string speakerName;
    public string dialogueText;
    public Color speakerColor;
    public Dialogue nextBranch; // if dialogue should loop back to a branch without
    public List<Dialogue> choices;
}

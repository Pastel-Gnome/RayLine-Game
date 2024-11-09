using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueSegment
{
	[Header("Displayed Information")]
	public string speakerName;
    [TextArea(minLines: 1, maxLines: 4)]
    public string dialogueText;
    public Sprite speakerIcon;
	public Sprite speakerBkg;

	[Header("Dialogue Actions")]
	public Dialogue nextBranch;
    public int actionIndex = -99;
    public List<Dialogue> choices;
}

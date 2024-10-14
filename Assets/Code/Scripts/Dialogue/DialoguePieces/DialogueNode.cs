using System.Collections.Generic;

[System.Serializable]
public class DialogueNode
{
	public string choiceText;
    public List<DialogueSegment> segments;

	internal bool OnLastSegment(int currSegment)
	{
		return currSegment >= segments.Count - 1 ;
	}
}

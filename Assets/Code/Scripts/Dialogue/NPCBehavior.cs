using UnityEngine;

public class NPCBehavior : DialogueActivator
{
	[SerializeField] protected bool canTalk;
	[SerializeField] protected bool dialogueLoops = true;

	[SerializeField] protected Dialogue dialogue;

	public override InteractData Interact(Interactor interactor)
	{
		InteractData tempData = new InteractData();
		if (isPromptable)
        {
			if (!DialogueManager.inProgress)
			{
				if (canTalk)
				{
					DialogueManager.instance.StartDialogue(dialogue.RootNode);
					canTalk = dialogueLoops;
				}
				else
				{
					DialogueManager.instance.StartDialogue(description.RootNode);
				}
			}
			else
			{
				if (!DialogueManager.instance.Interact()) // if the dialogue has ended and can no longer continue
				{
					tempData.questPieceID = questPieceID;
				}
			}
		}
		return tempData;
	}
}

using UnityEngine;

public class NPCBehavior : DialogueActivator
{
	[SerializeField] protected bool canTalk;
	[SerializeField] protected bool dialogueLoops = true;

	[SerializeField] protected Dialogue dialogue;

	public override void Interact(Interactor interactor)
	{
		if (isPromptable)
        {
			if (!DialogueManager.inProgress)
			{
				if (canTalk)
				{
					DialogueManager.instance.StartDialogue(dialogue.RootNode, dialogueEvents);
					canTalk = dialogueLoops;
				}
				else
				{
					DialogueManager.instance.StartDialogue(description.RootNode, dialogueEvents);
				}
			}
			else
			{
				DialogueManager.instance.Interact();
				if (afterEffect)
				{
					dialogueEvents[afterEffectIndex].Invoke();
					afterEffect = false;
				}
			}
		}
	}
}

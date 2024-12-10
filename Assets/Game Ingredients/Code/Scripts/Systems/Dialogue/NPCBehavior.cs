using UnityEngine;

public class NPCBehavior : DialogueActivator
{
	[SerializeField] protected bool isOnMainDia;
	[SerializeField] protected bool dialogueLoops = true;

	[SerializeField] protected Dialogue dialogue;

	public override void Interact()
	{
		if (isPromptable)
        {
			if (!DialogueManager.inProgress)
			{
				if (isOnMainDia)
				{
					DialogueManager.instance.StartDialogue(dialogue.RootNode, dialogueEvents);
					isOnMainDia = dialogueLoops;
				}
				else
				{
					DialogueManager.instance.StartDialogue(description.RootNode, dialogueEvents);
				}
			}
		}
	}

	public virtual void UpdateDialogue(Dialogue newDialogue)
	{
		dialogue = newDialogue;
	}

	public virtual void SetMainDiaActive(bool newState)
	{
		isOnMainDia = newState;
	}

	public virtual void SetDiaLoop(bool newState)
	{
		dialogueLoops = newState;
	}
}

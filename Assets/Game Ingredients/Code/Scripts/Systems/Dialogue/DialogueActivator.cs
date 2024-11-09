using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueActivator : SaveableObj, IInteractable
{
	[SerializeField] protected List<UnityEvent> dialogueEvents = new List<UnityEngine.Events.UnityEvent>();

	[SerializeField] protected Dialogue description;

	public bool interactable = true;

	public bool isPromptable => interactable;

	public virtual void Interact(Interactor interactor)
	{
		if (isPromptable)
		{
			if (!DialogueManager.inProgress)
			{
				DialogueManager.instance.StartDialogue(description.RootNode, dialogueEvents);
			}
			else
			{
				DialogueManager.instance.Interact();
			}
		}
	}
}
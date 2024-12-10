using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueActivator : SaveableObj, IInteractable
{
	[SerializeField] protected List<UnityEvent> dialogueEvents = new List<UnityEngine.Events.UnityEvent>();

	[SerializeField] protected Dialogue description;

	public bool interactable = true;
	protected bool afterEffect = false;

	public bool isPromptable => interactable;

	public virtual void Interact()
	{
		if (isPromptable)
		{
			if (!DialogueManager.inProgress)
			{
				DialogueManager.instance.StartDialogue(description.RootNode, dialogueEvents);
			}
		}
	}

	public void TriggerEvent(int eventIndex)
	{
		if (dialogueEvents.Count > 0 && eventIndex >= 0)
		{
			dialogueEvents[eventIndex].Invoke();
		}
		else if (dialogueEvents.Count == 0 && eventIndex >= 0)
		{
			Debug.LogError("Cannot Trigger Event");
		}
	}

	public virtual void UpdateDescription(Dialogue newDescription)
	{
		description = newDescription;
	}

	public virtual void SetInteractable(bool newState)
	{
		interactable = newState;
	}
}
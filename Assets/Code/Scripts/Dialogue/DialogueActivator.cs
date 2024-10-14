using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
	[SerializeField] protected string prompt;
	public int questPieceID;

	[SerializeField] protected Dialogue description;

	public string interactionPrompt => prompt;
	public bool isPromptable { get; set; } = true;

	public virtual InteractData Interact(Interactor interactor)
	{
		InteractData tempData = new InteractData();
		if (isPromptable)
		{
			if (!DialogueManager.inProgress)
			{
				DialogueManager.instance.StartDialogue(description.RootNode);
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

public struct InteractData
{
	public int questPieceID;
}
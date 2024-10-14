using UnityEngine;

public class KeyGiver : DialogueActivator
{
	[SerializeField] string givenKey;

	public override InteractData Interact(Interactor interactor)
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
					GameInfo.instance.AddKey(givenKey);
					tempData.questPieceID = questPieceID;
				}
			}
		}
		return tempData;
	}
}

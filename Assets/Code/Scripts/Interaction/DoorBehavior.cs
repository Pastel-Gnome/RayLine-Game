using UnityEngine;

public class DoorBehavior : DialogueActivator
{
	public bool isUnlocked = false;
	[SerializeField] string destinationScene;
	//[SerializeField] string destinationDoor; // add this if doors lead to walkable areas
	[SerializeField] int locationID;
	[SerializeField] string neededKey;
	[SerializeField] int keyQuestPieceID;

	[SerializeField] AudioClip[] doorClips;
	private AudioSource audioSource;


	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public override InteractData Interact(Interactor interactor)
	{
		InteractData tempData = new InteractData();
		if (isUnlocked || GameInfo.instance.CheckKey(neededKey))
		{
			tempData.questPieceID = questPieceID;
			audioSource.PlayOneShot(doorClips[1]);
			GameInfo.instance.SetLocation(locationID, destinationScene); // add destinationDoor as a third argument if this door leads to a walkable area
		} else if (isPromptable) // if door is locked but promptable
		{
			if (!DialogueManager.inProgress)
			{
				audioSource.PlayOneShot(doorClips[0]);
				DialogueManager.instance.StartDialogue(description.RootNode);
			}
			else
			{
				if (!DialogueManager.instance.Interact()) // if the dialogue has ended and can no longer continue
				{
					GameInfo.instance.AddQuestPiece(keyQuestPieceID);
				}
			}
		}
		return tempData;
	}
}

using UnityEngine;

public class DoorBehavior : DialogueActivator
{
	[Header("Destination Info")]
	[SerializeField] string destinationSceneName;
	//[SerializeField] string destinationDoor; // add this if doors lead to walkable areas
	[SerializeField] GameInfo.partOfTown locationID;

	[Header("Locked Status")]
	public bool isUnlocked = false;
	[SerializeField] int itemToUnlock;
	[SerializeField] int doorOpenIndex;

	[Header("Sound")]
	[SerializeField] AudioClip[] doorClips;
	private AudioSource audioSource;


	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public override void Interact(Interactor interactor)
	{
		if (isUnlocked || InventoryTracker.instance.CheckItem(itemToUnlock))
		{
			audioSource.PlayOneShot(doorClips[1]);
			dialogueEvents[doorOpenIndex].Invoke();
			GameInfo.instance.SetLocation(locationID, destinationSceneName); // add destinationDoor as a third argument if this door leads to a walkable area
		} else if (isPromptable) // if door is locked but promptable
		{
			if (!DialogueManager.inProgress)
			{
				audioSource.PlayOneShot(doorClips[0]);
				DialogueManager.instance.StartDialogue(description.RootNode, dialogueEvents);
			}
			else
			{
				DialogueManager.instance.Interact();
			}
		}
	}
}

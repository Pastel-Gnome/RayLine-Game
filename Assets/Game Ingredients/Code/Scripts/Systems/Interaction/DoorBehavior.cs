using UnityEngine;

public class DoorBehavior : DialogueActivator
{
	[Header("Destination Info")]
	[SerializeField] string destinationSceneName;
	//[SerializeField] string destinationDoor; // add this if doors lead to walkable areas
	[SerializeField] GameInfo.partOfTown locationID;

	[Header("Locked Status")]
	public bool isUnlocked = false;
	[SerializeField] int itemToUnlock = -1;
	[SerializeField] int doorOpenIndex;

	[Header("Sound")]
	[SerializeField] AudioClip[] doorClips;
	private AudioSource audioSource;


	protected override void Start()
	{
		uniqueId = UniqueID.CreateID(destinationSceneName, transform);
		audioSource = GetComponent<AudioSource>();
	}

	public override void Interact()
	{
		if (isUnlocked || (itemToUnlock >= 0 && InventoryTracker.instance.CheckItem(itemToUnlock)))
		{
			audioSource.PlayOneShot(doorClips[1]);
			if (doorOpenIndex >= 0) dialogueEvents[doorOpenIndex].Invoke();
			GameInfo.instance.SetLocation(locationID, destinationSceneName); // add destinationDoor as a third argument if this door leads to a walkable area
		} else if (isPromptable) // if door is locked but promptable
		{
			if (!DialogueManager.inProgress)
			{
				audioSource.PlayOneShot(doorClips[0]);
				DialogueManager.instance.StartDialogue(description.RootNode, dialogueEvents);
			}
		}
	}

	public virtual void SetLockedState(bool newLockedState)
	{
		isUnlocked = !newLockedState;
	}

	public virtual void SetItemReq(int newItemReq)
	{
		itemToUnlock = newItemReq;
	}
}

using UnityEngine;

public class InsideManager : MonoBehaviour
{
	[SerializeField] string destinationScene;
	[SerializeField] string spawnLocation;
	[SerializeField] GameInfo.partOfTown locationID;

	private void Start()
	{
		transform.GetComponent<DialogueActivator>().Interact();
	}

	public void ExitToOutside()
	{
		CallAfterDelay.Create(0.4f, () =>
		{
			GameInfo.instance.SetLocation(locationID, destinationScene, spawnLocation);
		});
	}
}

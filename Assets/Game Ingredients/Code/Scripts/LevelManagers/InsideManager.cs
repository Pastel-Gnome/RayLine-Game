using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InsideManager : MonoBehaviour
{
	[SerializeField] string destinationScene;
	[SerializeField] string spawnLocation;
	[SerializeField] GameInfo.partOfTown locationID;

	private void Start()
	{
		transform.GetComponent<DialogueActivator>().Interact(null);
	}

	public void ExitToOutside()
	{
		CallAfterDelay.Create(0.4f, () =>
		{
			GameInfo.instance.SetLocation(locationID, destinationScene, spawnLocation);
		});
	}

	public void EndPlaytest()
	{
		CallAfterDelay.Create(0.4f, () =>
		{
			GameInfo.instance.SetLocation(GameInfo.partOfTown.Inside, "PlaytestEnd");
		});
	}
}

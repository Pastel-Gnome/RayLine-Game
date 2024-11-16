using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
	[Header("Destination Info")]
	[SerializeField] string destinationSceneName;
	[SerializeField] GameInfo.partOfTown locationID;

	public void StartGame()
    {
		GameInfo.instance.SetLocation(locationID, destinationSceneName);
    }
}

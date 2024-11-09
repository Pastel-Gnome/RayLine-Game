using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePortal : MonoBehaviour
{
	[SerializeField] string destinationSceneName;
	[SerializeField] string spawnLocationName;
	[SerializeField] GameInfo.partOfTown locationID;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			GameInfo.instance.SetLocation(locationID, destinationSceneName, spawnLocationName);
		}
	}
}

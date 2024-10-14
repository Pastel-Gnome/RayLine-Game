using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePortal : MonoBehaviour
{
	[SerializeField] string destination;
	[SerializeField] string spawnLocation;
	[SerializeField] int locationID;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			GameInfo.instance.SetLocation(locationID, destination, spawnLocation);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapUpdater : MonoBehaviour
{
    [SerializeField] Transform mapContents;
	Transform playerIcon;
	Transform mapLocationParent;

	private void Start()
	{
		if (transform.root.GetComponent<GameInfo>() == GameInfo.instance)
		{
			playerIcon = mapContents.GetChild(0).GetChild(0);
			mapLocationParent = mapContents.GetChild(0).GetChild(1);

			UpdateMapLocation();
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		UpdateMapLocation();
	}

	private void UpdateMapLocation()
	{
		GameInfo.partOfTown gameLocation = GameInfo.instance.CheckLocation();
		int locationIndex = (int)mapLocation.StartingBeach;

		if (gameLocation == GameInfo.partOfTown.Beach)
		{
			// if the scene is a beach but not the starting area, it is the fishing shack area
			// otherwise default to the starting area
			if (!SceneManager.GetActiveScene().name.Equals("BeachKeyArea"))
			{
				locationIndex = (int)mapLocation.FishingShack;
			}
		}
		else if (gameLocation == GameInfo.partOfTown.Town)
		{
			// if the scene is a part of town that contains the general store, it is the general store area
			// otherwise default to the town hall
			if (SceneManager.GetActiveScene().name.Equals("TownArea"))
			{
				locationIndex = (int)mapLocation.GeneralStore;
			} else
			{
				locationIndex = (int)mapLocation.TownHall;
			}
		}
		else if (gameLocation == GameInfo.partOfTown.Forest)
		{
			// currently the only forest area is the player's house
			locationIndex = (int)mapLocation.PlayerHouse;
		}

		playerIcon.position = mapLocationParent.GetChild(locationIndex).position;
	}

	public enum mapLocation
	{
		StartingBeach = 0,
		FishingShack = 1,
		GeneralStore = 2,
		TownHall = 3,
		PlayerHouse = 4
	}

}

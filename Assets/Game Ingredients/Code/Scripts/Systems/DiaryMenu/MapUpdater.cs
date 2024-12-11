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

	public void UpdateMapLocation()
	{
		GameInfo.partOfTown gameLocation = GameInfo.instance.CheckLocation();
		int locationIndex = (int)mapLocation.StartingBeach;

		if (SceneManager.GetActiveScene().name.Equals("TownScene"))
		{
			// currently the only forest area is the player's house
			locationIndex = (int)mapLocation.PlayerHouse;
		} else if (gameLocation == GameInfo.partOfTown.Beach)
		{
			// if the scene is a beach but not the starting area, it is the fishing shack area
			// otherwise default to the starting area
			if (SceneManager.GetActiveScene().name.Equals("BeachScene"))
			{
				locationIndex = (int)mapLocation.FishingShack;
			}
		} else if (SceneManager.GetActiveScene().name.Equals("TownMain"))
		{
			locationIndex = (int)mapLocation.TownSide;
		}

		if (playerIcon != null && mapLocationParent != null)
		{
			playerIcon.position = mapLocationParent.GetChild(locationIndex).position;
		}else
		{
			playerIcon = mapContents.GetChild(0).GetChild(0);
			mapLocationParent = mapContents.GetChild(0).GetChild(1);
			playerIcon.position = mapLocationParent.GetChild(locationIndex).position;
		}
	}

	public enum mapLocation
	{
		StartingBeach = 0,
		FishingShack = 1,
		GeneralStore = 2,
		TownHall = 3,
		TownSide = 4,
		PlayerHouse = 5
	}

}

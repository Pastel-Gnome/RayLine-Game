using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInfo : MonoBehaviour
{
	public static GameInfo instance { get; private set; }
	private DayNightCycle dayNight;

	[SerializeField] timeOfDay gameTime = timeOfDay.Morning;
	[SerializeField] partOfTown gameLocation = partOfTown.Beach;
	private string spawnObjName = "";

	

	public static PlayerData currPlayer;
	private Transform playerTransform;
	[SerializeField] GameObject exploreUI;

	private void Start()
	{
		// singleton pattern changed to address issue where duplicates were not deleted properly and code was running twice
		if (instance != this && instance != null)
		{
			Destroy(gameObject);
		} else
		{
			if (instance == null)
			{
				instance = this;
			}

			DontDestroyOnLoad(gameObject);

			CreateNewPlayer();

			SceneManager.sceneLoaded += OnSceneLoaded;
			SceneSetup();
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (instance == this)
		{
			SceneSetup();
			SoundManager.instance.DetermineMusic((int)gameTime, (int)gameLocation);
		}
	}

	private void SceneSetup() 
	{
		dayNight = GetComponentInChildren<DayNightCycle>();

		PlayerMovement tempPM = FindFirstObjectByType<PlayerMovement>();
		if (tempPM != null)	playerTransform = tempPM.transform;
		if (!string.IsNullOrEmpty(spawnObjName) && playerTransform != null) playerTransform.position = GameObject.Find(spawnObjName).transform.GetChild(0).position;

		exploreUI.SetActive(gameLocation != partOfTown.Inside && exploreUI != null);

		TimeSetup();
	}

	private void TimeSetup()
	{
		dayNight.SetTimeVisuals(gameTime);
		SoundManager.instance.DetermineMusic((int)gameTime, (int)gameLocation);
	}

	public void SetLocation(partOfTown desLocation, string destination = null, string spawnLocation = "")
	{
		gameLocation = desLocation;
		if (destination != null)
		{
			spawnObjName = spawnLocation;
			LevelLoader.instance.LoadNextLevel(destination);
		}
	}

	public void RegisterQuestInput(int questID)
	{
		
	}

	public void AddQuestPiece(int questID)
	{
		
	}

	private void CreateNewPlayer()
	{
		currPlayer = new PlayerData();
		currPlayer.playerName = "TestPlayer";
		currPlayer.keys = new List<string>();
	}

	public struct PlayerData
	{
		public string playerName;
		public List<string> keys;
	}

	public enum timeOfDay
	{
		Morning = 0, // 5AM-12PM
		Afternoon = 1, // 12PM-5PM
		Evening = 2, // 5PM-9PM
		Night = 3 // 9PM-4AM
	}

	public enum partOfTown
	{
		Beach = 0,
		Town = 1,
		Forest = 2,
		SnowyMountain = 3,
		Inside = 4
	}
}

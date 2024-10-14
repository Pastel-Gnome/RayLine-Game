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

	[SerializeField] int gameTime = (int)timeOfDay.Morning;
	[SerializeField] int gameLocation = (int)partOfTown.Beach;
	private string spawnObjName = "";

	[SerializeField] List<int> quest = new List<int>() { 1, 2 };

	public static PlayerData currPlayer;
	private Transform playerTransform;
	[SerializeField] GameObject exploreUI;
	[SerializeField] Transform questTracker; 

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
			SoundManager.instance.DetermineMusic(gameTime, gameLocation);
		}
	}

	private void SceneSetup() 
	{
		dayNight = GetComponentInChildren<DayNightCycle>();

		PlayerMovement tempPM = FindFirstObjectByType<PlayerMovement>();
		if (tempPM != null)	playerTransform = tempPM.transform;
		if (!string.IsNullOrEmpty(spawnObjName) && playerTransform != null) playerTransform.position = GameObject.Find(spawnObjName).transform.GetChild(0).position;

		exploreUI.SetActive(gameLocation != (int)partOfTown.Inside && exploreUI != null);

		TimeSetup();
	}

	private void TimeSetup()
	{
		dayNight.SetTimeVisuals(gameTime);
		SoundManager.instance.DetermineMusic(gameTime, gameLocation);
	}

	public void SetLocation(int desLocation, string destination = null, string spawnLocation = "")
	{
		gameLocation = desLocation;
		if (destination != null)
		{
			if (!string.IsNullOrEmpty(spawnLocation)) spawnObjName = spawnLocation;
			LevelLoader.instance.LoadNextLevel(destination);
		}
	}

	public void RegisterQuestInput(int questID)
	{
		if (quest.Contains(questID))
		{
			TextMeshProUGUI questStep = questTracker.GetChild(questID-1).GetComponent<TextMeshProUGUI>();
			questStep.text = "<s>" + questStep.text + "</s>";
			quest.Remove(questID);
			if (quest.Count > 0)
			{
				SoundManager.instance.PlayQuestSound(1);
			} else
			{
				SoundManager.instance.PlayQuestSound(2);
			}
		}
	}

	public void AddQuestPiece(int questID)
	{
		SoundManager.instance.PlayQuestSound(0);
		questTracker.GetChild(questID-1).gameObject.SetActive(true);
		quest.Add(questID);
	}

	public bool CheckKey(string desKey)
	{
		return currPlayer.keys.Contains(desKey);
	}

	public void AddKey(string desKey)
	{
		currPlayer.keys.Add(desKey);
	}

	public void LoseKey(string desKey)
	{
		currPlayer.keys.Remove(desKey);
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
		Cliffside = 1,
		Forest = 2,
		SnowyMountain = 3,
		Inside = 4
	}
}

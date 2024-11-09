using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance { get; private set; }
	private AudioSource musicSource;
	private AudioSource environSource;
	private AudioSource charSource;

	private AudioClip currMusicClip = null;
	private int prevTerrain;
	
	[SerializeField] AudioClip[] dayMusicClips;
	[SerializeField] AudioClip[] nightMusicClips;

	[SerializeField] AudioClip[] questClips;
	[SerializeField] AudioClip[] footstepClips;

	private void Start()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		SceneManager.sceneLoaded += OnSceneLoaded;
		SetupSources();
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		SetupSources();
	}

	private void SetupSources()
	{
        if (instance == this)
        {
			musicSource = GetComponent<AudioSource>();
			environSource = GetComponentInChildren<AudioSource>();
			PlayerMovement tempPlayer = FindAnyObjectByType<PlayerMovement>();
			if (tempPlayer != null)
			{
				charSource = tempPlayer.GetComponent<AudioSource>();
			}
		}
	}

	private void SwitchMusic()
	{
		musicSource.Play();
	}

	public void DetermineMusic(int gameTime, int gameLocation)
	{
		AudioClip tempClip;
		if (gameTime < (int)GameInfo.timeOfDay.Evening)
		{
			tempClip = dayMusicClips[gameLocation];
		} else
		{
			tempClip = nightMusicClips[gameLocation];
		}

		if (currMusicClip != tempClip)
		{
			musicSource.clip = tempClip;
			currMusicClip = tempClip;
			SwitchMusic();
		}
	}

	public void PlayQuestSound(int questStage)
	{
		environSource.PlayOneShot(questClips[questStage]);
	}

	public void ToggleWalkSound(bool shouldPlay, int desTerrain = 2)
	{
		if (charSource != null)
		{
			if (shouldPlay)
			{
				if (!charSource.isPlaying) // if no walking audio playing yet
				{
					charSource.clip = footstepClips[desTerrain];
					charSource.Play();
				}
				else if (desTerrain != prevTerrain) // if the player's new terrain is different from the previous terrain
				{
					charSource.clip = footstepClips[desTerrain];
				}
				// if the audio is playing and the terrain hasn't changed, don't change the clip (that would restart the sound)
			}
			else
			{
				charSource.Stop();
			}
		} else
		{
			Debug.LogError("No character Audio Source");
		}
	}
}

public enum TerrainType
{
	sand = 1,
	dirt = 2,
	grass = 3,
	stone = 4,
	snow = 5
}

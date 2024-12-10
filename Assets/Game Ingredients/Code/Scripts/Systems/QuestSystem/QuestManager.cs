using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
	public static QuestManager instance;

	[SerializeField] List<QuestInfoSO> questPool = new List<QuestInfoSO>();
	[SerializeField] List<Quest> activeQuests = new List<Quest>();
	private List<int> questIDList = new List<int>();

	[SerializeField] Transform questTracker;
	[SerializeField] List<QuestDisplay> displays = new List<QuestDisplay>();
	[SerializeField] GameObject questDisplayPrefab;

	public QuestLog questLog;

	private DialogueActivator playerDiaActivator;

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

		if (instance == this)
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
			playerDiaActivator = transform.GetComponent<DialogueActivator>();
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (instance == this && scene.name == "BeachTutorial")
		{
			CallAfterDelay.Create(0.1f, () => { playerDiaActivator.Interact(); });
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}
	}

	public void StartQuest(string combinedValues)
	{
		int questID;
		bool soundPlayed = false;

		if (combinedValues.Contains(","))
		{
			string[] splitIDs = combinedValues.Split(',');
			questID = int.Parse(splitIDs[0]);
			soundPlayed = int.Parse(splitIDs[1]) == 1;
		}
		else
		{
			questID = int.Parse(combinedValues);
		}

		var questInfo = questPool.FirstOrDefault(q => q.questID  == questID);
		if (questInfo != null)
		{
			Quest newQuest = new Quest();
			newQuest.SetUpQuest(questInfo);

			if (!questIDList.Contains(questID))
			{
				if (soundPlayed) SoundManager.instance.PlayQuestSound(0);

				questLog.CreateEntry(questInfo);
				QuestDisplay newDisplay = Instantiate(questDisplayPrefab, questTracker).GetComponentInChildren<QuestDisplay>();
				newDisplay.SetupDisplay(questInfo.questName, questInfo.steps);

				displays.Add(newDisplay);
				activeQuests.Add(newQuest);
				questIDList.Add(questID);
			}
		}
	}


    public void AdvanceQuest(string combinedQuestIDs)
    {
		int questID, stepID;

		if (combinedQuestIDs.Contains(","))
		{
			string[] splitIDs = combinedQuestIDs.Split(',');
			questID = int.Parse(splitIDs[0]);
			stepID = int.Parse(splitIDs[1]);
		}
		else
		{
			questID = int.Parse(combinedQuestIDs);
			stepID = 0;
		}

		int questIndex = activeQuests.FindIndex(q => q.questInfo.questID == questID);

		if (questIndex != -1)
		{
			if (activeQuests[questIndex].CompleteQuestStep(stepID)) // if all steps in the quest have been completed
			{
				FinishQuest(questID, questIndex);
			}
			else // if quest still has steps to complete
			{
				displays[questIndex].CrossOutStep(stepID);
				SoundManager.instance.PlayQuestSound(1);
			}
		}
	}

    public void FinishQuest(int questID, int questIndex)
    {
		questLog.UpdateQuestStatus(questID, true);

		Destroy(displays[questIndex].transform.parent.parent.gameObject);
		displays.RemoveAt(questIndex);

		Dialogue dialogue = activeQuests[questIndex].questInfo.postQuestDialogue;
		if (dialogue != null)
		{
			CallAfterDelay.Create(0.25f, () =>
			{
				playerDiaActivator.UpdateDescription(dialogue);
				playerDiaActivator.Interact();
			});
			
		}

		activeQuests.RemoveAt(questIndex);
		SoundManager.instance.PlayQuestSound(2);
	}
}

[Serializable]
public class Quest
{
	public QuestInfoSO questInfo;
	List<bool> stepCompletion = new List<bool>();

	public void SetUpQuest(QuestInfoSO desQuest)
	{
		questInfo = desQuest;
		foreach (QuestStep step in questInfo.steps)
		{
			stepCompletion.Add(false);
		}
	}

	public bool CompleteQuestStep(int stepID)
	{
		stepCompletion[stepID] = true;
		return !stepCompletion.Contains(false);
	}
}

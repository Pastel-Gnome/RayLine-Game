using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	public static QuestManager instance;

	[SerializeField] List<QuestInfoSO> questPool = new List<QuestInfoSO>();
	[SerializeField] List<Quest> activeQuests = new List<Quest>();
	private List<int> questIDList = new List<int>();

	[SerializeField] Transform questTracker;
	[SerializeField] List<QuestDisplay> displays = new List<QuestDisplay>();
	[SerializeField] GameObject questDisplayPrefab;

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
			StartQuest(100, false);
		}
	}

	public void StartQuest(int questID, bool soundPlayed = true)
	{
		var questInfo = questPool.First(q => q.questID  == questID);
		Quest newQuest = new Quest();
		newQuest.SetUpQuest(questInfo);

		if (!questIDList.Contains(questID))
		{
			if (soundPlayed) SoundManager.instance.PlayQuestSound(0);
			QuestDisplay newDisplay = Instantiate(questDisplayPrefab, questTracker).GetComponentInChildren<QuestDisplay>();
			newDisplay.SetupDisplay(questInfo.questName, questInfo.steps);

			displays.Add(newDisplay);
			activeQuests.Add(newQuest);
			questIDList.Add(questID);
		}
	}


    public void AdvanceQuest(int questID, int stepID)
    {
		int questIndex = activeQuests.FindIndex(q => q.questInfo.questID == questID);

		if (questIndex != -1)
		{
			if (activeQuests[questIndex].CompleteQuestStep(stepID)) // if all steps in the quest have been completed
			{
				FinishQuest(questIndex);
			}
			else // if quest still has steps to complete
			{
				displays[questIndex].CrossOutStep(stepID);
				SoundManager.instance.PlayQuestSound(1);
			}
		}
	}

    public void FinishQuest(int questIndex)
    {
		Destroy(displays[questIndex].transform.parent.gameObject);
		displays.RemoveAt(questIndex);
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

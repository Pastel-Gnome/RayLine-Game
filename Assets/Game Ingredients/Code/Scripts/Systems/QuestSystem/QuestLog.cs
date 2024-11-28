using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
	[Header("UI Objects")]
	[SerializeField] Transform questContainer;
    [SerializeField] TextMeshProUGUI questTitle;
    [SerializeField] TextMeshProUGUI questDescription;
    [SerializeField] Image completionIndicator;
	[SerializeField] Image giverPhoto;
	[SerializeField] TextMeshProUGUI giverName;

	[Header("Sprites & Prefabs")]
	public Sprite[] envelopeMode = new Sprite[4]; // 0 = unread, 1 = in progress, 2 = complete, 3 = failed
	[SerializeField] Sprite[] indicatorMode = new Sprite[3]; // 0 = incomplete, 1 = complete, 2 = failed
    public GameObject logEntryPrefab;

	private List<LogEntry> logEntries = new List<LogEntry>();
	private List<int> entryIndexes = new List<int>();
	private bool newMail = false;

	public void CreateEntry(QuestInfoSO questInfo)
    {
        entryIndexes.Add(questInfo.questID);

        LogEntry newEntry = Instantiate(logEntryPrefab, questContainer).GetComponent<LogEntry>();
        newEntry.InitializeEntry(questInfo);
        logEntries.Add(newEntry);
    }

    public void DisplayEntry(int questID)
    {
        foreach (LogEntry entry in logEntries) { entry.DeselectIcon(); }
        int entryIndex = entryIndexes.IndexOf(questID);

        if (entryIndex >= 0)
        {
            LogEntry selectedEntry = logEntries[entryIndex];
            selectedEntry.SelectIcon();
            questTitle.text = selectedEntry.questName;
            questDescription.text = selectedEntry.questDescription;
            completionIndicator.sprite = indicatorMode[selectedEntry.GetCompletionState()];

            giverPhoto.sprite = selectedEntry.characterPortrait;
            giverName.text = selectedEntry.characterName;
        } else { Debug.LogError("Quest entry not found"); }
    }

    public void UpdateQuestStatus(int questID, bool isComplete)
    {
		LogEntry selectedEntry = logEntries[entryIndexes.IndexOf(questID)];
        if (isComplete)
        {
            selectedEntry.UpdateIcon(2);
        }
	}
}

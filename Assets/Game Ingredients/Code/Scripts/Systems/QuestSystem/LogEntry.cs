using UnityEngine;
using UnityEngine.UI;

public class LogEntry : MonoBehaviour
{
	[Header("Quest Info")]
	public string questName;
    public string questDescription;
    public string characterName;
    public Sprite characterPortrait;
	public int questID;

	Image envelopeIcon;
    bool isRead = false;
    bool isComplete = false;

	public void TriggerDisplay()
    {
        if (!isRead && !isComplete) UpdateIcon(1);
        QuestManager.instance.questLog.DisplayEntry(questID);
    }

	public int GetCompletionState()
	{
		return isComplete ? 1 : 0;
	}

    public void UpdateIcon(int desIcon)
    {
        if (!isComplete)
        {
            envelopeIcon.sprite = QuestManager.instance.questLog.envelopeMode[desIcon];
            isRead = true;

            if (desIcon == 4) isComplete = true;
		}
    }

    public void DeselectIcon()
    {
        envelopeIcon.color = Color.white;
    }

	public void SelectIcon()
	{
		envelopeIcon.color = new Color(0.9882354f, 0.9529412f, 0.8901961f);
	}

	public void InitializeEntry(QuestInfoSO questInfo)
    {
		transform.SetSiblingIndex(0);
		envelopeIcon = GetComponent<Image>();

		questName = questInfo.questName;
		questDescription = questInfo.questDescription;
		characterName = questInfo.questGiver;
		characterPortrait = questInfo.giverIcon;
		questID = questInfo.questID;
	}
}

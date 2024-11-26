using System.Collections.Generic;
using UnityEngine;

public class InteractionPipeline : MonoBehaviour
{
	public void AddItem(int desItem)
    {
        InventoryTracker.instance.AddItem(desItem);
    }


	public void AddQuest(int questID)
    {
        QuestManager.instance.StartQuest(questID);
    }

    public void RegisterQuestInput(string combinedQuestIDs)
    {
        int questID, stepID;

		if (combinedQuestIDs.Contains(","))
        {
			string[] splitIDs = combinedQuestIDs.Split(',');
			questID = int.Parse(splitIDs[0]);
			stepID = int.Parse(splitIDs[1]);
		} else
        {
            questID = int.Parse(combinedQuestIDs);
            stepID = 0;
        }

        QuestManager.instance.AdvanceQuest(questID, stepID);
    }

	public void FixAnomalyDialogue(string anomalyType)
	{
		DialogueNode anomalyNode = Resources.Load<Dialogue>("Dialogue/AnomalyFix_" + anomalyType).RootNode;

		DialogueManager.instance.StartDialogue(anomalyNode, new List<UnityEngine.Events.UnityEvent>());
	}

	public void DeleteObject(GameObject go)
    {
        SaveableObj saveable = go.GetComponent<SaveableObj>();
        if (saveable != null) GameEventsManager.instance.AddDeleteable(saveable.uniqueId);
        if (DialogueManager.inProgress) DialogueManager.instance.EndDialogue();
        Destroy(go);
    }

    public void QuitGame()
    {
        LevelLoader.instance.QuitGame();
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionPipeline : MonoBehaviour
{
	public void AddItem(int desItem)
    {
        InventoryTracker.instance.AddItem(desItem);
    }

	public void RemoveItem(int desItem)
	{
		InventoryTracker.instance.RemoveItem(desItem);
	}


	public void AddQuest(string combinedValues)
    {
		// questID, playsSound?
        QuestManager.instance.StartQuest(combinedValues);
    }

    public void RegisterQuestInput(string combinedQuestIDs)
    {
        QuestManager.instance.AdvanceQuest(combinedQuestIDs);
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
        Destroy(go);
    }

    public void QuitGame()
    {
        LevelLoader.instance.QuitGame();
    }

	public void OnDialogueInteract(InputAction.CallbackContext context)
	{
        if (context.started && DialogueManager.inProgress)
        {
            DialogueManager.instance.Interact();
        }
	}

	public void SetProgressID(string newID)
	{
		DialogueManager.instance.progressUpdater.SetProgressID(newID);
	}

	public void SetProgressDesc(Dialogue newDesc)
	{
		DialogueManager.instance.progressUpdater.SetProgressDesc(newDesc);
	}

	public void SetProgressDia(Dialogue newDia)
	{
		DialogueManager.instance.progressUpdater.SetProgressDia(newDia);
	}

	public void AddProgress()
	{
		DialogueManager.instance.progressUpdater.AddProgress();
	}
}

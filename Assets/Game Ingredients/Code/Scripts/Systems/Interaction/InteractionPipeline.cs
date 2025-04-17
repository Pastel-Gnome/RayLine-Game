using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameInfo;

public class InteractionPipeline : MonoBehaviour
{
	public void AddItem(int desItem)
    {
        InventoryTracker.instance.AddItem(desItem);
    }

	public void AddCluePiece(string cluePiece)
	{
		string[] splitIDs = cluePiece.Split(',');
		int anomalyID = int.Parse(splitIDs[1]);
		char clueLetter = char.Parse(splitIDs[0]);
		HiddenMessageManager.instance.AddClueLetter(anomalyID, clueLetter);
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

	public void FixAnomalyDialogue(Dialogue anomalyDialogue)
	{
		DialogueManager.instance.StartDialogue(anomalyDialogue.RootNode, new List<UnityEngine.Events.UnityEvent>());
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

	public void SetProgressVis(bool newVis)
	{
		DialogueManager.instance.progressUpdater.SetProgressVisibility(newVis);
	}

	public void SetProgressActiveObj(int newState)
	{
		DialogueManager.instance.progressUpdater.SetProgressActiveObj(newState);
	}

	public void SetProgressDoorLock(bool newDoorLock)
	{
		DialogueManager.instance.progressUpdater.SetProgressDoorLock(newDoorLock);
	}

	public void SetProgressDoorKey(int newDoorKey)
	{
		DialogueManager.instance.progressUpdater.SetProgressDoorKey(newDoorKey);
	}

	public void AddProgress()
	{
		DialogueManager.instance.progressUpdater.AddProgress();
	}

	public void EarlyEndDia()
	{
		DialogueManager.instance.EndDialogue();
	}

	public void EndPlaytest()
	{
		CallAfterDelay.Create(0.4f, () =>
		{
			GameInfo.instance.SetLocation(GameInfo.partOfTown.Inside, "PlaytestEnd");
		});
	}

	public void AddCheese(int newCheese)
	{
		GameInfo.instance.AddCheese(newCheese);
	}

	public void SetWeird()
	{
		GameInfo.instance.SetTime(timeOfDay.Night);
	}
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProgressUpdater : MonoBehaviour
{
    List<ProgressData> progressData = new List<ProgressData>();
    string tempID;
    Dialogue tempDia;
	Dialogue tempDesc;
	bool tempVisible = true;
	int tempActivationState = 0; // 0 = unchanged, 1 = active, -1 = inactive

    public void SetUpProgress()
    {
		SaveableObj[] objs = FindObjectsByType<SaveableObj>(FindObjectsInactive.Include, FindObjectsSortMode.None);
		foreach (ProgressData data in progressData)
		{
			var obj = objs.FirstOrDefault(o => o.uniqueId == data.objID);

            if (obj != null)
			{
				Debug.Log(obj.name);
				if (data.activationState == 1)
				{
					obj.gameObject.SetActive(true);
				}

				if (data.startingDesc != null)
				{
					DialogueActivator activator = obj.GetComponent<DialogueActivator>();
					activator.UpdateDescription(data.startingDesc);
					activator.SetInteractable(true);
				}

				NPCBehavior diaActivator = obj.GetComponent<NPCBehavior>();
				if (data.startingDialogue != null)
				{
					diaActivator.UpdateDialogue(data.startingDialogue);
					diaActivator.SetMainDiaActive(true);
					diaActivator.SetInteractable(true);
				}

				SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
				if (sr != null)
				{
					if (data.isVisible)
					{
						if (diaActivator != null) diaActivator.SetInteractable(true);
						sr.enabled = true;
						foreach (SpriteRenderer sprite in obj.GetComponentsInChildren<SpriteRenderer>())
						{
							sprite.enabled = true;
						}
					}
					else
					{
						if (diaActivator != null) diaActivator.SetInteractable(false);
						sr.enabled = false;
						foreach (SpriteRenderer sprite in obj.GetComponentsInChildren<SpriteRenderer>())
						{
							sprite.enabled = false;
						}
					}
				}
			}
		}
    }

    public void SetProgressID(string newID)
    {
        tempID = newID;
    }

	public void SetProgressDesc(Dialogue newDesc)
	{
		tempDesc = newDesc;
	}

	public void SetProgressDia(Dialogue newDia)
	{
		tempDia = newDia;
	}

	public void SetProgressActiveObj(int newState)
	{
		tempActivationState = newState;
	}

	public void SetProgressVisibility (bool newVisible)
	{
		tempVisible = newVisible;
	}

	public void AddProgress()
    {
		ProgressData tempData = new ProgressData();
		tempData.objID = tempID;
		tempData.startingDialogue = tempDia;
		tempData.startingDesc = tempDesc;
		tempData.isVisible = tempVisible;
		tempData.activationState = tempActivationState;
		progressData.Add(tempData);

		tempID = string.Empty;
		tempDia = null;
		tempDesc = null;
		tempVisible = true;
		tempActivationState = 0;
    }

	public struct ProgressData
	{
		public string objID;
		public Dialogue startingDialogue;
		public Dialogue startingDesc;
		public bool isVisible;
		public int activationState;
	}
}

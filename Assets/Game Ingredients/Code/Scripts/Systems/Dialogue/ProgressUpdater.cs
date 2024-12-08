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

    public void SetUpProgress()
    {
		SaveableObj[] objs = FindObjectsOfType<SaveableObj>();
		foreach (ProgressData data in progressData)
		{
			var obj = objs.FirstOrDefault(o => o.uniqueId == data.objID);
			if (obj != null)
			{
				if (data.startingDesc != null)
				{
					DialogueActivator activator = obj.GetComponent<DialogueActivator>();
					activator.UpdateDescription(data.startingDesc);
				}

				if (data.startingDialogue != null)
				{
					NPCBehavior activator = obj.GetComponent<NPCBehavior>();
					activator.UpdateDialogue(data.startingDialogue);
					activator.SetMainDiaActive(true);
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

	public void AddProgress()
    {
		ProgressData tempData = new ProgressData();
		tempData.objID = tempID;
		tempData.startingDialogue = tempDia;
		progressData.Add(tempData);

		tempID = string.Empty;
		tempDia = null;
		tempDesc = null;
    }

	public struct ProgressData
	{
		public string objID;
		public Dialogue startingDialogue;
		public Dialogue startingDesc;
	}
}

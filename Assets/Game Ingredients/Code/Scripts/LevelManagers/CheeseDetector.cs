using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseDetector : MonoBehaviour
{
	[SerializeField] Dialogue desDia;
	[SerializeField] SaveableObj me;

	private void Start()
	{
		if (me.uniqueId == string.Empty) me.IDPlease();
		if (desDia != null && (GameInfo.instance.cheeseList[2] == 1))
		{
			gameObject.GetComponent<DialogueActivator>().UpdateDescription(desDia);
		}
		
		if ((GameInfo.instance.cheeseList[0] == 1) && me.uniqueId.Contains("EnterDay2Trigger"))
		{
			me.deactivateOnStart = false;
		} else if ((GameInfo.instance.cheeseList[1] == 1) && me.uniqueId.Contains("Day2Trigger") && !me.uniqueId.Contains("Enter"))
		{
			me.deactivateOnStart = false;
		} else if ((GameInfo.instance.cheeseList[3] == 1) && me.uniqueId.Contains("EndPlaytestTrigger"))
		{
			me.deactivateOnStart = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventTrigger : MonoBehaviour
{
	[SerializeField] bool physicalTrigger = true;
	DialogueActivator dialogueActivator;
	int halfwayIndex, finalIndex;

	private void Start()
	{
		dialogueActivator = GetComponent<DialogueActivator>();
		LevelLoader.instance.midTransition.AddListener(TriggerMidEvent);
		LevelLoader.instance.endTransition.AddListener(TriggerEndEvent);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (physicalTrigger)
		{
			dialogueActivator.SetInteractable(true);
			dialogueActivator.Interact(null);
		}
	}

	public void TriggerFadeOut(string comboIndexes)
	{
		if (comboIndexes.Contains(","))
		{
			string[] splitIDs = comboIndexes.Split(',');
			halfwayIndex = int.Parse(splitIDs[0]);
			finalIndex = int.Parse(splitIDs[1]);
		}
		else
		{
			halfwayIndex = -1;
			finalIndex = int.Parse(comboIndexes);
		}
		StartCoroutine(LevelLoader.instance.FadeOutTransition());
	}

	private void TriggerMidEvent()
	{
		//wait for half of transition
		if (halfwayIndex >= 0)
		{
			dialogueActivator.TriggerEvent(halfwayIndex);
			halfwayIndex = -1;
		}
	}

	private void TriggerEndEvent()
	{
		//wait for end of transition
		if (finalIndex >= 0)
		{
			dialogueActivator.TriggerEvent(finalIndex);
			finalIndex = -1;
		}
	}
}

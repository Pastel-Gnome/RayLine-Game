using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
	// !! WARNING !! 



	// CHANGING THIS SCRIPT MAY DELETE DIALOGUE ASSETS! PLEASE BACK UP DIALOGUE TREE INFORMATION BEFORE CHANGING THIS SCRIPT!
	// IGNORING THIS WARNING MAY RESULT IN CRYING, SOBBING, AND DELAYS IN DEVELOPMENT!
	// YOU HAVE BEEN WARNED!



	// !! WARNING !!

	
	public static DialogueManager instance { get; private set; }
	public static bool inProgress = false;

	[Header("Text Components")]
	[SerializeField] TextMeshProUGUI nameText;
	[SerializeField] TextMeshProUGUI dialogueText;
	[SerializeField] TextMeshProUGUI descriptionText;

	[Header("Objects & Containers")]
	private GameObject dialogueParent;
	[SerializeField] GameObject dialogueBox;
	[SerializeField] GameObject descriptionBox;
	[SerializeField] GameObject choiceButtonPrefab;
	[SerializeField] Transform choiceButtonContainer;

	private DialogueNode currentNode;

	private int diaIndex = 0;
	private int nodeState = -1; // -1 = none started, 0 = choices available, 1 = node in progress and no choices available, 2 = node finished (and should be clicked to close dialogue)
	private List<UnityEvent> dialogueEvents = new List<UnityEngine.Events.UnityEvent>();

	[Header("Images")]
	[SerializeField] Image characterPortrait;
	[SerializeField] Image characterBkg;
	[SerializeField] Image continueIcon;

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

		SceneManager.sceneLoaded += OnSceneLoaded;
		dialogueParent = transform.GetChild(0).gameObject;
		HideDialogue();
		nodeState = -1;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (instance == this)
		{
			dialogueParent.SetActive(false);
		}
	}

	public void Interact()
	{
		if (nodeState >= 0)
		{
			if (nodeState == 1)
			{
				//Debug.Log("Continue");
				diaIndex++;
				ContinueDialogue();
			}
			else if (nodeState == 2)
			{
				DialogueSegment currentSegment = currentNode.segments[diaIndex];
				if (currentSegment.nextBranch == null)
				{
					EndDialogue();
				} else
				{
					ChangeNode(currentSegment.nextBranch.RootNode);
				}
				// Dialogue branch ended
			}
		}
	}

	public void StartDialogue(DialogueNode node, List<UnityEvent> tempEvents)
	{
		PlayerMovement.DisableMovement();
		currentNode = node;
		if (currentNode.segments.Count > 0)
		{
			inProgress = true;
			nodeState = 0;
			diaIndex = 0;

			dialogueEvents.Clear();
			dialogueEvents = tempEvents;

			ContinueDialogue();
			ShowDialogue();
		}
	}

	public void ContinueDialogue()
	{
		nodeState = 1;
		ShowDialogue();

		DialogueSegment currentSegment = currentNode.segments[diaIndex];

		if (currentSegment.speakerName != "")
		{
			nameText.text = currentSegment.speakerName;
			dialogueText.text = currentSegment.dialogueText;
			characterPortrait.sprite = currentSegment.speakerIcon;
			characterBkg.sprite = currentSegment.speakerBkg;
			continueIcon.enabled = true;

			descriptionBox.SetActive(false);
			dialogueBox.SetActive(true);
		}
		else if (currentSegment.dialogueText != "")
		{
			descriptionText.text = currentSegment.dialogueText;
			continueIcon.enabled = true;

			dialogueBox.SetActive(false);
			descriptionBox.SetActive(true);
		}
        else
        {
			dialogueBox.SetActive(false);
			descriptionBox.SetActive(false);
			continueIcon.enabled = false;
		}

        InvokeDialogueEvents(currentSegment);
		SetUpChoiceButtons(currentSegment);

		if (currentNode.OnLastSegment(diaIndex) && currentSegment.choices.Count == 0)
		{
			//Debug.Log("On Last");
			nodeState = 2;
		}
	}

	public void EndDialogue()
	{
		if (inProgress)
		{
			HideDialogue();

			inProgress = false;
			nodeState = -1;

			PlayerMovement.EnableMovement();
		}
	}

	public void ChangeNode(DialogueNode choice)
	{
		currentNode = choice;
		diaIndex = 0;
		ContinueDialogue();
	}

	public void ShowDialogue()
	{
		dialogueParent.SetActive(true);
	}

	public void HideDialogue()
	{
		dialogueParent.SetActive(false);
	}

	private void InvokeDialogueEvents(DialogueSegment currentSegment)
	{
		if (dialogueEvents.Count > 0 && currentSegment.actionIndex >= 0)
		{
			//Debug.Log("Activating Event " + currentSegment.actionIndex + ", " + dialogueEvents[currentSegment.actionIndex].GetPersistentMethodName(0));
			dialogueEvents[currentSegment.actionIndex].Invoke();
		}
	}

	private void SetUpChoiceButtons(DialogueSegment currentSegment)
	{
		int delay = 0;
		foreach (Transform child in choiceButtonContainer)
		{
			child.GetComponent<Button>().onClick.RemoveAllListeners();
		}

		// if there are more buttons than are needed for a response, delete the extraneous buttons at the end of the responseButtonContainer's child list
		if (currentSegment.choices.Count < choiceButtonContainer.childCount)
		{
			for (int i = 0; i < (choiceButtonContainer.childCount - currentSegment.choices.Count); i++)
			{
				delay++;
				CallAfterFrames.Create(1 + i, () => {
					Destroy(choiceButtonContainer.GetChild(choiceButtonContainer.childCount - 1).gameObject);
				}); // delete the last button in the index
			}
		}

		CallAfterFrames.Create(delay + 1, () =>
		{
			for (int i = 0; i < currentSegment.choices.Count; i++)
			{
				if (i == 0) nodeState = 0;
				DialogueNode choice = currentSegment.choices[i].RootNode;

				// if more buttons are needed than already exist on the responseButtonContainer, instantiate a new button
				if (currentSegment.choices.Count > choiceButtonContainer.childCount && i > choiceButtonContainer.childCount - 1)
				{
					GameObject newButton = Instantiate(choiceButtonPrefab, choiceButtonContainer);
					newButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;

					newButton.GetComponent<Button>().onClick.AddListener(() => ChangeNode(choice));
				}
				else // if the amount of needed and existing buttons are equal OR the current button number to be changed already exists, change an existing button to reflect the new response
				{
					Transform desButton = choiceButtonContainer.GetChild(i);
					desButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;
					desButton.GetComponent<Button>().onClick.AddListener(() => ChangeNode(choice));
				}
			}
		});
	}
}

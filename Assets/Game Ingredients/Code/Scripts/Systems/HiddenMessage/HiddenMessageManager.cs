using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HiddenMessageManager : MonoBehaviour
{
	public static HiddenMessageManager instance;

	private GameObject hiddenMessageParent;
    private GameObject inputWindow;
    private GameObject clueWindow;

	private Transform letterButtonContainer;
	private Transform letterInputContainer;
	private Image submitButtonImage;
	[SerializeField] GameObject inputPrefab;

	[SerializeField] Color notFoundColor = Color.white;
	[SerializeField] Color foundColor = Color.yellow;

	private StringBuilder clueLettersObtained = new StringBuilder();
	private StringBuilder inputtedLetters = new StringBuilder();
	private string correctAnswer;
	private int currInputIndex = -1;

	private Anomaly currAnomaly;

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

		if (instance == this)
		{
			hiddenMessageParent = transform.GetChild(0).gameObject;
			inputWindow = hiddenMessageParent.transform.GetChild(1).gameObject;
			clueWindow = hiddenMessageParent.transform.GetChild(2).gameObject;

			letterButtonContainer = inputWindow.transform.GetChild(0).GetChild(0);
			letterInputContainer = inputWindow.transform.GetChild(0).GetChild(1);
			submitButtonImage = inputWindow.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>();

			CloseClueWindow();
			CloseInputWindow();
		}
	}

	public void SubmitAnswer()
	{
		string playerAnswer = inputtedLetters.ToString();
		correctAnswer = currAnomaly.answer;
		if (playerAnswer.Equals(correctAnswer))
		{
			currAnomaly.FixAnomaly();
			CloseInputWindow();
			ClearClueLetters();
		} else
		{
			IncorrectInput();
		}
	}

	public void UpdateCurrentAnomaly(Anomaly newAnomaly)
	{
		if(currAnomaly != newAnomaly)
		{
			currAnomaly = newAnomaly;
			ClearInputContainer();
			for (int i = 0; i < currAnomaly.answer.Length; i++)
			{
				Instantiate(inputPrefab, letterInputContainer);
			}
		}
		UpdateLetters();
	}



	public void AddClueLetter(char newLetter)
	{
		clueLettersObtained.Append(newLetter);
	}

	private void ClearClueLetters()
	{
		clueLettersObtained.Clear();
		
	}

	private void UpdateLetters()
	{
		foreach (Image buttonImage in letterButtonContainer.GetComponentsInChildren<Image>())
		{
			if(clueLettersObtained.ToString().Contains(buttonImage.name))
			{
				buttonImage.color = foundColor;
			} else
			{
				buttonImage.color = notFoundColor;
			}
		}
	}

	public void InputLetter(string input)
	{
		currInputIndex++;
		if (currInputIndex >= letterInputContainer.childCount)
		{
			ClearInputLetters();
			currInputIndex++;
		}
		letterInputContainer.GetChild(currInputIndex).GetComponentInChildren<TextMeshProUGUI>().text = input;
		inputtedLetters.Append(input);
	}

	private void ClearInputLetters()
	{
		inputtedLetters.Clear();
		currInputIndex = -1;
		foreach (TextMeshProUGUI text in letterInputContainer.GetComponentsInChildren<TextMeshProUGUI>())
		{
			text.text = "";
		}
	}

	private void ClearInputContainer()
	{
		foreach (Transform child in letterInputContainer)
		{
			Destroy(child.gameObject);
		}
	}

	private void IncorrectInput()
	{
		Color normalColor = submitButtonImage.color;
		submitButtonImage.color = Color.red;

		CallAfterDelay.Create(0.5f, () => {
			submitButtonImage.color = normalColor;
		});

		ClearInputLetters();
	}

	public void OpenInputWindow()
	{
		PlayerMovement.DisableMovement();
		hiddenMessageParent.SetActive(true);
		inputWindow.SetActive(true);
	}

	public void OpenClueWindow(string clue)
	{
		PlayerMovement.DisableMovement();
		clueWindow.GetComponentInChildren<TextMeshProUGUI>().text = clue;
		hiddenMessageParent.SetActive(true);
		clueWindow.SetActive(true);
	}

	public void CloseInputWindow()
    {
		PlayerMovement.EnableMovement();
		hiddenMessageParent.SetActive(false);
		inputWindow.SetActive(false);
		ClearInputLetters();
    }

	public void CloseClueWindow()
	{
		PlayerMovement.EnableMovement();
		hiddenMessageParent.SetActive(false);
		clueWindow.SetActive(false);
	}
}

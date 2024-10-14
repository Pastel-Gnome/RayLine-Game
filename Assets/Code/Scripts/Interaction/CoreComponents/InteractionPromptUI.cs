using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI promptText;
	[SerializeField] GameObject promptBackground;

	public bool isShowing = false;

	private void Start()
	{
		HidePrompt();
	}

	public void ShowPrompt(string tempPromptText)
	{
		promptText.text = tempPromptText;
		promptBackground.SetActive(true);
		isShowing = true;
	}

	public void HidePrompt()
	{
		promptBackground.SetActive(false);
		isShowing = false;
	}
}

using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
	[SerializeField] GameObject promptBackground;

	public bool isShowing = false;

	private void Start()
	{
		HidePrompt();
	}

	public void ShowPrompt()
	{
		promptBackground.SetActive(true);
		isShowing = true;
	}

	public void HidePrompt()
	{
		promptBackground.SetActive(false);
		isShowing = false;
	}
}

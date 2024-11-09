using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public static LevelLoader instance { get; private set; }

	public Animator transition;
	//public Animator countdown;
	public string animBranchStart = "ScreenSwipe_StartLevel";
	public float transitionTime = 1f;

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
		FindObjectOfType<PlayerInput>().currentActionMap.FindAction("Quit Game").performed += QuitGame;
		transition.Play(animBranchStart);
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (instance == this)
		{
			PlayerMovement.EnableMovement();
			FindObjectOfType<PlayerInput>().currentActionMap.FindAction("Quit Game").performed += QuitGame;
			transition.Play(animBranchStart);
		}
	}

	public void LoadNextLevel(string desScene)
	{
		StartCoroutine(LoadLevelWithTransition(desScene));
	}

	private IEnumerator LoadLevelWithTransition(string desScene)
	{
		PlayerMovement.DisableMovement();
		transition.SetTrigger("StartPlaying");

		yield return new WaitForSeconds(transitionTime);
		DialogueManager.instance.EndDialogue();
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(desScene);

		yield return new WaitForSeconds(0.75f);
		if (!asyncLoad.isDone)
		{
			transition.SetTrigger("Loading");
		}
	}

	public void QuitGame(InputAction.CallbackContext context)
	{
		Application.Quit();
	}
}

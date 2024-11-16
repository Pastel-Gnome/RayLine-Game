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
		PlayerInput playerInput = FindObjectOfType<PlayerInput>();
		if (playerInput != null){
			playerInput.currentActionMap.FindAction("Quit Game").performed += OnQuitGame;
		}
		transition.Play(animBranchStart);
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (instance == this)
		{
			PlayerMovement.EnableMovement();
			PlayerInput playerInput = FindObjectOfType<PlayerInput>();
			if (playerInput != null)
			{
				playerInput.currentActionMap.FindAction("Quit Game").performed += OnQuitGame;
			}
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

	public void OnQuitGame(InputAction.CallbackContext context)
	{
		QuitGame();
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}

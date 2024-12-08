using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public static LevelLoader instance { get; private set; }

	public Animator transition;
	//public Animator countdown;
	public string animBranchStart = "ScreenSwipe_StartLevel";
	public string animBranchEnd = "ScreenSwipe_EndLevel";
	public string fadeBranchStart = "Crossfade_StartLevel";
	public float transitionTime = 1f;

	public UnityEvent midTransition = new UnityEvent();
	public UnityEvent endTransition = new UnityEvent();

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
		if (transition.GetCurrentAnimatorStateInfo(0).IsName(animBranchStart))
		{
			transition.SetTrigger("StartPlaying");
		} else
		{
			transition.Play(animBranchEnd);
		}
		

		yield return new WaitForSeconds(transitionTime);
		DialogueManager.instance.EndDialogue();
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(desScene);

		yield return new WaitForSeconds(0.75f);
		if (!asyncLoad.isDone)
		{
			transition.SetTrigger("Loading");
		}
	}

	public IEnumerator FadeOutTransition()
	{
		// play fade out transition
		transition.Play(fadeBranchStart);
		yield return new WaitForSeconds(transitionTime);
		midTransition.Invoke();
		transition.SetTrigger("StartPlaying");
		yield return new WaitForSeconds(transitionTime);
		endTransition.Invoke();
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

using System.Collections;
using UnityEngine;
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
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (instance == this)
		{
			PlayerMovement.EnableMovement();
			//transition.Play(animBranchStart);
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

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(desScene);

		yield return new WaitForSeconds(0.75f);
		if (!asyncLoad.isDone)
		{
			transition.SetTrigger("Loading");
		}
	}
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }
	private List<string> deleteables = new List<string>();
	private List<int> fixedAnomalies = new List<int>();

	private string sceneName;
	private SaveableObj[] sceneObjs;
	private List<string> sceneObjIDs = new List<string>();

	/*public InputEvents inputEvents;
	public PlayerEvents playerEvents;
	public InventoryEvents inventoryEvents;
	public MiscEvents miscEvents;*/

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		// initialize all events
		/*inputEvents = new InputEvents();
		playerEvents = new PlayerEvents();
		inventoryEvents = new InventoryEvents();
		miscEvents = new MiscEvents();*/
		SceneManager.sceneLoaded += OnSceneLoaded;
		sceneName = SceneManager.GetActiveScene().name;
		SceneSetup();
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		sceneName = scene.name;
		SceneSetup();
	}

	private void SceneSetup()
	{
		if (instance == this)
		{
			sceneObjs = FindObjectsOfType<SaveableObj>();
			sceneObjIDs.Clear();

			CallAfterFrames.Create(1, () => {
				foreach (SaveableObj obj in sceneObjs)
				{
					sceneObjIDs.Add(obj.uniqueId);
				}

				DestroyCluesAndAnomalies();
				DestroyDeleteables();
			});
		}
	}

	public void DestroyDeleteables()
	{
		foreach(string ID in deleteables)
		{
			if (sceneObjIDs.Contains(ID))
			{
				Destroy(sceneObjs[sceneObjIDs.IndexOf(ID)].gameObject);
			}
		}
	}

	public void DestroyCluesAndAnomalies()
	{
		MessageClue fixedClue;
		Anomaly fixedAnomaly;
		foreach (SaveableObj obj in sceneObjs)
		{
			if (obj != null)
			{
				if ((fixedClue = obj.GetComponent<MessageClue>()) != null)
				{
					if (fixedAnomalies.Contains(fixedClue.anomalyID))
					{
						Destroy(obj.gameObject);
					}
				}
				else if ((fixedAnomaly = obj.GetComponent<Anomaly>()) != null)
				{
					if (fixedAnomalies.Contains(fixedAnomaly.anomalyID))
					{
						Destroy(obj.gameObject);
					}
				}
			}
		}
	}

	public void AddDeleteable(string ID)
	{
		deleteables.Add(ID);
	}

	public void AddFixedAnomalies(int anomalyID)
	{
		fixedAnomalies.Add(anomalyID);
	}
}

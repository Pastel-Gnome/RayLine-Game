using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

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
	}
}

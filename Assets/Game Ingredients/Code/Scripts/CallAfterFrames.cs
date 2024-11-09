using UnityEngine;

public class CallAfterFrames : MonoBehaviour
{
	int delay;
	int age;
	System.Action action;

	public static CallAfterFrames Create(int delay, System.Action action)
	{
		CallAfterFrames caf = new GameObject("CallAfterFrames").AddComponent<CallAfterFrames>();
		caf.delay = delay;
		caf.action = action;
		return caf;
	}

	private void Update()
	{
		if (age > delay)
		{
			action();
			Destroy(gameObject);
		}
	}

	private void LateUpdate()
	{
		age++;
	}
}

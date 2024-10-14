using UnityEngine;

public class CallAfterDelay : MonoBehaviour
{
	float delay;
	float age;
	System.Action action;

	public static CallAfterDelay Create(float delay, System.Action action)
	{
		CallAfterDelay cad = new GameObject("CallAfterDelay").AddComponent<CallAfterDelay>();
		cad.delay = delay;
		cad.action = action;
		return cad;
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
		age += Time.deltaTime;
	}
}

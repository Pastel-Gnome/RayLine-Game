using System.Text;
using UnityEngine;

public class SaveableObj : MonoBehaviour
{
	public string uniqueId;
	public bool deactivateOnStart = false;

	protected virtual void Start()
	{
		IDPlease();
		if (deactivateOnStart && (GameInfo.instance.cheeseList[0] != 1))
		{
			gameObject.SetActive(false);
		} else if (deactivateOnStart)
		{
			CallAfterDelay.Create(0.1f, () =>
			{
				if (deactivateOnStart)
				{
					gameObject.SetActive(false);
				}
			}).transform.SetParent(transform);
		}
	}

	public virtual void IDPlease()
	{
		uniqueId = UniqueID.CreateID(gameObject.name, transform);
	}
}

public static class UniqueID
{
	public static string CreateID(string modifier, Transform desTransform)
	{
		StringBuilder newID = new StringBuilder();
		newID.Append(modifier);
		newID.Append(desTransform.position.x);
		newID.Append(desTransform.position.y);
		return newID.ToString();
	}
}

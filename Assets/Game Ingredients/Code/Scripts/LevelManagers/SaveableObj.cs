using System.Text;
using UnityEngine;

public class SaveableObj : MonoBehaviour
{
	public string uniqueId;
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

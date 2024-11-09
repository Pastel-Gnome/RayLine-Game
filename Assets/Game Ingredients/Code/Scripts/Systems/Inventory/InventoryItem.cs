using UnityEngine;

[CreateAssetMenu(fileName = "New_Item", menuName = "ScriptableObjects/Item", order = 4)]
public class InventoryItem : ScriptableObject
{
	public string itemName;
	public int id;
	[TextArea(minLines: 1, maxLines: 4)]
	public string description;
	public Sprite sprite;
}

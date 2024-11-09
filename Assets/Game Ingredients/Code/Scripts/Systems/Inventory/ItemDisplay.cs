using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
	private Image itemImage;
	private Button slotButton;
	private InventoryItem item = null;

	private void Start()
	{
		slotButton = GetComponent<Button>();
		itemImage = transform.GetChild(0).GetComponent<Image>();
	}

	public void UpdateItem(InventoryItem newItem)
	{
		if (item == null || item != newItem)
		{
			if (newItem != null)
			{
				item = newItem;
				slotButton.interactable = true;
				itemImage.enabled = true;
				itemImage.sprite = item.sprite;
			} else
			{
				item = null;
				slotButton.interactable = false;
				itemImage.enabled = false;
			}
		}
	}

	public void DisplayItemInfo()
	{
		if (item != null)
		{
			Sprite itemSprite = item.sprite;
			string itemDescription = item.description;
			InventoryTracker.instance.DisplayItemInfo(itemSprite, itemDescription);
		}
	}
}

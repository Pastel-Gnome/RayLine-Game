using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryTracker : MonoBehaviour
{
	[Header("Instance & Event")]
	public static InventoryTracker instance;
    public static UnityEvent OnInventoryChange = new UnityEngine.Events.UnityEvent();

	[Header("Item Fields")]
    [SerializeField] InventoryItem[] itemCatalog = new InventoryItem[9];
    [SerializeField] List<int> items = new List<int>();
	[SerializeField] Transform inventorySlotParent;

	[Header("Display Components")]
	[SerializeField] Image displayImage;
	[SerializeField] TextMeshProUGUI displayText;


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

        if (instance == this)
        {
			for (int i = 0; i < itemCatalog.Length; i++)
			{
				itemCatalog[i] = Resources.Load<InventoryItem>("Items/Item" + i);
			}
			OnInventoryChange.AddListener(UpdateInventorySlots);
			HideInventory();
		}
	}

	public void AddItem(int desItem)
    {
		if (!items.Contains(desItem))
		{
			items.Add(desItem);
			OnInventoryChange.Invoke();
		}
    }

    public void RemoveItem(int desItem)
    {
		if (items.Contains(desItem))
		{
			items.Remove(desItem);
			OnInventoryChange.Invoke();
		}
    }

	public bool CheckItem(int desItem)
	{
		return items.Contains(desItem);
	}

	public void UpdateInventorySlots()
    {
        for (int i = 0; i < items.Count; i++)
        {
            ItemDisplay slotItem = inventorySlotParent.GetChild(i).GetComponentInChildren<ItemDisplay>();
            slotItem.UpdateItem(itemCatalog[items[i]]);
        }
    }

	public void DisplayItemInfo(Sprite itemSprite, string itemDescription)
	{
		displayImage.sprite = itemSprite;
		displayImage.enabled = true;
		displayText.text = itemDescription;
	}

	public void ClearItemInfo()
	{
		displayImage.enabled = false;
		displayText.text = string.Empty;
	}


	public void ShowInventory()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void HideInventory()
    {
		transform.GetChild(0).gameObject.SetActive(false);
		ClearItemInfo();
	}
}

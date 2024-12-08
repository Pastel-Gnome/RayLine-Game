using UnityEngine;
using UnityEngine.UI;

public class DiaryController : MonoBehaviour
{
    [SerializeField] Image mapTab;
    [SerializeField] Image questLogTab;

    [SerializeField] Sprite[] tabModes = new Sprite[2]; // 0 = unselected, 1 = selected

	private void Start()
	{
		ShowMap();
		HideDiary();
	}

	public void ShowDiary()
    {
		PlayerMovement.DisableMovement();
		transform.parent.GetChild(0).gameObject.SetActive(true);
		transform.GetChild(0).gameObject.SetActive(true);
	}

	public void HideDiary()
	{
		PlayerMovement.EnableMovement();
		transform.parent.GetChild(0).gameObject.SetActive(false);
		transform.GetChild(0).gameObject.SetActive(false);
	}

	public void ShowMap()
    {
        mapTab.sprite = tabModes[1];
        questLogTab.sprite = tabModes[0];

        mapTab.transform.GetChild(1).gameObject.SetActive(true);
		questLogTab.transform.GetChild(1).gameObject.SetActive(false);

		mapTab.transform.SetSiblingIndex(1);
	}

    public void ShowQuestLog()
    {
		mapTab.sprite = tabModes[0];
		questLogTab.sprite = tabModes[1];

		mapTab.transform.GetChild(1).gameObject.SetActive(false);
		questLogTab.transform.GetChild(1).gameObject.SetActive(true);

		questLogTab.transform.SetSiblingIndex(1);
	}
}

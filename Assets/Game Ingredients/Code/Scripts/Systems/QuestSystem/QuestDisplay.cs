using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class QuestDisplay : MonoBehaviour
{
    private bool stepsShowing = true;
    public void ToggleQuestSteps()
    {
        stepsShowing = !stepsShowing;
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(stepsShowing);
        }
    }

    public void SetupDisplay(string questName, List<QuestStep> questSteps)
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questName;
        TextMeshProUGUI firstStep = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
		firstStep.text = questSteps[0].stepName + " -";
        if (questSteps.Count > 1 )
        {
            for (int i = 1; i < questSteps.Count; i++)
            {
				Instantiate(firstStep.gameObject, transform).GetComponent<TextMeshProUGUI>().text = questSteps[i].stepName + " -";
			}
        }
    }

    public void CrossOutStep(int stepIndex)
    {
        TextMeshProUGUI stepText = transform.GetChild(stepIndex + 1).GetComponent<TextMeshProUGUI>();
        stepText.text = "<s>" + stepText.text + "</s>";
	}
}

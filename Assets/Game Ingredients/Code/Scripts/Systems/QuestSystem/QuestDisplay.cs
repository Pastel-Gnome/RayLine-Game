using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDisplay : MonoBehaviour
{

    public void SetupDisplay(string questName, List<QuestStep> questSteps)
    {
        Transform closedVersion = transform.parent.parent.GetChild(1).GetChild(0);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questName;

		TextMeshProUGUI firstStep = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
		firstStep.text = questSteps[0].stepName + " -";

		if (questSteps.Count > 1)
        {
            for (int i = 1; i < questSteps.Count; i++)
            {
                GameObject newStep = Instantiate(firstStep.gameObject, transform);
                newStep.GetComponent<TextMeshProUGUI>().text = questSteps[i].stepName + " -";

                Instantiate(closedVersion.GetChild(1), closedVersion);
			}
        }
    }

    public void CrossOutStep(int stepIndex)
    {
        TextMeshProUGUI stepText = transform.GetChild(stepIndex + 1).GetComponent<TextMeshProUGUI>();
        stepText.text = "<s>" + stepText.text + "</s>";
	}
}

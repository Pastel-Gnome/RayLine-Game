using UnityEngine;

[CreateAssetMenu(fileName = "QuestStepSO", menuName = "ScriptableObjects/QuestStepSO", order = 2)]
public class QuestStep : ScriptableObject
{
	[Header("Displayed Information")]
	public string stepName;
}

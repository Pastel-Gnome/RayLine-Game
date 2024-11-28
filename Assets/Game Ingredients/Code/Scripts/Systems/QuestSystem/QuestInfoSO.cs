using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
	[Header("Displayed Information")]
	public string questName;
	[TextArea(minLines: 1, maxLines: 4)]
	public string questDescription;
	public string questGiver;
	public Sprite giverIcon;

	[Header("Quest Data")]
	public int questID;
	public List<QuestStep> steps;
}

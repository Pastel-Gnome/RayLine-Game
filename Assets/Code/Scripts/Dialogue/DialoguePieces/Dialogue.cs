using UnityEngine;

[CreateAssetMenu(fileName = "New_Dialogue", menuName = "ScriptableObjects/DialogueAsset", order = 2)]
public class Dialogue : ScriptableObject
{
    // first node in the conversation
    public DialogueNode RootNode;
}

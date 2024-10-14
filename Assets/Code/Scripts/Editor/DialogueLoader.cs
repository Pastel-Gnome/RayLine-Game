using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DialogueLoader : EditorWindow
{
    string jsonName;

    public struct DialogueInfo
    {
        public string choiceText;
        // still in progress
	}

    private void CreateAssetFromJSON()
    {
        Dialogue newDialogueAsset = Dialogue.CreateInstance<Dialogue>();
        DialogueInfo info = JsonUtility.FromJson<DialogueInfo>(LoadResourcesJSON(jsonName));
        newDialogueAsset.RootNode.choiceText = info.choiceText;
    }

	private string LoadResourcesJSON(string path)
	{
		string json = Resources.Load<TextAsset>("JSON/Dialogue/" + path.Replace(".json", "")).text;
        return json;
	}

    [MenuItem("Tools/Dialogue Loader")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DialogueLoader));
    }

	private void OnGUI()
	{
        GUILayout.Label("Load Dialogue", EditorStyles.boldLabel);
        jsonName = EditorGUILayout.TextField("Dialogue JSON File Name", jsonName);

        if(GUILayout.Button("Create Dialogue Asset"))
        {
            CreateAssetFromJSON();
        }
	}
}

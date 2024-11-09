using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DialogueLoader : EditorWindow
{
    string jsonName;
    string newAssetName;
    string newChoiceText;

    public struct SegmentInfo
    {
        
	}

    private void CreateAssetFromJSON()
    {
        Dialogue newDialogueAsset = Dialogue.CreateInstance<Dialogue>();
        newDialogueAsset.name = newAssetName;
        newDialogueAsset.RootNode.choiceText = newChoiceText;
        SegmentInfo info = JsonUtility.FromJson<SegmentInfo>(LoadResourcesJSON(jsonName));
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
        newAssetName = EditorGUILayout.TextField("Name Of New Asset", newAssetName);
        newChoiceText = EditorGUILayout.TextField("Choice Text For New Asset", newChoiceText);

        if(GUILayout.Button("Create Dialogue Asset"))
        {
            CreateAssetFromJSON();
        }
	}
}

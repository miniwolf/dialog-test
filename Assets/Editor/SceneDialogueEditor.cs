using UnityEditor;

//[CustomEditor(typeof(DialogueImporter.SceneDialogue))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DialogueImporter.SceneDialogue myTarget = (DialogueImporter.SceneDialogue)target;

        EditorGUILayout.LabelField("Hello");
    }
}
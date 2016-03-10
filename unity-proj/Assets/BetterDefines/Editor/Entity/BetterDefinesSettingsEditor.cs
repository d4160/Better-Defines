using BetterDefines.Editor.Entity;
using UnityEditor;

[CustomEditor(typeof(BetterDefinesSettings))]
public class BetterDefinesSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Label");
        base.OnInspectorGUI();
    }
}
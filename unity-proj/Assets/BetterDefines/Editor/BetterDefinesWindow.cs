using UnityEditor;

public class BetterDefinesWindow : EditorWindow
{
    [MenuItem("Window/Better Defines")]
    private static void Init()
    {
        var window = (BetterDefinesWindow) GetWindow(typeof (BetterDefinesWindow));
        window.Show();
    }

    void OnGUI()
    {
        for (int i = 0; i < 10; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("Label");
            EditorGUILayout.EndHorizontal();
        }
    }
}
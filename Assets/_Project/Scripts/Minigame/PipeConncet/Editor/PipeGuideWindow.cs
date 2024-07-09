using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
public class PipeGuideWindow : EditorWindow
{
    private string text = "Change row or columns value to change grid\n" +
        "Click left mouse to change value (pipe type)\n" +
        "Click right mouse to change pipe rotate angle\n" +
        "Find object Controller in Scene to Add new level";
    private Texture2D image;

    [MenuItem("Window/Pipe Guide")]
    public static void ShowWindow()
    {
        GetWindow<PipeGuideWindow>("Pipe Guide");
    }

    private void OnEnable()
    {
        string scriptPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
        string scriptDirectory = Path.GetDirectoryName(scriptPath);
        string imagePath = Path.Combine(scriptDirectory, "Texture/Guide.png");

        image = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
    }

    private void OnGUI()
    {
        GUILayout.Label("Contact Huy Dev for more", EditorStyles.boldLabel);
        GUILayout.Label(text);

        if (image != null)
        {
            GUILayout.Label(image);
        }
        else
        {
            GUILayout.Label("Cannot found guide image");
        }
    }
}

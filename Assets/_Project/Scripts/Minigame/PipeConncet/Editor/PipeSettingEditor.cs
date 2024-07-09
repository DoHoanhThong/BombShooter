using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PipeLevelSetting))]
public class PipeSettingEditor : Editor
{
    private PipeLevelSetting pipeLevelSetting;
    private GUIStyle cellStyle;

    public override void OnInspectorGUI()
    {
        if (cellStyle == null)
        {
            cellStyle = new GUIStyle(GUI.skin.button);
            cellStyle.margin = new RectOffset(0, 0, 0, 0);
        }

        pipeLevelSetting = (PipeLevelSetting)target;

        EditorGUI.BeginChangeCheck();

        if (GUILayout.Button("Open Guide"))
        {
            PipeGuideWindow.ShowWindow();
        }

        pipeLevelSetting.rows = EditorGUILayout.IntField("Rows", pipeLevelSetting.rows);
        pipeLevelSetting.columns = EditorGUILayout.IntField("Columns", pipeLevelSetting.columns);

        if (EditorGUI.EndChangeCheck())
        {
            UpdateCellColorsArray();
            EditorUtility.SetDirty(pipeLevelSetting);
            
        }

        EditorGUILayout.Space();
   
        DrawGrid();
    }

    private void UpdateCellColorsArray()
    {
        int newSize = pipeLevelSetting.rows * pipeLevelSetting.columns;
        //System.Array.Resize(ref pipeLevelSetting.cellColors, newSize);
        System.Array.Resize(ref pipeLevelSetting.cellNumbers, newSize);
        System.Array.Resize(ref pipeLevelSetting.cellRotations, newSize);

        for (int i = 0; i < newSize; i++)
        {
            //if (pipeLevelSetting.cellColors[i] == default)
            //{
            //    pipeLevelSetting.cellColors[i] = Color.white;
            //}
            if (pipeLevelSetting.cellNumbers[i] == 0)
            {
                pipeLevelSetting.cellNumbers[i] = 0;
            }
            if (pipeLevelSetting.cellRotations[i] == 0)
            {
                pipeLevelSetting.cellRotations[i] = 0;
            }
        }
    }

    private void DrawGrid()
    {
        Event currentEvent = Event.current;

        for (int i = 0; i < pipeLevelSetting.rows; i++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int j = 0; j < pipeLevelSetting.columns; j++)
            {
                int index = i * pipeLevelSetting.columns + j;
                Color originalColor = GUI.backgroundColor;
                //GUI.backgroundColor = pipeLevelSetting.cellColors[index];
                GUI.backgroundColor = Color.white;

                Rect buttonRect = GUILayoutUtility.GetRect(40, 40, cellStyle);
                string numberText = pipeLevelSetting.cellNumbers[index] == 0 ? "" : $"Value: {pipeLevelSetting.cellNumbers[index]}";
                string rotationText = pipeLevelSetting.cellRotations[index].ToString();
                string buttonText = pipeLevelSetting.cellNumbers[index] == 0 ? "" : $"{numberText}\nRotation: {rotationText}°";

                if (buttonRect.Contains(currentEvent.mousePosition))
                {
                    if (currentEvent.type == EventType.MouseDown)
                    {
                        if (currentEvent.button == 1) // Left mouse button
                        {
                            pipeLevelSetting.cellRotations[index] = (pipeLevelSetting.cellRotations[index] + 90) % 360;
                        }
                        else if (currentEvent.button == 0) // Right mouse button
                        {
                            pipeLevelSetting.cellNumbers[index] = (pipeLevelSetting.cellNumbers[index] + 1) % 5;
                        }
                        currentEvent.Use();
                        EditorUtility.SetDirty(pipeLevelSetting);
                    }
                }

                GUI.Button(buttonRect, buttonText, cellStyle);

                GUI.backgroundColor = originalColor;
            }

            EditorGUILayout.EndHorizontal();
        }
    }


    //private Color PickColor(Color currentColor)
    //{
    //    int index = System.Array.IndexOf(colors, currentColor);
    //    if (index == -1 || index == colors.Length - 1)
    //    {
    //        index = 0;
    //    }
    //    else
    //    {
    //        index++;
    //    }
    //    return colors[index];
    //}

}

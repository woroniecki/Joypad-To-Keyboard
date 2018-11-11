using UnityEngine;
using System.Collections;
using UnityEditor;
using UI;
using System;

[CustomEditor(typeof(UIController))]
public class LevelScriptEditor : Editor
{
    UIController myTarget;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        myTarget = (UIController)target;

        EditorGUILayout.Space();

        Button.Type[] buttonTypes = (Button.Type[])Enum.GetValues(typeof(Button.Type));
        DualAxis.Type[] dualAxisTypes = (DualAxis.Type[])Enum.GetValues(typeof(DualAxis.Type));

        if(myTarget.buttons == null || myTarget.buttons.Length != buttonTypes.Length + dualAxisTypes.Length)
        {
            myTarget.buttons = new UnityEngine.UI.Button[buttonTypes.Length + dualAxisTypes.Length];
        }

        for (int i = 0; i < buttonTypes.Length; i++)
        {
            DrawButtonRow(buttonTypes[i].ToString(), i);
        }


        for (int i = 0; i < dualAxisTypes.Length; i++)
        {
            DrawButtonRow(dualAxisTypes[i].ToString(), buttonTypes.Length + i);
        }
    }

    private void DrawButtonRow(string name, int i)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(name, GUILayout.Width(120));
        myTarget.buttons[i] = (UnityEngine.UI.Button)EditorGUILayout.ObjectField(myTarget.buttons[i], typeof(UnityEngine.UI.Button), true);
        EditorGUILayout.EndHorizontal();
    }
}
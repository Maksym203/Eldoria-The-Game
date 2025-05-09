﻿using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ModulesShaker))]
public class customButton : Editor
{
    public override void OnInspectorGUI()
    {
        ModulesShaker myScript = (ModulesShaker)target;

        GUILayout.Label("Choose gender:");

        myScript.gender_idx = EditorGUILayout.Popup(myScript.gender_idx, myScript.Gender);       

        if (GUILayout.Button("Set all elements by numeration"))
        {
            myScript.SetAll(myScript.set_numeration);
        }

        if (GUILayout.Button("Randomize All (one package)"))
        {
            myScript.RandomizeAll();
        }

        if (GUILayout.Button("Randomize All (including other packages)"))
        {
            myScript.RandomizeAllOther();
        }

        DrawDefaultInspector();
    }
}
#endif

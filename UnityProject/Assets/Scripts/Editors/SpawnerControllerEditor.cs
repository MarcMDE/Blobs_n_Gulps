using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(SpawnerController))]
public class NavigatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SpawnerController myScript = (SpawnerController)target;
        if (GUILayout.Button("Test mult spawn"))
        {
            myScript.TestSpawnInitial();
        }
        /*
        if (GUILayout.Button("Clear"))
        {
            myScript.Clear();
        }
        */
    }
}

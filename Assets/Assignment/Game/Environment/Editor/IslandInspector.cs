using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Island))]
public class IslandInspector : Editor {

    public override void OnInspectorGUI()
    {
        Island island = target as Island;

        base.OnInspectorGUI();

        island.Radius = EditorGUILayout.Slider("Radius", island.Radius, 0, 50);
    }
}

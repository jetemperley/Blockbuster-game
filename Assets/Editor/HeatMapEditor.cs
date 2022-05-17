using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HeatMap))]
public class HeatMapEditor : Editor
{
    public override void OnInspectorGUI () {

        HeatMap component = (HeatMap)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Build heatmap")){
            component.buildHeatmap();
        }
    }

    
    
}

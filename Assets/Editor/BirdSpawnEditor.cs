using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BirdSpawn))]
public class BirdSpawnEditor : Editor
{
    private bool yPadFold = false;
    
    public override void OnInspectorGUI()
    {
        BirdSpawn bird = (BirdSpawn)target;
        DrawDefaultInspector();
        yPadFold = EditorGUILayout.Foldout(yPadFold, "Y-Padding Configuration");
        if (yPadFold)
        {
            EditorGUI.indentLevel++;
            //[Tooltip("Padding from the top and bottom of the spawn area.")]
            bird.yPadding = EditorGUILayout.FloatField("Y-Padding", bird.yPadding);
            bird.forcedYOffset = EditorGUILayout.FloatField("Forced Y-Offset", bird.forcedYOffset);
            bird.minYDistance = EditorGUILayout.FloatField("Min Y-Distance", bird.minYDistance);
        }
    }
}

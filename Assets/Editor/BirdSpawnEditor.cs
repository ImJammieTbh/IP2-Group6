using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BirdSpawn))]
public class BirdSpawnEditor : Editor
{
    private bool yPadFold;
    
    SerializedProperty _yPaddingProp;
    SerializedProperty _forcedYOffsetProp;
    SerializedProperty _minYDistanceProp;

    private void OnEnable()
    {
        _yPaddingProp = serializedObject.FindProperty("yPadding");
        _forcedYOffsetProp = serializedObject.FindProperty("forcedYOffset");
        _minYDistanceProp = serializedObject.FindProperty("minYDistance");
    }
    
    public override void OnInspectorGUI()
    {
        BirdSpawn bird = (BirdSpawn)target;
        
        serializedObject.Update();
        
        DrawDefaultInspector();
        EditorGUILayout.Space();
        yPadFold = EditorGUILayout.Foldout(yPadFold, "Y-Padding Configuration");
        if (yPadFold)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_yPaddingProp);
            EditorGUILayout.PropertyField(_forcedYOffsetProp);
            EditorGUILayout.PropertyField(_minYDistanceProp);
        }
        serializedObject.ApplyModifiedProperties();
    }
}

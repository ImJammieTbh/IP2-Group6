using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(BoundsBox))]
public class BoundsBoxEditor : Editor
{
    private BoxBoundsHandle _boundsHandle = new BoxBoundsHandle();

    private bool BoxFoldout = true;

    private void OnSceneGUI()
    {
        BoundsBox box = (BoundsBox)target;

        _boundsHandle.center = Vector3.zero;
        _boundsHandle.size = box.size;

        Matrix4x4 matrix = Matrix4x4.TRS(
            box.transform.position,
            box.transform.rotation,
            Vector3.one
        );

        using (new Handles.DrawingScope(matrix))
        {
            EditorGUI.BeginChangeCheck();
            _boundsHandle.DrawHandle();
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(box, "Resize Bounds Box");
                box.size = _boundsHandle.size;
                EditorUtility.SetDirty(box);
            }
        }
    }
}
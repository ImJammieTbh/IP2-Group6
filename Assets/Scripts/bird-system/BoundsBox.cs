using UnityEngine;

[ExecuteAlways]
public class BoundsBox : MonoBehaviour
{
    public Vector3 size = Vector3.one;
    public Color gizmoColor = new Color(0f, 1f, 0f, 0.8f);

    [Tooltip("Only draw the gizmo when the object is selected")]
    public bool showOnlyWhenSelected = true;
    
    //Helper features for grabbing the info for the bounding box. ( for spawn points or restrictions )
    /// <summary>
    /// Returns the centre of the <b>World Left</b> face of the bounding box.
    /// </summary>
    public Vector3 WorldLeft   => transform.TransformPoint(Vector3.left    * size.x * 0.5f);
    
    /// <summary>
    /// Returns the centre of the <b>World Right</b> face of the bounding box.
    /// </summary>
    public Vector3 WorldRight  => transform.TransformPoint(Vector3.right   * size.x * 0.5f);

    /// <summary>
    /// Returns the centre of the <b>World Bottom</b> face of the bounding box.
    /// </summary>
    public Vector3 WorldBottom => transform.TransformPoint(Vector3.down    * size.y * 0.5f);
    
    /// <summary>
    /// Returns the centre of the <b>World Top</b> face of the bounding box.
    /// </summary>
    public Vector3 WorldTop    => transform.TransformPoint(Vector3.up      * size.y * 0.5f);

    /// <summary>
    /// Returns the centre of the <b>World Back</b> face of the bounding box.
    /// </summary>
    public Vector3 WorldBack   => transform.TransformPoint(Vector3.back    * size.z * 0.5f);
    
    /// <summary>
    /// Returns the centre of the <b>World Front</b> face of the bounding box.
    /// </summary>
    public Vector3 WorldFront  => transform.TransformPoint(Vector3.forward * size.z * 0.5f);

    //Gizmo drawing for the area.
    private void DrawGizmo()
    {
        Gizmos.color = gizmoColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, size);
    }

    private void OnDrawGizmos()
    {
        if (!showOnlyWhenSelected)
            DrawGizmo();
    }

    private void OnDrawGizmosSelected()
    {
        if (showOnlyWhenSelected)
            DrawGizmo();
    }
}


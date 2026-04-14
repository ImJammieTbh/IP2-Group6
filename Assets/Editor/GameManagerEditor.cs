using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private bool photosFoldout;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameManager gameManager = (GameManager)target;

        if (gameManager.birdPhotos == null)
        {
            return;
        }
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Photos [Debug View]", EditorStyles.boldLabel);

        photosFoldout = EditorGUILayout.Foldout(photosFoldout, "Photos");
        if (photosFoldout)
        {
            foreach (var kvp in gameManager.birdPhotos)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField($"ID: {kvp.Key}");

                if (kvp.Value != null)
                {
                    EditorGUILayout.LabelField($"Photo Count: {kvp.Value.Count}");

                    foreach (var photo in kvp.Value)
                    {
                        EditorGUILayout.BeginVertical("box");

                        if (photo != null)
                        {
                            EditorGUILayout.LabelField("Photo Data");

                            EditorGUILayout.ObjectField(photo.Texture, typeof(Texture2D), false);
                            EditorGUILayout.ObjectField(photo.PolaroidSprite, typeof(Sprite), false);
                            EditorGUILayout.LabelField($"Photo Score: {photo.photoScore}");
                        }
                        else
                        {
                            EditorGUILayout.LabelField("Null Photo");
                        }

                        EditorGUILayout.EndVertical();
                    }
                }
                EditorGUILayout.EndVertical();
            }
        }
    }
}

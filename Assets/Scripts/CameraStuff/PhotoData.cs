using System.IO;
using UnityEngine;

[System.Serializable]
public class PhotoData
{
    public Texture2D Texture;
    public Sprite PolaroidSprite; // spongbort image,
    public System.DateTime Timestamp;
    public float photoScore;

    public PhotoData(Texture2D Photo)
    {
        Texture = Photo;
        Timestamp = System.DateTime.Now; // I'll expand upon this stuff if we want to use this kinda section to save data in actual files on the disk.

        string folder = Application.persistentDataPath + "/Photos/";
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        byte[] bytes = Photo.EncodeToPNG();

        string fileName = Timestamp.ToString("ddMMyyyy_HHmmss") + ".png";
        string fullPath = Path.Combine(folder, fileName);

        File.WriteAllBytes(fullPath, bytes);

        Debug.Log(Application.persistentDataPath);
    }
}

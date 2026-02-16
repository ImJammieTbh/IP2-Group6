using UnityEngine;

[System.Serializable]
public class PhotoData
{
    public Texture2D Texture;
    public Sprite PolaroidSprite; // spongbort image
    public System.DateTime Timestamp;

    public PhotoData(Texture2D Photo)
    {
        Texture = Photo;
        Timestamp = System.DateTime.Now; // I'll expand upon this stuff if we want to use this kinda section to save data in actual files on the disk.
    }
}

using UnityEngine;

public class PhotoManager : MonoBehaviour
{
    public static PhotoManager Instance;

    public PhotoData LatestPhoto;

    void Awake()
    {
        Instance = this; // omg no way it's the instance?? the instance is this????? this thing right here is the instance???
    }

    public void SetLatestPhoto(Texture2D texture, Sprite polaroid) //this should use the polaroid sprite
    {
        LatestPhoto = new PhotoData(texture);
        LatestPhoto.PolaroidSprite = polaroid;
    }
}

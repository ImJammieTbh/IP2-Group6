using UnityEngine;

public class PolaroidRenderer : MonoBehaviour
{
    public Texture2D polaroidFrame;
    public Vector2Int photoAreaOffset;
    public Vector2Int photoAreaSize;


    public Sprite CreatePolaroid(Texture2D photo)
    {
        Texture2D final = new Texture2D(polaroidFrame.width, polaroidFrame.height, TextureFormat.RGBA32, false);

        Graphics.CopyTexture(polaroidFrame, final); // final is the final product, this is taking the polaroid frame and final product image so they're ready to work with

        Color[] photoPixels = photo.GetPixels(); // gets the photo from earlier
        final.SetPixels((int)photoAreaOffset.x, (int)photoAreaOffset.y, (int)photoAreaSize.x, (int)photoAreaSize.y, photoPixels); // uses the vectors to fit the photo taken into the frame for the final product

        final.Apply();

        return Sprite.Create(final, new Rect(0, 0, final.width, final.height), new Vector2(0.5f, 0.5f)); // this returns the photo sprite for the camera to eject
    }

}









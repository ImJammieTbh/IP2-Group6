using UnityEngine;

public class PhotoCapture : MonoBehaviour
{
    public Camera photoCamera;
    public RenderTexture PolaroidRT;

    public void Awake()
    {
        PolaroidRT = new RenderTexture(800, 800, 24); //this is to make the square polaroid photo
        photoCamera.aspect = 800f / 800f;
    }

    public Texture2D Capture()
    {
        photoCamera.targetTexture = PolaroidRT; // this kinda just nicely asks the camera to take what's essentially a screenshot but at a set width and height so it works with the texture it's going to be set to
        photoCamera.Render();

        RenderTexture.active = PolaroidRT;

        Texture2D Photo = new Texture2D(PolaroidRT.width, PolaroidRT.height, TextureFormat.RGB24, false);
        Photo.ReadPixels(new Rect(0, 0, PolaroidRT.width, PolaroidRT.height), 0, 0); // I love reading pixels that go to the width and height of an image
        Photo.Apply();

        photoCamera.targetTexture = null; // resets the camera for the next shot
        RenderTexture.active = null;

        return Photo;
    }
}


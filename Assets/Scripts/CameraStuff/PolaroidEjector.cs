using System.Collections;
using UnityEngine;

public class PolaroidEjector : MonoBehaviour
{
    public CanvasGroup viewfinderBlackout; // I'm not sure if I want to touch UI since I dunno what's up w that
    public AudioSource shutterSound; // haven't found/made the sounds yet
    public AudioSource ejectSound;
    public GameObject polaroidPrefab; // world or UI version
    public Transform uiTransform;
    public Transform ejectPoint;
    public Transform CameraHolder;

    public PhotoCapture capture;
    public PolaroidRenderer render;
    public PolaroidUI uiDisplay;

    public bool isBusy = false;

    public IEnumerator TakePhoto()
    {
        if (isBusy)
            yield break;
        isBusy = true;
        
        // viewfinderBlackout.alpha = 1f;

        yield return new WaitForSeconds(0.05f);

        // shutterSound.Play();
        Texture2D raw = capture.Capture(); //goes to photocapture n tells it to capture

        Sprite polaroid = render.CreatePolaroid(raw); //takes the polaroid sprite

        PhotoManager.Instance.SetLatestPhoto(raw, polaroid);

        // ejectSound.Play(); // Eject animation sound

        GameObject ui = Instantiate(polaroidPrefab, uiTransform);
        ui.GetComponent<PolaroidUI>().Init(polaroid, this);

        yield return new WaitForSeconds(0.1f); // fades back in after a bit
        // viewfinderBlackout.alpha = 0f;
    }
}

// OLD PHYSICAL POLAROID EJECT

//GameObject print = Instantiate(polaroidPrefab, ejectPoint.position, ejectPoint.rotation); // this spawns in the polaroid photo
//print.GetComponentInChildren<SpriteRenderer>().sprite = polaroid;

//var behaviour = print.GetComponent<PolaroidBehaviour>();
//behaviour.followTarget = ejectPoint;
using System;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class CameraLag : MonoBehaviour
{
    public Transform CamPos; // Player Camera
    public Camera Cam; // Player Camera
    public Camera PhotoCam;
    public GameObject FilmCam; // this is the film cam representation, it does nothing but exist and make the player think, "WOW! I'm really taking photos with this!". also, artists please make a cool camera because clearly the current one is very temporary.
    public float rotationLag = 10f;   // higher = snappier, lower = heavier, just for if we use different cameras/lenses, the weight/lag can easily be edited, maybe from a table or smt.
    public float AimSpeed;
    private float sinTime;
    public bool isAiming = false;
    public Transform Aiming;
    public Transform Resting;
    private Transform Current;
    private Transform Target;
    public GameObject ViewFinder; //this can be linked to ui or whatever else, artists pretty please make a nice viewfinder thank you.
    public GameObject UIcrosshair;
    public PolaroidEjector Ejector; //it ejects.
    public float maxDistance;

    public LimitedLook shaker;

    private float TargetFOV;
    private float CurrentFOV;
    private float SavedFOV;
    public float maxZoom;
    public float minZoom;
    private bool dpadup;
    private bool dpaddown;

    public int pics;
    public bool maxPicsReached;

    private InputSystem_Actions _actions;
    private bool _leftTriggerDown;
    private bool _rightTriggerDown;

    public Text FrameCounter;
    private float FramesLeft;

    public static event Action<BirdData, BirdController> OnPhotoTaken;
    public static event Action OnPhotosUsed;

    public void Awake()
    {
        //new input system stuff
        _actions = new InputSystem_Actions();
        _actions.Enable();

        _actions.Player.View.performed += _ => LeftTriggerToggle(); //event for the looking ( left trigger )
        _actions.Player.View.canceled += _ => LeftTriggerToggle();

        _actions.Player.Photo.performed += _ => RightTriggerToggle(); //event for the taking photo ( right trigger )
        _actions.Player.Photo.canceled += _ => RightTriggerToggle();

        _actions.Player.ZoomIn.started += _ => dpadup = true;
        _actions.Player.ZoomIn.canceled += _ => dpadup = false;

        _actions.Player.ZoomOut.started += _ => dpaddown = true;
        _actions.Player.ZoomOut.canceled += _ => dpaddown = false;

    }

    public void Start()
    {
        Current = Resting;
        Target = Aiming;
        maxPicsReached = false;
    }
    public void LateUpdate()
    {


        if (FilmCam.transform.position == Target.position && isAiming == true) // this whole thing just stops the "camera" from rendering whenever you're trying to look through the viewfinder.
        {
            FilmCam.gameObject.SetActive(false);
            ViewFinder.SetActive(true);
            UIcrosshair.SetActive(false);
        }
        else
        {
            FilmCam.gameObject.SetActive(true);
            ViewFinder.SetActive(false);
            UIcrosshair.SetActive(true);
        }

        transform.rotation = Quaternion.Slerp // smoothly rotate toward the Closed rotation.
        (
            transform.rotation,
            CamPos.rotation,
            rotationLag * Time.deltaTime
        );

        if (Input.GetKey(KeyCode.Mouse1) || _leftTriggerDown)
        {
            isAiming = true;
            Swap();
            if (FilmCam.transform.position != Target.position)
            {
                sinTime += Time.deltaTime * AimSpeed;
                sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
                float t = Evaluate(sinTime);
                FilmCam.transform.position = Vector3.Lerp(Current.position, Target.position, t); //checks is rmb is held, changes film cam position and main cam fov.

            }

            // AimTrue();
        }
        else
        {
            isAiming = false;
            Swap();
            if (FilmCam.transform.position != Target.position)
            {
                sinTime += Time.deltaTime * AimSpeed;
                sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
                float t = Evaluate(sinTime);
                FilmCam.transform.position = Vector3.Lerp(Current.position, Target.position, t); //checks if aiming is false, then changes to Resting, sets fov back to normal.

                TargetFOV = 70f;

            }

            // AimFalse();
        }

        CameraZoom();
    }


    public void CameraZoom()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) // this is just for when you begin zooming, the targetFOV is initially set to 50.
        {
            if (SavedFOV < minZoom && SavedFOV > 0) // this checks if SavedFOV is within its constraints, and if it is, it'll just set the target to the saved one.
            {
                TargetFOV = SavedFOV;
            }
            else
            {
                TargetFOV = minZoom; // this is if savedFOV is somehow not within its constraints, for example when you first boot up the game at the moment. but it also helps prevent any weird glitches if they happen ig.
            }
            shaker.shakeActive = true;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (isAiming && scroll >0f && CurrentFOV >= maxZoom) // mind this is FOV, so maxZoom will be smaller than minZoom.
        {
            PlusZoom();
        }
        if (isAiming && dpadup && CurrentFOV >= maxZoom)
        {
            PlusZoom2();
        }

        if (isAiming && scroll <0f && CurrentFOV <= minZoom)
        {
            MinusZoom();
        }
        if (isAiming && dpaddown && CurrentFOV <= minZoom)
        {
            MinusZoom2();
        }

        sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
        float t = Evaluate(sinTime) * 0.025f;
        CurrentFOV = Mathf.Lerp(CurrentFOV, TargetFOV, t);
        Cam.fieldOfView = CurrentFOV;
        PhotoCam.fieldOfView = CurrentFOV;

        if (Input.GetKeyUp(KeyCode.Mouse1)) // saves current fov for when you aim again.
        {
            SavedFOV = CurrentFOV;
            print("saved FOV is" + SavedFOV);
            shaker.shakeActive = false;
        }
    }

    public void Update()
    {
        // SHOOTING STUFF!!!!!!!!!!!

        FramesLeft = (10f - pics);
        FrameCounter.text = ("" + FramesLeft);

        if (FilmCam.transform.position == Target.position && isAiming && (Input.GetKeyDown(KeyCode.Mouse0) || _rightTriggerDown) && !Ejector.isBusy && maxPicsReached == false) // you should totally spam lmb with an autoclicker it's very fun for your pc
        {
            Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("Bird"))
                {
                    print("I miss my wife"); // future voiceline mechanic? very important story telling dialogue.
                    pics++;

                    if (OnPhotoTaken != null)
                        OnPhotoTaken.Invoke(hit.collider.gameObject.GetComponent<BirdController>().birdData, hit.collider.gameObject.GetComponent<BirdController>());
                    StartCoroutine(Ejector.TakePhoto());

                    

                    if (pics >= 10f)
                    {
                        maxPicsReached = true;
                        OnPhotosUsed.Invoke();
                    }
                }

                // or maybe check for a component Jamie adds to the birds
                // if (hit.collider.GetComponent<Birdtype(?)>() != null)
                // {
                //     print("I miss my wife"); probably looks smt like this
                //     RunCameraShit();
                // }
            }
        }
    }

    public float Evaluate(float x)
    {
        return 0.5f * Mathf.Sin(x - Mathf.PI / 2f) + 0.5f; //this is for a smooth transition between Resting and aiming/zooming values
    }

    public void AimTrue()
    {
        isAiming = true;
        Swap();
        if (FilmCam.transform.position != Target.position)
        {
            sinTime += Time.deltaTime * AimSpeed;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
            float t = Evaluate(sinTime);
            FilmCam.transform.position = Vector3.Lerp(Current.position, Target.position, t); //checks is rmb is held, changes film cam position and main cam fov.

        }
    }

    public void AimFalse()
    {
        isAiming = false;
        Swap();
        if (FilmCam.transform.position != Target.position)
        {
            sinTime += Time.deltaTime * AimSpeed;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
            float t = Evaluate(sinTime);
            FilmCam.transform.position = Vector3.Lerp(Current.position, Target.position, t); //checks if aiming is false, then changes to Resting, sets fov back to normal.

            TargetFOV = 70f;

        }
    }
    
    public void Swap() //this is for switching the Closed destination of the moving film camera
    {
        if (isAiming == true)
        {
            if (FilmCam.transform.position != Target.position)
            {
                return;
            }

            if (FilmCam.transform.position == Resting.position)
            {
                Transform t = Current;
                Current = Target;
                Target = t;
                sinTime = 0;
            }
        }
        else
        {
            if (FilmCam.transform.position != Target.position)
            {
                return;
            }

            if (FilmCam.transform.position == Aiming.position)
            {
                Transform t = Current;
                Current = Target;
                Target = t;
                sinTime = 0;
            }

        }
    }

    public void LeftTriggerToggle()
    {
        _leftTriggerDown = !_leftTriggerDown;
        if (!_leftTriggerDown)
        {
            SavedFOV = CurrentFOV;
            print("saved FOV is" + SavedFOV);
        }
        else
        {
            if (SavedFOV < minZoom && SavedFOV > 0) // this checks if SavedFOV is within its constraints, and if it is, it'll just set the target to the saved one.
            {
                TargetFOV = SavedFOV;
            }
            else
            {
                TargetFOV = minZoom; // this is if savedFOV is somehow not within its constraints, for example when you first boot up the game at the moment. but it also helps prevent any weird glitches if they happen ig.
            }
        }
    }

    public void RightTriggerToggle()
    {
        _rightTriggerDown = !_rightTriggerDown;
    }

    public void PlusZoom()
    {
        if (isAiming && CurrentFOV >= maxZoom) // mind this is FOV, so maxZoom will be smaller than minZoom.
        {
            TargetFOV = CurrentFOV - 5f;
        }
    }

    public void MinusZoom()
    {
        if (isAiming && CurrentFOV <= minZoom)
        {
            TargetFOV = CurrentFOV + 5f;
        }
    }

    public void PlusZoom2()
    {
        if (isAiming && CurrentFOV >= maxZoom) // this is also a separate one that adds/takes away less to the fov since the controller one is different.
        {
            TargetFOV = CurrentFOV - 1f;
        }
    }

    public void MinusZoom2()
    {
        if (isAiming && CurrentFOV <= minZoom)
        {
            TargetFOV = CurrentFOV + 1f;
        }
    }
}


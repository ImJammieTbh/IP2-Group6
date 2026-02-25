using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class CameraLag : MonoBehaviour
{
    public Transform CamPos; // Player Camera
    public Camera Cam; // Player Camera
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
    public PolaroidEjector Ejector; //it ejects.
    public float maxDistance;
    
    private InputSystem_Actions _actions;
    private bool _leftTriggerDown;
    private bool _rightTriggerDown;

    public void Awake()
    {
        //new input system stuff
        _actions = new InputSystem_Actions();
        _actions.Enable();
        
        _actions.Player.View.performed += _ => LeftTriggerToggle(); //event for the looking ( left trigger )
        _actions.Player.View.canceled += _ => LeftTriggerToggle();
        
        _actions.Player.Photo.performed += _ => RightTriggerToggle(); //event for the taking photo ( right trigger )
        _actions.Player.Photo.canceled += _ => RightTriggerToggle();
    }

    public void Start()
    {
        Current = Resting;
        Target = Aiming;
    }
    public void LateUpdate()
    {

        // AIMING STUFF
        
        if (FilmCam.transform.position == Target.position && isAiming == true) // this whole thing just stops the "camera" from rendering whenever you're trying to look through the viewfinder.
        {
            FilmCam.GetComponent<MeshRenderer>().enabled = false;
            ViewFinder.SetActive(true);
        }
        else
        {
            FilmCam.GetComponent<MeshRenderer>().enabled = true;
            ViewFinder.SetActive(false);
        }

        // CAM HOLDER ROTATION

        transform.rotation = Quaternion.Slerp // smoothly rotate toward the Target rotation
        (
            transform.rotation,
            CamPos.rotation,
            rotationLag * Time.deltaTime
        );

        // PLAYER MOUSE BUTTONS

        // aiming stuff
        if (Input.GetKey(KeyCode.Mouse1) || _leftTriggerDown)
        {
            isAiming = true;
            Swap();
            if (FilmCam.transform.position != Target.position)
            {
                sinTime += Time.deltaTime * AimSpeed;
                sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
                float t = Evaluate(sinTime);
                FilmCam.transform.position = Vector3.Lerp(Current.position, Target.position, t); //checks is rmb is held, changes film cam position and main cam fov
            }

            Cam.fieldOfView = 50f;

            

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
                FilmCam.transform.position = Vector3.Lerp(Current.position, Target.position, t); //checks if aiming is false, then changes to Resting, sets fov back to normal
            }

            Cam.fieldOfView = 70f;
        }

    }

    public void Update()
    {
        // shooting stuff
        if (FilmCam.transform.position == Target.position && isAiming && Input.GetKeyDown(KeyCode.Mouse0) || _rightTriggerDown) // you should totally spam lmb with an autoclicker it's very fun for your pc
        {
            Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("Bird"))
                {
                    print("I miss my wife"); // future voiceline mechanic? very important story telling dialogue.
                    StartCoroutine(Ejector.TakePhoto());
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

    // FUNNY LITTLE EXTRA THINGS THAT HELP WITH AIMING

    public float Evaluate(float x)
    {
        return 0.5f * Mathf.Sin(x - Mathf.PI / 2f) + 0.5f; //this is for a smooth transition between Resting and aiming.
    }

    public void Swap() //this is for switching the Target destination of the moving film camera
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
    }

    public void RightTriggerToggle()
    {
        _rightTriggerDown = !_rightTriggerDown;
    }
}


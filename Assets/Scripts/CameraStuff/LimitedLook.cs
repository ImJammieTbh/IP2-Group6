
using UnityEngine;
using UnityEngine.UI;

public class LimitedLook : MonoBehaviour
{
    public Transform Cam;

    public float sensitivity = 400f;

    public float maxX;
    public float maxY;

    public float xRotation = 0f;
    public float yRotation = 0f;

    public bool shakeActive = false;
    public float shakeIntensity = 0.5f;
    public float shakeSpeed = 0.005f; // really really slow, if you want something funny just turn this up to anything higher than this lmao (epilepsy warning ofc)

    private float shakeTime = 0f;

    public Slider sensitivitySlider;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X"); //gets mouse x n y from player input
        float mouseY = Input.GetAxis("Mouse Y");

        float joystickX = Input.GetAxisRaw("RightStickX"); //gets right joystick x n y 
        float joystickY = -Input.GetAxisRaw("RightStickY");

        float finalX = (joystickX + mouseX) * (sensitivity * (sensitivitySlider.value / 100)) * Time.deltaTime; // combines to allow for easy switch
        float finalY = (joystickY + mouseY) * (sensitivity * (sensitivitySlider.value / 100)) * Time.deltaTime;

        yRotation += finalX;
        xRotation -= finalY;

        xRotation = Mathf.Clamp(xRotation, -maxX, maxX); //make up rotations
        yRotation = Mathf.Clamp(yRotation, -maxY, maxY);

        
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f); // just the base rotations

        
        Quaternion camRotate = Quaternion.Euler(xRotation, 0f, 0f); // camera pitch rotation


        Quaternion shakeRotate = Quaternion.identity; // adds shake if active
        if (shakeActive)
        {
            //print("shakeActive");
            shakeTime += Time.deltaTime * (shakeSpeed/60);

            float shakeX = (Mathf.PerlinNoise(shakeTime, 0f) - 0.5f) * shakeIntensity;
            float shakeY = (Mathf.PerlinNoise(0f, shakeTime) - 0.5f) * shakeIntensity;

            shakeRotate = Quaternion.Euler(shakeX, shakeY, 0f);
        }

        Cam.localRotation = camRotate * shakeRotate;
    }
}

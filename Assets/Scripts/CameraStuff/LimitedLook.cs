using UnityEngine;

public class LimitedLook : MonoBehaviour
{
    public Transform Cam;

    public float sensitivity = 200f;

    public float maxX; // can be changed when we get the map stuff done, so yk where you're looking n shit
    public float maxY;

    public float xRotation = 0f;
    public float yRotation = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; //gets mouse x n y from player input
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, (maxX*(-1)), maxX); //make up rotations
        yRotation = Mathf.Clamp(yRotation, (maxY*(-1)), maxY);

        
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f); // apply rotations
        Cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


    }
}

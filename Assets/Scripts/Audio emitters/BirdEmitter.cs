using UnityEngine;

public class BirdEmitter : MonoBehaviour
{
    //[Header("MOVING")]
    //public float moveDistance = 10; // max distance it can move, before moving back
    //float currentDistance = 0; // keeps track of how far the object has moved
    //public float moveSpeed = 1f;
    //public int direction = 1; // tells the code to move back until reaches the original position
    //public bool movingX; // tells the object to move in either the x or z axis
    //public bool movingY;
    //public bool movingZ;

    [Header("SFX")]
    public BirdSoundManager birdManager;
    [SerializeField] private AudioClip[] quackSoundClips;
    public float quackCoolDown = 0f;
    public float coolDownStrength = 1f;
    public float coolDownMin = 5f;
    public float coolDownMax = 10f;


    void Update()
    {
        // moving had no benifit to the audio
        ////moving X axis
        //if (movingX)
        //{
        //    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime * direction); // contiues to move based on the objects current position
        //    currentDistance += moveSpeed * Time.deltaTime * direction;

        //    if (currentDistance > moveDistance) // checks if has met the set distance 
        //    {
        //        direction = -direction; // changes the direction the turret is moving
        //    }
        //    else if (currentDistance <= 0 && direction == -1) //has returned to its original position
        //    {
        //        direction = -direction; // changes the direction the turret is moving
        //    }
        //}

        ////moving Y axis
        //if (movingY)
        //{
        //    transform.Translate(Vector3.up * moveSpeed * Time.deltaTime * direction); // contiues to move based on the objects current position
        //    currentDistance += moveSpeed * Time.deltaTime * direction;

        //    if (currentDistance > moveDistance) // checks if has met the set distance 
        //    {
        //        direction = -direction; // changes the direction the turret is moving
        //    }
        //    else if (currentDistance <= 0 && direction == -1) //has returned to its original position
        //    {
        //        direction = -direction; // changes the direction the turret is moving
        //    }
        //}

        ////moving Z axis
        //if (movingZ)
        //{
        //    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * direction); // contiues to move based on the objects current position
        //    currentDistance += moveSpeed * Time.deltaTime * direction;

        //    if (currentDistance > moveDistance) // checks if has met the set distance 
        //    {
        //        direction = -direction; // changes the direction the turret is moving
        //    }
        //    else if (currentDistance <= 0 && direction == -1) //has returned to its original position
        //    {
        //        direction = -direction; // changes the direction the turret is moving
        //    }
        //}

        //quacking
        if (quackCoolDown <= 0)
        {
            birdManager.PlayRandomBirdSoundClip(quackSoundClips, transform, 0.5f);

            quackCoolDown = Random.Range(coolDownMin,coolDownMax);
        }
        else
        {
            quackCoolDown -= coolDownStrength * Time.deltaTime;
        }
    }
}

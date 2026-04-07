using UnityEngine;

interface IInteractable // just add IInteractable next to MonoBehaviour (like this: MonoBehaviour, IInteractable)
{
    public void Interact(); //used so it the object interacted with can be inherited so the interactor can invoke any specific behaviours
}

public class PlayerInteraction : MonoBehaviour
{
    // code very closely taken from this video: https://www.youtube.com/watch?v=K06lVKiY-sY

    public Transform InteractorSource; // stores ref to the transform which the interaction ray will be cast from
    public float InteractRange; // the range of the interactive raycast

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward); //creates a ray infront of the interactor source (camera)

            if (Physics.Raycast(r, out RaycastHit hitinfo, InteractRange)) // if the raycast detects a collision
            {
                if (hitinfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact(); //calls the interact function from the object
                }
            }
        }
    }
}

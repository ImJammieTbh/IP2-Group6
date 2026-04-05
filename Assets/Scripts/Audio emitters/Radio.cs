using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour, IInteractable
{
    float currentVolume; //store the volume before music is muted
    bool isOn = true; //check if interacting with the radio when it is on or not
    public Slider musicVolume;

    public void Interact()
    {
        //print("egg");

        if (isOn)
        {
            currentVolume = musicVolume.value;

            musicVolume.value = 0;
            isOn = false;
        }
        else
        {
            musicVolume.value = currentVolume;
        }
    }
}


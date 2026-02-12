using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    // Tutorial referenced: https://youtu.be/DU7cgVsU2rM?si=eEa7792XOCXeBQVj

    /// Implementing sfx in a different script
    /// [SerializeField] private AudioClip (variable name) , put the audio you want to play in here
    /// 
    /// In Code
    /// SoundFXManager.instance.PlaySFXClip([variable name], transform, 1f);
    /// 

    public static SoundFXManager instance; // Makes it easier to call in other scripts

    [SerializeField] private AudioSource soundFXObject; //skips having to grab a reference to the audio source

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //SINGLE SFX
    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //Spawn game object
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity); //spawning the audio source, at the game object, ration doesn't matter

        //Assign audioclip
        audioSource.clip = audioClip;

        //Assign volume
        audioSource.volume = volume;

        //Play sound
        audioSource.Play();

        //Get length of audioclip
        float clipLength = audioSource.clip.length;

        //Destroy the clip after done playing
        Destroy(audioSource.gameObject, clipLength);
    }



    //RANDOM SFX
    public void PlayRandomSFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        //Random SFX selector
        int rand = Random.Range(0, audioClip.Length);

        //Spawn game object
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity); //spawning the audio source, at the game object, ration doesn't matter

        //Assign audioclip
        audioSource.clip = audioClip[rand];

        //Assign volume
        audioSource.volume = volume;

        //Play sound
        audioSource.Play();

        //Get length of audioclip
        float clipLength = audioSource.clip.length;

        //Destroy the clip after done playing
        Destroy(audioSource.gameObject, clipLength);
    }
}
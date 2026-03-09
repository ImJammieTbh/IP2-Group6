using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    // following tutorial: https://www.youtube.com/watch?v=DU7cgVsU2rM

    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        //audioMixer.SetFloat ("Master volume", level);
        audioMixer.SetFloat("Master Volume", Mathf.Log10(level) * 20); // increases the volume logathrimically 
    }

    public void SetSFXVolume(float level)
    {
        //audioMixer.SetFloat("SFX volume", level);
        audioMixer.SetFloat("Sfx Volume", Mathf.Log10(level) * 20);
    }

    public void SetBirdsVolume(float level)
    {
        //audioMixer.SetFloat("Birds volume", level);
        audioMixer.SetFloat("Birds Volume", Mathf.Log10(level) * 20);
    }

    public void SetMusicVolume(float level)
    {
        //audioMixer.SetFloat("Music volume", level);
        audioMixer.SetFloat("Music Volume", Mathf.Log10(level) * 20);
    }

    public void SetBackgroundVolume(float level)
    {
        //audioMixer.SetFloat("Background volume", level);
        audioMixer.SetFloat("Background Volume", Mathf.Log10(level) * 20);
    }
}

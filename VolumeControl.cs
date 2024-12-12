using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeControl : MonoBehaviour
{
    public Slider musicSlider; // Drag your slider here
    public Slider sfxSlider; // Drag your slider here
    public AudioMixer audioMix;
   

    // Start is called before the first frame update

    // This method will set the global volume level
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMix.SetFloat("music", Mathf.Log10(volume)*20);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMix.SetFloat("sfx", Mathf.Log10(volume)*20);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{

    public AudioSource audioSource;

    public Slider volumeSlider;

    //value from slider
    private float musicVolume = 1f;

    
    private void Start()
    {
        audioSource.Play();
        musicVolume = PlayerPrefs.GetFloat("volume");
        audioSource.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }

    
    private void Update()
    {
        audioSource.volume = musicVolume;
        PlayerPrefs.SetFloat("volume", musicVolume);
    }


    public void VolumeUpdater(float volume)
    {
        musicVolume = volume;
    }
}

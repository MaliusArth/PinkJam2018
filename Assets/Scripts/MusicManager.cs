using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour {

    public AudioSource MyAudioSource;
    public Slider VolumeSlider;
    //float MySliderValue;

    void Start()
    {
        //Initiate the Slider value to half way
        //MySliderValue = 0.7f;
        VolumeSlider.value = 0.7f;
        //Fetch the AudioSource from the GameObject
        MyAudioSource = GetComponent<AudioSource>();
        //Play the AudioClip attached to the AudioSource on startup
        MyAudioSource.Play();
    }

    void Update()
    {
        
        MyAudioSource.volume = VolumeSlider.value;
    }
}
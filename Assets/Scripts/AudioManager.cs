/******************************************************************************
Author: Kang Xuan
Name of Class: AudioManager
Description of Class: Manages the entire audio of the game.
Date Created: 03/08/21
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// AudioMixer that is dragged in
    /// </summary>
    public AudioMixer mixer;

    public void SetMasterVolume(float sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetEffectsVolume(float sliderValue)
    {
        mixer.SetFloat("EffectsVol", Mathf.Log10(sliderValue) * 20);
    }
}

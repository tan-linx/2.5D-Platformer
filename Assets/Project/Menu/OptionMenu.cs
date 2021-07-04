using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//https://www.youtube.com/watch?v=YOaYQrN1oYQ
public class OptionMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}

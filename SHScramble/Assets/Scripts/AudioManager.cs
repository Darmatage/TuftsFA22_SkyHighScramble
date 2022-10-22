using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource flip_audio;

    // Music volume variable that will be modified
    // by dragging slider knob
    private float musicVolume = .3f;

    // Start is called before the first frame update
    void Start()
    {
        flip_audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        flip_audio.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}

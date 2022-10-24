using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UpdateSFX : MonoBehaviour
{
    //public static float audioExposed { get; private set; }
    public AudioMixer masterMixer;

    public void setSFXLevel(float value) {
        masterMixer.SetFloat("audioExposed", value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public AudioClip clip;
    //public AudioMixerGroup output;

    public string name;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;
    public bool playOnAwake;

    [HideInInspector]
    public AudioSource source;
}

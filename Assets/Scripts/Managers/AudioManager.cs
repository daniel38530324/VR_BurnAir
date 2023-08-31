using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] GameObject soundPrefab;
    [SerializeField] SoundData soundData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string clipNmae)
    {
        Sounds s = Array.Find(soundData.Music, sound => sound.name == clipNmae);
        if (s == null)
            return;
        AudioSource sp = Instantiate(soundPrefab, Vector3.zero, Quaternion.identity, transform.GetChild(0)).GetComponent<AudioSource>();

        sp.clip = s.clip;
        //sp.outputAudioMixerGroup = s.output;
        sp.volume = s.volume;
        sp.pitch = s.pitch;
        sp.loop = s.loop;
        sp.playOnAwake = s.playOnAwake;
        sp.Play();
    }

    public void PlaySound(string clipNmae)
    {
        Sounds s = Array.Find(soundData.Sound, sound => sound.name == clipNmae);
        if (s == null)
            return;
        AudioSource sp = Instantiate(soundPrefab, Vector3.zero, Quaternion.identity, transform.GetChild(0)).GetComponent<AudioSource>();

        sp.clip = s.clip;
        //sp.outputAudioMixerGroup = s.output;
        sp.volume = s.volume;
        sp.pitch = s.pitch;
        sp.loop = s.loop;
        sp.playOnAwake = s.playOnAwake;
        sp.Play();
        Destroy(sp.gameObject, sp.clip.length);
    }

    public void Stop()
    {
        AudioSource[] soundPrefabs = transform.GetChild(0).transform.GetComponentsInChildren<AudioSource>();
        foreach (AudioSource item in soundPrefabs)
        {
            Destroy(item.gameObject);
        }
    }

    public void StopSound(string clipNmae)
    {
        Sounds s = Array.Find(soundData.Sound, sound => sound.name == clipNmae);
        if (s == null)
            return;

        AudioSource[] soundPrefabs = transform.GetChild(0).transform.GetComponentsInChildren<AudioSource>();
        foreach (AudioSource item in soundPrefabs)
        {
            if (item.clip == s.clip)
                Destroy(item.gameObject);
        }
    }
}

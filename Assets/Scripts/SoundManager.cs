using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    public AudioInfo[] audioClip;
    public static SoundManager instance = null;
    private float volume = 0.5f;

    public float Volume
    {
        get { return volume; }
        set
        {
            volume = value;
            foreach (AudioInfo audioClip in audioClip)
            {
                audioClip.source.volume = value;
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            foreach (AudioInfo sound in audioClip)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.loop = sound.loop;
                sound.source.volume = sound.volumeSlider;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SwapMusic(scene.name);
    }

    void SwapMusic(string name)
    {
        if (name == "Map1Scene" || name == "TestRoomScene" || name == "Oskar'sWorkScene")
        {

            audioClip[0].source.Play();
        }
        else
        {
            audioClip[0].source.Stop();
        }
    }

    public void PlayEfx(int clipNumber)
    {
        audioClip[clipNumber].source.PlayOneShot(audioClip[clipNumber].clip);
    }
}

[Serializable]
public class AudioInfo
{
    public string clipName;
    public AudioClip clip;
    public bool loop;
    public float volumeSlider = 0.5f;
    [HideInInspector] public AudioSource source;
}

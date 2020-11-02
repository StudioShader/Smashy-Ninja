using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

    public void Awake()
    {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        Play("Hot");
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("INCORRECT AUDIO NAME -" + name);
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("INCORRECT AUDIO NAME -" + name);
            return;
        }
        s.source.Pause();
    }
    public void Continue(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("INCORRECT AUDIO NAME -" + name);
            return;
        }
        s.source.UnPause();
    }
}

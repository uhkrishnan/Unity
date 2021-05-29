using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.playOnAwake = false;
            if (s.source.clip.name == "GameTheme")
            {
                s.source.loop = true;
            }
            if (s.source.clip.name == "ClickSound")
            {
                s.source.priority = 0;
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Mute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.mute = true;
    }

    public void UnMute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.mute = false;
    }

}

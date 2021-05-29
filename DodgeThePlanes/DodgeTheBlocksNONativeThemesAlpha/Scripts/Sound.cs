using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    [HideInInspector]
    public bool mute;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;
}

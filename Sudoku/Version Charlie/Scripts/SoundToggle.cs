using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToggle : MonoBehaviour
{
    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}

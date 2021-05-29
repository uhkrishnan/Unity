using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SoundToggleButton : MonoBehaviour
{
    Toggle soundToggle;

    void Start()
    {
        soundToggle = GetComponent<Toggle>();
        int audioState = GameDataManager.GetSoundState();

        if (audioState == 0)
        {
            soundToggle.isOn = false;
        }
        else
        {
            soundToggle.isOn = true;
        }
    }

    public void ToggleSoundOnValueChange(bool audioIn)
    {
        if (audioIn)
        {
            AudioListener.volume = 1;
            GameDataManager.SetSoundState(1);
        }
        else
        {
            AudioListener.volume = 0;
            GameDataManager.SetSoundState(0);
        }
    }


}

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SoundToggleButton : MonoBehaviour
{
    Toggle soundToggle;

    void Start()
    {
        soundToggle = GetComponent<Toggle>();
        //int audioState = PlayerPrefs.GetInt("audioListener", 1);
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
            //PlayerPrefs.SetInt("audioListener", 1);
            GameDataManager.SetSoundState(1);
        }
        else
        {
            AudioListener.volume = 0;
            //PlayerPrefs.SetInt("audioListener", 0);
            GameDataManager.SetSoundState(0);
        }
    }


}

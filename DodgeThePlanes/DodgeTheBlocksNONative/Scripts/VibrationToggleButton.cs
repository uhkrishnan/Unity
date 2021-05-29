using UnityEngine;
using UnityEngine.UI;
using RDG;

[RequireComponent(typeof(Toggle))]
public class VibrationToggleButton : MonoBehaviour
{
    Toggle vibrationToggle;

    void Start()
    {
        vibrationToggle = GetComponent<Toggle>();
        int vibrationState = PlayerPrefs.GetInt("vibrationListener", 1);
        
        if (vibrationState == 0)
        {
            vibrationToggle.isOn = false;
        }
        else
        {
            vibrationToggle.isOn = true;
        }

    }

    public void ToggleVibrationOnValueChange(bool vibrationIn)
    {
        if (vibrationIn)
        {
            //Handheld.Vibrate();
            Vibration.Vibrate(300);
            PlayerPrefs.SetInt("vibrationListener", 1);
        }
        else
        {
            PlayerPrefs.SetInt("vibrationListener", 0);
        }
    }

}

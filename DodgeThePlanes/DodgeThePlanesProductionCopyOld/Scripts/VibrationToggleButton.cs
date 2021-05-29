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
        //int vibrationState = PlayerPrefs.GetInt("vibrationListener", 1);
        int vibrationState = GameDataManager.GetVibrationState();

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
            //PlayerPrefs.SetInt("vibrationListener", 1);
            GameDataManager.SetVibrationState(1);
        }
        else
        {
            //PlayerPrefs.SetInt("vibrationListener", 0);
            GameDataManager.SetVibrationState(0);
        }
    }

}

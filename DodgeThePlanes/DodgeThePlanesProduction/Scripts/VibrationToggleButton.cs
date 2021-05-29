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
            Vibration.Vibrate(300);
            GameDataManager.SetVibrationState(1);
        }
        else
        {
            GameDataManager.SetVibrationState(0);
        }
    }

}

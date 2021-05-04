using UnityEngine;
using UnityEngine.UI;

public class ExitPopup : MonoBehaviour
{
    [SerializeField] GameObject exitPopup;
    [SerializeField] Button rateUs;
    [SerializeField] Button exit;
    [SerializeField] Button closePopup;
    public void RateUs()
    {
        rateUs.GetComponent<AudioSource>().Play();
        Application.OpenURL("market://details?id=com.MoongaBros.CoinFlip");
    }

    public void Exit()
    {
        exit.GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    public void PopupClose()
    {
        exitPopup.SetActive(false);
    }

}

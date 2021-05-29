using UnityEngine;
using UnityEngine.UI;

public class ExitPopup : MonoBehaviour
{
    [SerializeField] GameObject exitPopup;
    [SerializeField] Button rateUs;
    [SerializeField] Button exit;

    private void Start()
    {
        exitPopup.SetActive(false);
    }

    public void RateUs()
    {
        Application.OpenURL("market://details?id=com.MoongaBros.DodgeThePlanes");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPopup.SetActive(true);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PopupClose()
    {
        exitPopup.SetActive(false);
    }

}

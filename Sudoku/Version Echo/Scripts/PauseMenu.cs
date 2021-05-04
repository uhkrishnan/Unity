using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Text time_text;
    public void DisplayTime()
    {
        AdManager.Instance.HideBanner(); // echo
        time_text.text = Clock.instance.GetCurrentTimeText().text;

    }
}

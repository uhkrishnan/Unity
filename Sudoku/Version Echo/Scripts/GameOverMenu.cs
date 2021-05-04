using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{

    public Text textClock;
    public Button helpToggle;

    void Start()
    {
        AdManager.Instance.HideBanner(); // echo
        helpToggle.interactable = false;
        textClock.text = Clock.instance.GetCurrentTimeText().text;
    }

}

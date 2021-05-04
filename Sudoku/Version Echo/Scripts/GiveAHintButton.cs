using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GiveAHintButton : MonoBehaviour
{
    private Button button;
    private int remainingHints;

    public void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        button.interactable = true;
        remainingHints = 3;
    }

    private void OnButtonClicked()
    {
        // if in unity, dont show add - just show hint
        /*if (Application.isEditor)
        {
            //this places a number on the board while hint button is pressed
            GameEvents.OnGiveAHintMethod();
        }
        else
        {
            AdManager.Instance.ShowRewardedAd();
        }*/

        // version Echo : Default hints = 3.
        if (remainingHints > 0)
        {
            PlayerPrefs.SetInt("hintToggle", 1);
            AdManager.Instance.LoadHint(); // Version Echo
            AdManager.Instance.ShowRewardedAd();
        }

        remainingHints--; // version Echo

    }

    private void OnEnable()
    {
        GameEvents.OnGiveAHintAdOpening += OnGiveAHintAdOpening;
    }


    private void OnDisable()
    {
        GameEvents.OnGiveAHintAdOpening -= OnGiveAHintAdOpening;
    }

    private void OnGiveAHintAdOpening()
    {
        //button.interactable = false;
        // version Echo : default hints = 3
        if (remainingHints <= 1)
        {
            button.interactable = false;
        }
        
        
    }
}

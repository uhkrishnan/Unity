using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GiveAHintButton : MonoBehaviour
{
    private Button button;

    public void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        button.interactable = true;
    }

    private void OnButtonClicked()
    {
        // if in unity, dont show add - just show hint
        if (Application.isEditor)
        {
            GameEvents.OnGiveAHintMethod();
        }
        else
        {
            AdManager.Instance.ShowRewardedAd();
        }
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
        button.interactable = false;
    }
}

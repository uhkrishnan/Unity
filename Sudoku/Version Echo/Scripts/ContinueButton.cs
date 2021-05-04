using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ContinueButton : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI levelText;

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }

    void Start()
    {
        //Debug.Log("continuebutton.start function");
        if(Config.GameDataFileExists() == false)
        {
            //Debug.Log("continuebutton.start.inside if");
            gameObject.GetComponent<Button>().interactable = false;
            timeText.text = " ";
            levelText.text = " ";
        }
        else
        {
            //Debug.Log("continuebutton.start.inside else");
            float delta_time = Config.ReadGameTime();
            delta_time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta_time);
            string hour = LeadingZero(span.Hours);
            string minute = LeadingZero(span.Minutes);
            string seconds = LeadingZero(span.Seconds);

            timeText.text = hour + ":" + minute + ":" + seconds;
            levelText.text = "(" + Config.ReadBoardLevel() + ")";
        }
    }

    public void SetGameData()
    {
        GameSettings.Instance.SetGameMode(Config.ReadBoardLevel());
    }
}

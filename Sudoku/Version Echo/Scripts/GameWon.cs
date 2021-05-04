using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWon : MonoBehaviour
{
    public GameObject WinPopup;
    public Button helpToggle;
    public Text ClockText;
    public string Level;
    public string date;

    void Start()
    {
        WinPopup.SetActive(false);
        ClockText.text = Clock.instance.GetCurrentTimeText().text;
    }

    private void OnBoardCompleted()
    {
        ClockText.text = Clock.instance.GetCurrentTimeText().text;
        Level = GameSettings.Instance.GetGameMode();
        date = System.DateTime.Now.ToString().Substring(0, 10);
        //Debug.Log("date : " + date);
        AddHighscoreEntry(ClockText.text, Level, date);
        WinPopup.SetActive(true);
        AdManager.Instance.HideBanner(); // echo
        helpToggle.interactable = false;
        // need to disable continue button
    }

    private void AddHighscoreEntry(string score, string name, string date)
    {

        // Create HighscoreEntry

        //HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name, date = date };

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }


    private void OnEnable()
    {
        GameEvents.OnBoardCompleted += OnBoardCompleted;
    }

    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= OnBoardCompleted;
    }


    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable]
    private class HighscoreEntry
    {
        public string score;
        public string name;
        public string date;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWon : MonoBehaviour
{
    public GameObject WinPopup;
    public Text ClockText;
    public string Level;

    void Start()
    {
        WinPopup.SetActive(false);
        ClockText.text = Clock.instance.GetCurrentTimeText().text;
    }

    private void OnBoardCompleted()
    {
        ClockText.text = Clock.instance.GetCurrentTimeText().text;
        Level = GameSettings.Instance.GetGameMode();
        AddHighscoreEntry(ClockText.text, Level);
        WinPopup.SetActive(true);
    }

    //public void AddHighscoreEntry(int score, string name)
    private void AddHighscoreEntry(string score, string name)
    {

        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

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
        //public int score;
        //public string name;

        public string score;
        public string name;
    }

}

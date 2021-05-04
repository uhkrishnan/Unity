using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HighscoreTable : MonoBehaviour {

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake() {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        /*if (highscores == null) {
            // There's no stored table, initialize
            Debug.Log("Initializing table with default values...");
            AddHighscoreEntry(1000000, "CMK");
            AddHighscoreEntry(897621, "JOE");
            AddHighscoreEntry(872931, "DAV");
            AddHighscoreEntry(785123, "CAT");
            AddHighscoreEntry(542024, "MAX");
            AddHighscoreEntry(68245, "AAA");
            // Reload
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }*/

        if (highscores != null)
        {
            //Debug.Log("sort high scores");
            //Code to split hh:mm:ss with : and covert all to seconds then sort high score
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                {
                    string score_i = highscores.highscoreEntryList[i].score;
                    string[] seconds_i = score_i.Split(':');
                    int totalTime_i = 0;

                    totalTime_i = (int.Parse(seconds_i[0]) * 3600) + (int.Parse(seconds_i[1]) * 60) + int.Parse(seconds_i[2]);

                    string score_j = highscores.highscoreEntryList[j].score;
                    string[] seconds_j = score_j.Split(':');
                    int totalTime_j = 0;
                    totalTime_j = (int.Parse(seconds_j[0]) * 3600) + (int.Parse(seconds_j[1]) * 60) + int.Parse(seconds_j[2]);

                    if (totalTime_i > totalTime_j)
                    {
                        // Swap
                        HighscoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) 
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }


    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 60f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) {
        default:
            rankString = rank + "TH"; break;

        case 1: rankString = "1ST"; break;
        case 2: rankString = "2ND"; break;
        case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        //int score = highscoreEntry.score;
        string score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;
        
        /*
         * // Highlight First
        if (rank == 1) {
            entryTransform.Find("posText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
        }*/

        transformList.Add(entryTransform);
    }


    //public void AddHighscoreEntry(int score, string name) 
    public void AddHighscoreEntry(string score, string name)
    {
    
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        
        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        /*if (highscores == null) {
            // There's no stored table, initialize
            highscores = new Highscores() {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }*/

        // Add new entry to Highscores
        //highscores.highscoreEntryList.Add(highscoreEntry);

        /*// Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();*/
    }


    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable] 
    private class HighscoreEntry {
        //public int score;
        //public string name;

        public string score;
        public string name;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HighscoreTable : MonoBehaviour {

    private Transform entryContainerEasy;
    private Transform entryTemplateEasy;
    private List<Transform> highscoreEntryTransformListEasy;

    private Transform entryContainerMedium;
    private Transform entryTemplateMedium;
    private List<Transform> highscoreEntryTransformListMedium;

    private Transform entryContainerHard;
    private Transform entryTemplateHard;
    private List<Transform> highscoreEntryTransformListHard;

    private Transform entryContainerVeryHard;
    private Transform entryTemplateVeryHard;
    private List<Transform> highscoreEntryTransformListVeryHard;

    private void Awake() {
        entryContainerEasy = transform.Find("highscoreEntryContainerEasy");
        entryTemplateEasy = entryContainerEasy.Find("highscoreEntryTemplateEasy");

        entryContainerMedium = transform.Find("highscoreEntryContainerMedium");
        entryTemplateMedium = entryContainerMedium.Find("highscoreEntryTemplateMedium");

        entryContainerHard = transform.Find("highscoreEntryContainerHard");
        entryTemplateHard = entryContainerHard.Find("highscoreEntryTemplateHard");

        entryContainerVeryHard = transform.Find("highscoreEntryContainerVeryHard");
        entryTemplateVeryHard = entryContainerVeryHard.Find("highscoreEntryTemplateVeryHard");

        entryTemplateEasy.gameObject.SetActive(false);
        entryTemplateMedium.gameObject.SetActive(false);
        entryTemplateHard.gameObject.SetActive(false);
        entryTemplateVeryHard.gameObject.SetActive(false);

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
            //Code to split hh:mm:ss with ':' and covert all to seconds, then sort high score
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

        
        highscoreEntryTransformListEasy = new List<Transform>();
        highscoreEntryTransformListMedium = new List<Transform>();
        highscoreEntryTransformListHard = new List<Transform>();
        highscoreEntryTransformListVeryHard = new List<Transform>();

        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) 
        {
            //CreateHighscoreEntryTransform(highscoreEntry, entryContainerEasy, highscoreEntryTransformListEasy);
            //CreateHighscoreEntryTransform(highscoreEntry, entryContainerMedium, highscoreEntryTransformListMedium);
            /*if(highscoreEntry.name == "Easy")
            {
                CreateHighscoreEntryTransformEasy(highscoreEntry, entryContainerEasy, highscoreEntryTransformListEasy);
            }
            else if(highscoreEntry.name == "Medium")
            {
                CreateHighscoreEntryTransformMedium(highscoreEntry, entryContainerMedium, highscoreEntryTransformListMedium);
            }*/

            switch (highscoreEntry.name)
            {
                //default:
                    //rankString = rank + "TH"; break;

                case "Easy": CreateHighscoreEntryTransformEasy(highscoreEntry, entryContainerEasy, highscoreEntryTransformListEasy); break;
                case "Medium": CreateHighscoreEntryTransformMedium(highscoreEntry, entryContainerMedium, highscoreEntryTransformListMedium); break;
                case "Hard": CreateHighscoreEntryTransformHard(highscoreEntry, entryContainerHard, highscoreEntryTransformListHard); break;
                case "VeryHard": CreateHighscoreEntryTransformVeryHard(highscoreEntry, entryContainerVeryHard, highscoreEntryTransformListVeryHard); break;
            }

        }

        /*highscoreEntryTransformListMedium = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainerMedium, highscoreEntryTransformListMedium);
        }*/
    }


    private void CreateHighscoreEntryTransformEasy(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) 
    {

        float templateHeight = 60f;
        Transform entryTransformEasy = Instantiate(entryTemplateEasy, container);
        RectTransform entryRectTransformEasy = entryTransformEasy.GetComponent<RectTransform>();
        entryRectTransformEasy.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        //entryTransform.gameObject.SetActive(true);
        //**-

        entryTransformEasy.gameObject.SetActive(true);
        int rank = transformList.Count + 1;

        // show only 5 entries
        if (rank <= 5)
        {
            /*string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }*/

            string rankString;
            if (rank < 10)
            {
                rankString = "0" + rank;
            }
            else
            {
                rankString = rank.ToString();
            }

            entryTransformEasy.Find("posTextEasy").GetComponent<Text>().text = rankString;

            //int score = highscoreEntry.score;
            string score = highscoreEntry.score;

            entryTransformEasy.Find("scoreTextEasy").GetComponent<Text>().text = score.ToString();

            string name = highscoreEntry.name;
            entryTransformEasy.Find("nameTextEasy").GetComponent<Text>().text = name;

            /*
             * // Highlight First
            if (rank == 1) {
                entryTransform.Find("posText").GetComponent<Text>().color = Color.green;
                entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
                entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
            }*/

            transformList.Add(entryTransformEasy);
        }
        else
        {
            Destroy(entryTransformEasy.gameObject);
        }
    }

    private void CreateHighscoreEntryTransformMedium(HighscoreEntry highscoreEntryMedium, Transform containerMedium, List<Transform> transformListMedium)
    {

        float templateHeightMedium = 60f;
        Transform entryTransformMedium = Instantiate(entryTemplateMedium, containerMedium);
        RectTransform entryRectTransformMedium = entryTransformMedium.GetComponent<RectTransform>();
        entryRectTransformMedium.anchoredPosition = new Vector2(0, -templateHeightMedium * transformListMedium.Count);
        //entryTransform.gameObject.SetActive(true);
        //**-

        entryTransformMedium.gameObject.SetActive(true);
        int rank = transformListMedium.Count + 1;

        // show only 5 entries
        if (rank <= 5)
        {
            /*string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }*/

            string rankString;
            if (rank < 10)
            {
                rankString = "0" + rank;
            }
            else
            {
                rankString = rank.ToString();
            }

            entryTransformMedium.Find("posTextMedium").GetComponent<Text>().text = rankString;

            //int score = highscoreEntry.score;
            string score = highscoreEntryMedium.score;

            entryTransformMedium.Find("scoreTextMedium").GetComponent<Text>().text = score.ToString();

            string name = highscoreEntryMedium.name;
            entryTransformMedium.Find("nameTextMedium").GetComponent<Text>().text = name;

            transformListMedium.Add(entryTransformMedium);
        }
        else
        {
            Destroy(entryTransformMedium.gameObject);
        }
    }

    private void CreateHighscoreEntryTransformHard(HighscoreEntry highscoreEntryHard, Transform containerHard, List<Transform> transformListHard)
    {

        float templateHeightHard = 60f;
        Transform entryTransformHard = Instantiate(entryTemplateHard, containerHard);
        RectTransform entryRectTransformHard = entryTransformHard.GetComponent<RectTransform>();
        entryRectTransformHard.anchoredPosition = new Vector2(0, -templateHeightHard * transformListHard.Count);
        //entryTransform.gameObject.SetActive(true);
        //**-

        entryTransformHard.gameObject.SetActive(true);
        int rank = transformListHard.Count + 1;

        // show only 5 entries
        if (rank <= 5)
        {
            /*string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }*/

            string rankString;
            if (rank < 10)
            {
                rankString = "0" + rank;
            }
            else
            {
                rankString = rank.ToString();
            }

            entryTransformHard.Find("posTextHard").GetComponent<Text>().text = rankString;

            //int score = highscoreEntry.score;
            string score = highscoreEntryHard.score;

            entryTransformHard.Find("scoreTextHard").GetComponent<Text>().text = score.ToString();

            string name = highscoreEntryHard.name;
            entryTransformHard.Find("nameTextHard").GetComponent<Text>().text = name;

            transformListHard.Add(entryTransformHard);
        }
        else
        {
            Destroy(entryTransformHard.gameObject);
        }
    }

    private void CreateHighscoreEntryTransformVeryHard(HighscoreEntry highscoreEntryVeryHard, Transform containerVeryHard, List<Transform> transformListVeryHard)
    {

        float templateHeightVeryHard = 60f;
        Transform entryTransformVeryHard = Instantiate(entryTemplateVeryHard, containerVeryHard);
        RectTransform entryRectTransformVeryHard = entryTransformVeryHard.GetComponent<RectTransform>();
        entryRectTransformVeryHard.anchoredPosition = new Vector2(0, -templateHeightVeryHard * transformListVeryHard.Count);
        //entryTransform.gameObject.SetActive(true);
        //**-

        entryTransformVeryHard.gameObject.SetActive(true);
        int rank = transformListVeryHard.Count + 1;

        // show only 5 entries
        if (rank <= 5)
        {
            /*string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }*/

            string rankString;
            if (rank < 10)
            {
                rankString = "0" + rank;
            }
            else
            {
                rankString = rank.ToString();
            }

            entryTransformVeryHard.Find("posTextVeryHard").GetComponent<Text>().text = rankString;

            //int score = highscoreEntry.score;
            string score = highscoreEntryVeryHard.score;

            entryTransformVeryHard.Find("scoreTextVeryHard").GetComponent<Text>().text = score.ToString();

            string name = highscoreEntryVeryHard.name;
            entryTransformVeryHard.Find("nameTextVeryHard").GetComponent<Text>().text = name;

            transformListVeryHard.Add(entryTransformVeryHard);
        }
        else
        {
            Destroy(entryTransformVeryHard.gameObject);
        }
    }


    //public void AddHighscoreEntry(int score, string name) 
    //*** was uncommented // public void AddHighscoreEntry(string score, string name)
    //*** was uncommented // {
    // Create HighscoreEntry
    //*** was uncommented // HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

    // Load saved Highscores
    //*** was uncommented // string jsonString = PlayerPrefs.GetString("highscoreTable");
    //*** was uncommented // Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

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
    //*** was uncommented // }


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

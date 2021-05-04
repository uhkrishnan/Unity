using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HighscoreTable : MonoBehaviour {

    //private Transform entryContainerEasy;
    private Transform entryContainerEasy, entryTemplateEasy;
    private List<Transform> highscoreEntryTransformListEasy;

    private Transform entryContainerMedium, entryTemplateMedium;
    //private Transform entryTemplateMedium;
    private List<Transform> highscoreEntryTransformListMedium;

    private Transform entryContainerHard, entryTemplateHard;
    //private Transform entryTemplateHard;
    private List<Transform> highscoreEntryTransformListHard;

    private Transform entryContainerExtreme, entryTemplateExtreme;
    //private Transform entryTemplateExtreme;
    private List<Transform> highscoreEntryTransformListExtreme;

    private void Awake() {
        entryContainerEasy = transform.Find("highscoreEntryContainerEasy");
        entryTemplateEasy = entryContainerEasy.Find("highscoreEntryTemplateEasy");

        entryContainerMedium = transform.Find("highscoreEntryContainerMedium");
        entryTemplateMedium = entryContainerMedium.Find("highscoreEntryTemplateMedium");

        entryContainerHard = transform.Find("highscoreEntryContainerHard");
        entryTemplateHard = entryContainerHard.Find("highscoreEntryTemplateHard");

        entryContainerExtreme = transform.Find("highscoreEntryContainerExtreme");
        entryTemplateExtreme = entryContainerExtreme.Find("highscoreEntryTemplateExtreme");

        entryTemplateEasy.gameObject.SetActive(false);
        entryTemplateMedium.gameObject.SetActive(false);
        entryTemplateHard.gameObject.SetActive(false);
        entryTemplateExtreme.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores != null)
        {
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
        highscoreEntryTransformListExtreme = new List<Transform>();

        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) 
        {
            switch (highscoreEntry.name)
            {
                case "Easy": CreateHighscoreEntryTransformEasy(highscoreEntry, entryContainerEasy, highscoreEntryTransformListEasy); break;
                case "Medium": CreateHighscoreEntryTransformMedium(highscoreEntry, entryContainerMedium, highscoreEntryTransformListMedium); break;
                case "Hard": CreateHighscoreEntryTransformHard(highscoreEntry, entryContainerHard, highscoreEntryTransformListHard); break;
                case "Extreme": CreateHighscoreEntryTransformExtreme(highscoreEntry, entryContainerExtreme, highscoreEntryTransformListExtreme); break;
            }
        }
    }

    private void CreateHighscoreEntryTransformEasy(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) 
    {

        float templateHeight = 60f;
        Transform entryTransformEasy = Instantiate(entryTemplateEasy, container);
        RectTransform entryRectTransformEasy = entryTransformEasy.GetComponent<RectTransform>();
        entryRectTransformEasy.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);

        entryTransformEasy.gameObject.SetActive(true);
        int rank = transformList.Count + 1;

        // show only 5 entries
        if (rank <= 5)
        {
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
            string score = highscoreEntry.score;
            entryTransformEasy.Find("scoreTextEasy").GetComponent<Text>().text = score.ToString();
            string name = highscoreEntry.name;
            entryTransformEasy.Find("nameTextEasy").GetComponent<Text>().text = name;

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

        entryTransformMedium.gameObject.SetActive(true);
        int rank = transformListMedium.Count + 1;

        // show only 5 entries
        if (rank <= 5)
        {
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

        entryTransformHard.gameObject.SetActive(true);
        int rank = transformListHard.Count + 1;

        // show only 5 entries
        if (rank <= 5)
        {
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

    private void CreateHighscoreEntryTransformExtreme(HighscoreEntry highscoreEntryExtreme, Transform containerExtreme, List<Transform> transformListExtreme)
    {

        float templateHeightExtreme = 60f;
        Transform entryTransformExtreme = Instantiate(entryTemplateExtreme, containerExtreme);
        RectTransform entryRectTransformExtreme = entryTransformExtreme.GetComponent<RectTransform>();
        entryRectTransformExtreme.anchoredPosition = new Vector2(0, -templateHeightExtreme * transformListExtreme.Count);

        entryTransformExtreme.gameObject.SetActive(true);
        int rank = transformListExtreme.Count + 1;

        // show only 5 entries
        if (rank <= 5)
        {
            string rankString;
            if (rank < 10)
            {
                rankString = "0" + rank;
            }
            else
            {
                rankString = rank.ToString();
            }

            entryTransformExtreme.Find("posTextExtreme").GetComponent<Text>().text = rankString;
            string score = highscoreEntryExtreme.score;
            entryTransformExtreme.Find("scoreTextExtreme").GetComponent<Text>().text = score.ToString();
            string name = highscoreEntryExtreme.name;
            entryTransformExtreme.Find("nameTextExtreme").GetComponent<Text>().text = name;

            transformListExtreme.Add(entryTransformExtreme);
        }
        else
        {
            Destroy(entryTransformExtreme.gameObject);
        }
    }

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable] 
    private class HighscoreEntry {
        public string score;
        public string name;
    }

}
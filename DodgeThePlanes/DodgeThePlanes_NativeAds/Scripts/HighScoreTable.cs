using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField] Transform highScoreContainer;
    [SerializeField] Transform highScoreTemplate;
    [Space(20)]
    [Header("Shop Events")]
    [SerializeField] GameObject highScoreUI;
    [SerializeField] Button openHighScoreButton;
    [SerializeField] Button closeHighScoreButton;

    [SerializeField] GameObject goldMedal;
    [SerializeField] GameObject silverMedal;
    [SerializeField] GameObject bronzeMedal;

    private List<Transform> highscoreEntryTransformList;
    private GameDataManager.Highscores highscores;

    private void Awake()
    {
        highScoreTemplate.gameObject.SetActive(false);
        highScoreUI.SetActive(false);
        highscores = GameDataManager.LoadHighScoreData();

        highscoreEntryTransformList = new List<Transform>();

        foreach (GameDataManager.HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, highScoreContainer, highscoreEntryTransformList); 
        }
    }

    private void Start()
    {

        goldMedal.gameObject.SetActive(false);
        silverMedal.gameObject.SetActive(false);
        bronzeMedal.gameObject.SetActive(false);
        AddHighScoreEvents();
    }

    void AddHighScoreEvents()
    {
        openHighScoreButton.onClick.RemoveAllListeners();
        openHighScoreButton.onClick.AddListener(OpenHighScorePanel);

        closeHighScoreButton.onClick.RemoveAllListeners();
        closeHighScoreButton.onClick.AddListener(CloseHighScorePanel);
    }

    private void CreateHighscoreEntryTransform(GameDataManager.HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {

        float templateHeight = 100f;
        Transform entryTransform = Instantiate(highScoreTemplate, container);
        RectTransform entryRectTransformEasy = entryTransform.GetComponent<RectTransform>();
        entryRectTransformEasy.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);

        entryTransform.gameObject.SetActive(true);
        int rank = transformList.Count + 1;

        if(rank <= 10)
        {
            string rankString;
            if (rank <= 9)
            {
                rankString = "0" + rank;
            }
            else
            {
                rankString = rank.ToString();
            }

            entryTransform.Find("RankTMP").GetComponent<Text>().text = rankString;
            int score = highscoreEntry.score;
            entryTransform.Find("ScoreTMP").GetComponent<Text>().text = score.ToString();
            string date = highscoreEntry.date;
            entryTransform.Find("DateTMP").GetComponent<Text>().text = date;

            transformList.Add(entryTransform);
        }
        else
        {
            Destroy(entryTransform.gameObject);
        }
        
        
    }
    
    void OpenHighScorePanel()
    {
        highScoreUI.SetActive(true);
        if (highscores.highscoreEntryList.Count == 1)
        {
            goldMedal.SetActive(true);
        }else if (highscores.highscoreEntryList.Count == 2)
        {
            goldMedal.SetActive(true);
            silverMedal.SetActive(true);
        }else if (highscores.highscoreEntryList.Count > 2)
        {
            goldMedal.SetActive(true);
            silverMedal.SetActive(true);
            bronzeMedal.SetActive(true);
        }


    }

    void CloseHighScorePanel()
    {
        highScoreUI.SetActive(false);
    }
}
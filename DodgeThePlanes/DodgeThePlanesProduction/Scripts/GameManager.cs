using UnityEngine;
using UnityEngine.UI;
using System;
using RDG;

public class GameManager : MonoBehaviour
{
    [SerializeField] Image noAdsToPlayPopup;
    [SerializeField] Image gameOverPopupBackground;
    [SerializeField] Text milesCounterTMP;
    [SerializeField] GameObject gameOverPopup;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject controlHints;
    [SerializeField] GameObject player;
    [SerializeField] GameObject highScoreAchieved;
    [SerializeField] AudioManager audioManager;

    private Animator highScoreAchievedAnimator;

    private float timer = 0.0f;
    private int highestScore;
    private int vibrationState;

    private void Start()
    {
        gameOverPopup.SetActive(false);
        explosionPrefab.SetActive(false);
        player.SetActive(false);
        highScoreAchieved.SetActive(false);
        highestScore = GameDataManager.GetHighestScore();
        vibrationState = GameDataManager.GetVibrationState();
        highScoreAchievedAnimator = highScoreAchieved.GetComponent<Animator>();
        highScoreAchievedAnimator.enabled = false;
        this.Wait(4.5f, () =>
        {
            controlHints.GetComponent<Animator>().enabled = false;
            controlHints.SetActive(false);
            player.SetActive(true);
        });
    }
    
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        milesCounterTMP.text = Convert.ToInt32(timer).ToString();
        if(timer > highestScore)
        {
            HighScoreAchieved();
        }
    }

    private void HighScoreAchieved()
    {
        if(highestScore > 0)
        {
            highScoreAchieved.SetActive(true);
            highScoreAchievedAnimator.enabled = true;
            Invoke("DisableHighScoreAcheived", 3f);
        }
    }

    private void DisableHighScoreAcheived()
    {
        highScoreAchieved.SetActive(false);
        highScoreAchievedAnimator.enabled = false;
    }

    public void ExplodeGameOver()
    {
        explosionPrefab.SetActive(true);
        if(vibrationState == 1)
        {
            Vibration.Vibrate(300);
        }
        audioManager.Play("Explosion");
        this.Wait(0.5f, () =>
        {
            gameOverPopup.SetActive(true);
            PauseGame();
        });
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        audioManager.Mute("GameTheme");
    }

    public void RemovePause()
    {
        Time.timeScale = 1;
        audioManager.UnMute("GameTheme");
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
        FindObjectOfType<AudioManager>().UnMute("GameTheme");
    }

    public void SaveHighScoreOnExit()
    {
        string date = System.DateTime.Now.ToString("MMM dd").ToUpper();
        GameDataManager.AddHighscoreEntry((int)timer, date);
    }
}
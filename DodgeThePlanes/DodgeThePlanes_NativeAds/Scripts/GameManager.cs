using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float slowness = 10f;
    [SerializeField] GameObject gameOverPopup;
    [SerializeField] Image noAdsToPlayPopup;
    [SerializeField] Image gameOverPopupBackground;
    //[SerializeField] SpriteRenderer backgroundImage;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] Text milesCounterTMP;
    private float timer, counter = 0.0f;

    [SerializeField] GameObject controlHints;
    [SerializeField] GameObject player;


    private void Start()
    {
        gameOverPopup.SetActive(false);
        explosionPrefab.SetActive(false);
        player.SetActive(false);
        //Invoke("DisableControlHints", 3.25f);
        this.Wait(3.25f, () =>
        {
            controlHints.GetComponent<Animator>().enabled = false;
            controlHints.SetActive(false);
            player.SetActive(true);
        });
    }
    /*
    private void DisableControlHints()
    {
        controlHints.GetComponent<Animator>().enabled = false;
        controlHints.SetActive(false);
        player.SetActive(true);

    }*/

    //private void Update()
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        milesCounterTMP.text = Convert.ToInt32(timer).ToString();
        counter += Time.deltaTime;        
    }

    public void ExplodeGameOver()
    {
        explosionPrefab.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Explosion");
        //Invoke("GameOver", 0.5f);
        this.Wait(0.5f, () =>
        {
            gameOverPopup.SetActive(true);
            PauseGame();
        });
    }
    /*
    public void GameOver()
    {
        gameOverPopup.SetActive(true);
        PauseGame();
    }*/

    void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void RemovePause()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }


    public static void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false; 
    }

    public void SaveHighScoreOnExit()
    {
        //date = System.DateTime.Now.ToString().Substring(0, 10);
        //string date = System.DateTime.Now.ToString("MMM dd, yyyy");
        string date = System.DateTime.Now.ToString("MMM dd");
        //rank auto populate
        GameDataManager.AddHighscoreEntry((int)timer, date);
    }

}


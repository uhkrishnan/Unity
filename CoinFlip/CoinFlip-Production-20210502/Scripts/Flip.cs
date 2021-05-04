﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Flip : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Rigidbody coin;
    [SerializeField] Button resetButton;
    [SerializeField] Button headsButton;
    [SerializeField] Button tailsButton;
    [SerializeField] Button flipButton;
    [SerializeField] Button flipButtonThree;
    [SerializeField] Button flipButtonFive;
    [SerializeField] Button rateUsButton;
    [SerializeField] Button exitButton;
    [SerializeField] Image closePopupButton;
    [SerializeField] Button ribbon;
    [SerializeField] GameObject statsPanel;
    [SerializeField] GameObject totalCounterPanel;
    [SerializeField] TextMeshProUGUI headsTMP;
    [SerializeField] TextMeshProUGUI tailsTMP;
    [SerializeField] TextMeshProUGUI remainingFlipsTMP;
    [SerializeField] TextMeshProUGUI headStats;
    [SerializeField] TextMeshProUGUI tailStats;
    [SerializeField] GameObject exitPopup;
    [SerializeField] GameObject popupPanel;

    private Quaternion startRotation;
    private float rotateForce;    
    private bool clicked = false;
    private bool buttonOneClicked = false;
    private bool buttonThreeClicked = false;
    private bool buttonFiveClicked = false;
    private bool coinDragEnable = false;
    private int randomFlips;
    private int flipCount = 0;
    private int totalFlips = 0;
    private int headsCount = 0;
    private int tailsCount = 0;
    private int FlipButtonCounter = 0;
    private int savedHeadStats = 0;
    private int savedTailStats = 0;

    private void Awake()
    {
        Color startColor = GetColor("canvasColor");
        if(startColor.r == 0)
        {
            Color themeButtonColor = canvas.GetComponent<Image>().color;
            CopyColour.SaveColor(themeButtonColor, "canvasColor");

        }

        canvas.GetComponent<Image>().color = flipButton.GetComponent<Image>().color
            = flipButtonThree.GetComponent<Image>().color = flipButtonFive.GetComponent<Image>().color
            = resetButton.GetComponent<Image>().color = headsButton.GetComponent<Image>().color
            = rateUsButton.GetComponent<Image>().color = exitButton.GetComponent<Image>().color
            = closePopupButton.GetComponent<Image>().color = tailsButton.GetComponent<Image>().color
            = popupPanel.GetComponent<Image>().color = GetColor("canvasColor");

        coin.GetComponent<MeshRenderer>().material.SetColor("_Color", GetColor("canvasColor"));
        ButtonNotInteractable();
        exitPopup.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPopup.SetActive(true);
        }
    }

    public static Color GetColor(string key)
    {
        float R = PlayerPrefs.GetFloat(key + "R");
        float G = PlayerPrefs.GetFloat(key + "G");
        float B = PlayerPrefs.GetFloat(key + "B");
        return new Color(R, G, B);
    }

    private void Start()
    {
        randomFlips = 0;
        totalFlips = 0;
        tailsCount = 0;
        headsCount = 0;
        FlipButtonCounter = 0;
        headsTMP.text = "0";
        tailsTMP.text = "0";
        savedHeadStats = 0;
        savedTailStats = 0;
        rotateForce = 4725f;
        totalCounterPanel.SetActive(false);
        ButtonNotInteractable();

        coin.GetComponent<Animator>().enabled = true;

        this.Wait(2f, () =>
        {
            coin.GetComponent<Animator>().enabled = false;
            ButtonIsInteractable();
        });
        startRotation = coin.transform.rotation;
        savedHeadStats = PlayerPrefs.GetInt("headStats");
        headStats.text = savedHeadStats.ToString();
        savedTailStats = PlayerPrefs.GetInt("tailStats");
        tailStats.text = savedTailStats.ToString();
    }
    
    public void Clicked()
    {
        if (FlipButtonCounter == 2)
        {
            Admob.Instance.ShowBanner();
        }

        FlipButtonCounter++;
        flipCount = 1;
        clicked = true;
        buttonOneClicked = true;
        coinDragEnable = false;
        ButtonNotInteractable();
    }

    public void ClickedThree()
    {
        flipCount = 3;
        totalFlips = 3;
        clicked = true;
        buttonThreeClicked = true;
        coinDragEnable = false;
        ButtonNotInteractable();
    }

    public void ClickedFive()
    {
        flipCount = 5;
        totalFlips = 5;
        clicked = true;
        buttonFiveClicked = true;
        coinDragEnable = false;
        ButtonNotInteractable();
    }

    void FixedUpdate()
    {
        if (clicked)
        {
            randomFlips = Random.Range(1, 10);
            RotateCoin();
        }

        if (!clicked && coinDragEnable)
        {
            coin.angularDrag += 0.01f;
        }

        if (coinDragEnable && coin.angularVelocity.y < 0.01f && coin.angularVelocity.y > 0 && buttonOneClicked)
        {
            ResetCoinRotation(coin);
            coinDragEnable = false;
            buttonOneClicked = false;
            UpdateCountOnScreen();
            ButtonIsInteractable();

        }
        else if (coinDragEnable && coin.angularVelocity.y < 0.01f && coin.angularVelocity.y > 0 && buttonThreeClicked)
        {
            flipCount--;
            if (flipCount > 0)
            {
                clicked = true;
                coinDragEnable = false;
            }
            if (flipCount == 0)
            {
                ButtonIsInteractable();
            }

            UpdateRemainingFlips();
            UpdateCountOnScreen();
            ResetCoinRotation(coin);
            
        }
        else if (coinDragEnable && coin.angularVelocity.y < 0.01f && coin.angularVelocity.y > 0 && buttonFiveClicked)
        {
            flipCount--;
            if (flipCount > 0)
            {
                clicked = true;
                coinDragEnable = false;
            }
            if (flipCount == 0)
            {
                ButtonIsInteractable();
            }

            UpdateRemainingFlips();
            UpdateCountOnScreen();
            ResetCoinRotation(coin);
        }

    }
    
    private void RotateCoin()
    {
        coin.GetComponent<AudioSource>().Play();
        coin.angularDrag = 0f;
        coin.AddTorque(0, rotateForce * randomFlips * Time.deltaTime, 0);
        clicked = false;
        coinDragEnable = true;
    }

    private void ResetCoinRotation(Rigidbody coin)
    {
        coin.angularVelocity = Vector3.zero;
        coin.velocity = Vector3.zero;
        coin.angularDrag = 0f;

        if ((transform.localRotation.z <= -0.50 && transform.localRotation.z >= -0.80) ||
            (transform.localRotation.z >= 0.50 && transform.localRotation.z <= 0.80))
        {
            Vector3 temp = transform.rotation.eulerAngles;
            temp.z = 0;
            transform.rotation = Quaternion.Euler(temp);
        }
        else
        {
            coin.transform.rotation = startRotation;
        }
    }

    public void UpdateCountOnScreen()
    {   
        if ((transform.localRotation.z <= -0.50 && transform.localRotation.z >= -0.80 ) || 
            (transform.localRotation.z >= 0.50 && transform.localRotation.z <= 0.80))
        {
            tailsCount++;
            savedTailStats++;
            PlayerPrefs.SetInt("tailStats", savedTailStats);
            tailStats.text = savedTailStats.ToString();
            tailsTMP.text = tailsCount.ToString();
            StartBlinkAnimation(tailsTMP);
        }
        else
        {
            headsCount++;
            savedHeadStats++;
            PlayerPrefs.SetInt("headStats", savedHeadStats);
            headStats.text = savedHeadStats.ToString();
            headsTMP.text = headsCount.ToString();
            StartBlinkAnimation(headsTMP);
        }

    }
    
    private void StartBlinkAnimation(TextMeshProUGUI textTMP)
    {
        textTMP.GetComponent<Animator>().enabled = true;
        this.Wait(3f, () =>
        {
            tailsTMP.GetComponent<Animator>().enabled = false;
            headsTMP.GetComponent<Animator>().enabled = false;
        });
    }
    
    public void UpdateRemainingFlips()
    {
        if(flipCount > 0)
        {
            remainingFlipsTMP.text = "Flips Remaining : " + flipCount + "/" + totalFlips;
        }
        else
        {
            remainingFlipsTMP.text = " ";
        }
        
    }

    public void ResetCountOnScreen()
    {
        resetButton.GetComponent<AudioSource>().Play();
        tailsCount = 0;
        headsCount = 0;
        headsTMP.text = headsCount.ToString();
        tailsTMP.text = tailsCount.ToString();
    }
    private void ButtonNotInteractable()
    {
        flipButton.interactable = false;
        flipButtonThree.interactable = false;
        flipButtonFive.interactable = false;
        resetButton.interactable = false;
        ribbon.interactable = false;
    }
    private void ButtonIsInteractable()
    {
        this.Wait(1f, () =>
        {
            tailsTMP.GetComponent<Animator>().enabled = false;
            headsTMP.GetComponent<Animator>().enabled = false;
        });
        flipButton.interactable = true;
        flipButtonThree.interactable = true;
        flipButtonFive.interactable = true;
        resetButton.interactable = true;
        ribbon.interactable = true;
    }

    public void RibbonClicked()
    {
        coin.gameObject.SetActive(false);
        statsPanel.GetComponent<Animator>().enabled = true;
        statsPanel.GetComponent<Animator>().Play("StatPanelDown");
        ribbon.GetComponent<AudioSource>().Play();
        ButtonNotInteractable();
        ribbon.Wait(0.8f, () =>
        {
            totalCounterPanel.SetActive(true);
            ribbon.Wait(4f, () =>
            {
                statsPanel.GetComponent<AudioSource>().Play();
                statsPanel.GetComponent<Animator>().Play("StatPanelUp");
                totalCounterPanel.SetActive(false);
                ribbon.Wait(1f, () =>
                {
                    coin.gameObject.SetActive(true);
                    ButtonIsInteractable();
                });
            });
        });

    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Flip : MonoBehaviour
{
    public Canvas canvas;
    public Rigidbody coin;
    public Button resetButton;
    public Button headsButton;
    public Button tailsButton;
    public Button flipButton;
    public Button flipButtonThree;
    public Button flipButtonFive;
    public Button ribbon;
    public GameObject statsPanel;
    public GameObject totalCounterPanel;
    public TextMeshProUGUI headsTMP;
    public TextMeshProUGUI tailsTMP;
    public TextMeshProUGUI remainingFlipsTMP;
    public TextMeshProUGUI headStats;
    public TextMeshProUGUI tailStats;

    // To capture the Coin's initial state
    private Quaternion startRotation;
    
    private float rotateForce;
    // even - 2 flips. odd - 1 flip
    private int randomFlips; 
    
    private bool clicked = false;
    private bool buttonOneClicked = false;
    private bool buttonThreeClicked = false;
    private bool buttonFiveClicked = false;
    private bool coinDragEnable = false;
    private bool firstTimeEven = false;
    private bool statPanelOff = false;

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
            = tailsButton.GetComponent<Image>().color = GetColor("canvasColor");

        coin.GetComponent<MeshRenderer>().material.SetColor("_Color", GetColor("canvasColor"));



        ButtonNotInteractable();
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
        
        coin.GetComponent<Animator>().enabled = true;
        flipButton.GetComponent<Animator>().enabled = true;
        flipButtonThree.GetComponent<Animator>().enabled = true;
        flipButtonFive.GetComponent<Animator>().enabled = true;

        totalCounterPanel.SetActive(false);

        ButtonNotInteractable();

        Invoke("StopFirstCoinAnimation", 2);
        startRotation = coin.transform.rotation;

        savedHeadStats = PlayerPrefs.GetInt("headStats");
        headStats.text = savedHeadStats.ToString();
        savedTailStats = PlayerPrefs.GetInt("tailStats");
        tailStats.text = savedTailStats.ToString();



    }

    private void StopFirstCoinAnimation()
    {
        coin.GetComponent<Animator>().enabled = false;
        flipButton.GetComponent<Animator>().enabled = false;
        flipButtonThree.GetComponent<Animator>().enabled = false;
        flipButtonFive.GetComponent<Animator>().enabled = false;
        ButtonIsInteractable();
    }

    public void Clicked()
    {
        if(FlipButtonCounter > 1)
        {
            Admob.Instance.ShowBanner();
        }

        FlipButtonCounter++;
        // prevent further interaction untill toss completion
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
            randomFlips = Random.Range(1, 10); // 0 to 9
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
        // rotate coin
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
        /*
        if (randomFlips % 2 == 0)
        {  
            coin.transform.rotation = startRotation;
        }
        else
        {
            Vector3 temp = transform.rotation.eulerAngles;
            temp.z = 0;
            transform.rotation = Quaternion.Euler(temp);
        }*/

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
        //tailsTMP.GetComponent<Animator>().enabled = false;
        //headsTMP.GetComponent<Animator>().enabled = false;
        
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
        Invoke("StopBlinkAnimation", 3);
    }

    private void StopBlinkAnimation()
    {
        tailsTMP.GetComponent<Animator>().enabled = false;
        headsTMP.GetComponent<Animator>().enabled = false;
    }

    public void UpdateRemainingFlips()
    {
        if(flipCount > 0)
        {
            //remainingFlipsTMP.text = ". . . Flips Remaining " + flipCount + "/" + totalFlips + " . . .";
            remainingFlipsTMP.text = "Flips Remaining " + flipCount + "/" + totalFlips;
        }
        else
        {
            remainingFlipsTMP.text = " ";
        }
        
    }

    public void ResetCountOnScreen()
    {
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
        //headsButton.interactable = false;
        //tailsButton.interactable = false;
        ribbon.interactable = false;
    }
    private void ButtonIsInteractable()
    {
        Invoke("StopBlinkAnimation", 1);
        flipButton.interactable = true;
        flipButtonThree.interactable = true;
        flipButtonFive.interactable = true;
        resetButton.interactable = true;
        //headsButton.interactable = true;
        //tailsButton.interactable = true;
        ribbon.interactable = true;
    }

    public void RibbonClicked()
    {
        /*
         * statPanelOff = !statPanelOff;
        if (statPanelOff)
        {
            coin.gameObject.SetActive(false);
            statsPanel.GetComponent<Animator>().enabled = true;
            statsPanel.GetComponent<Animator>().Play("StatPanelDown");
            Invoke("RibbonAnimationStop", 0.9f);
            ButtonNotInteractable();
            
        }
        else
        {
            //statsPanel.GetComponent<Animator>().enabled = true;
            statsPanel.GetComponent<Animator>().StopPlayback();
            statsPanel.GetComponent<Animator>().Play("StatPanelUp");
            
            totalCounterPanel.SetActive(false);
            Invoke("RibbonAnimationUp", 1);
            ButtonIsInteractable();
        }*/

        // changed to auto close TotalCounterPanel
        coin.gameObject.SetActive(false);
        statsPanel.GetComponent<Animator>().enabled = true;
        statsPanel.GetComponent<Animator>().Play("StatPanelDown");
        Invoke("RibbonAnimationStop", 0.9f);
        ButtonNotInteractable();
        Invoke("RibbonAnimationUp", 4);



    }

    private void RibbonAnimationStop()
    {
        //statsPanel.GetComponent<Animator>().enabled = false;
        totalCounterPanel.SetActive(true);
        //statsPanel.GetComponent<Animator>().Play("RibbonBlink");

    }

    private void RibbonAnimationUp()
    {
        //statsPanel.GetComponent<Animator>().enabled = false;

        //statsPanel.GetComponent<Animator>().StopPlayback();
        statsPanel.GetComponent<Animator>().Play("StatPanelUp");

        totalCounterPanel.SetActive(false);
        Invoke("EnableCoin", 1);
        ButtonIsInteractable();
        
    }

    private void EnableCoin()
    {
        coin.gameObject.SetActive(true);
    }
}

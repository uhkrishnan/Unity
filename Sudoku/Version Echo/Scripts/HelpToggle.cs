using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HelpToggle : MonoBehaviour, IPointerClickHandler
{
    public Image whiteCircleImage;
    public Image iImage;
    private Color _backgroundColor;
    private int prevCanvasColourValue;
    //off position
    private Vector3 position = new Vector3 (404.5f, 890f, 0f);
    private Vector3 offset = new Vector3(30f, 0f, 0f);
    private bool toggle = true;
    private int _currentItem;

    public GameObject helpPopup;
    public TextMeshProUGUI itemText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI resumeGameText;
    public Button nextButton;

    //List<string> colourList = new List<string>() { "#2D3A49", "#F57F4F", "#32533D", "#E8B447", "#B7386E" };
    List<string> colourList = new List<string>() { "#3E7EC7", "#F57F4F", "#497A5A", "#E8B447", "#DE4587" };
    // Help items
    List<string> itemList = new List<string>() { "Numbers", "Grid", "Delete", "Notes", "Lives", "Time", "Hint", "Pause"};
    //Help item descriptions
    List<string> descriptionList = new List<string>
    {
        "Click on a number to select it, press a cell on the board to place the number",
        "Sudoku Board where numbers need to be filled",
        "Button to erase a number already placed on the board. Click on delete button and select the number on the board",
        "Toggle note mode. Enter multiple numbers in a particular cell on the board. Press once to enter number press again to remove it",
        "Upto three mistakes allowed. Once you run out, game over!",
        "Displays the total time in the game. This is your score.",
        "Hint button fills up a random number on the board after watching an ad",
        "Pause, save and exit game"
    };

    public GameObject numbersArrow, gridArrow, deleteArrow, notesArrow, livesArrow, timeArrow, hintArrow, pauseArrow, backArrow;


    void Start()
    {
        prevCanvasColourValue = PlayerPrefs.GetInt("canvasColour");
        ColorUtility.TryParseHtmlString(colourList[prevCanvasColourValue], out _backgroundColor);
        whiteCircleImage.color = _backgroundColor;
        helpPopup.SetActive(false);

        //popup colouring
        itemText.color = _backgroundColor;
        resumeGameText.color = _backgroundColor;
        nextButton.image.color = _backgroundColor;
        nextButton.GetComponent<Image>().color = _backgroundColor;
        

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (toggle == true)
        {
            position.x += offset.x;
            //whiteCircleImage.GetComponent<Transform>().position = position;
            whiteCircleImage.GetComponent<RectTransform>().localPosition = position;
            whiteCircleImage.color = Color.white;
            //iImage.GetComponent<Transform>().position = position;
            iImage.GetComponent<RectTransform>().localPosition = position;


            // call function to display help text popups
            HelpPopup();

            toggle = false;
        }
        // help off toggle handled by Closehelp function
        /*else
        {
            position.x -= offset.x;
            whiteCircleImage.GetComponent<RectTransform>().localPosition = position;
            whiteCircleImage.color = _backgroundColor;
            iImage.GetComponent<RectTransform>().localPosition = position;
            toggle = true;
        }*/
        
    }

    public void HelpPopup()
    {
        helpPopup.SetActive(true);
        _currentItem = 0;
        itemText.text = itemList[_currentItem];
        descriptionText.text = descriptionList[_currentItem];
        nextButton.interactable = true;
        numbersArrow.SetActive(true);
    }

    public void GetNextHelpItem()
    {
        _currentItem++;

        if(_currentItem < 8)
        {
            itemText.text = itemList[_currentItem];
            descriptionText.text = descriptionList[_currentItem];

            //numbersArrow, gridArrow, deleteArrow;
            switch (_currentItem)
            {
                case 0:
                    numbersArrow.SetActive(true);
                    break;
                case 1:
                    numbersArrow.SetActive(false);
                    gridArrow.SetActive(true);
                    break;
                case 2:
                    gridArrow.SetActive(false);
                    deleteArrow.SetActive(true);
                    break;
                case 3:
                    deleteArrow.SetActive(false);
                    notesArrow.SetActive(true);
                    break;
                case 4:
                    notesArrow.SetActive(false);
                    livesArrow.SetActive(true);
                    break;
                case 5:
                    livesArrow.SetActive(false);
                    timeArrow.SetActive(true);
                    break;
                case 6:
                    timeArrow.SetActive(false);
                    hintArrow.SetActive(true);
                    break;
                case 7:
                    hintArrow.SetActive(false);
                    pauseArrow.SetActive(true);
                    break;
            }
        }
        else
        {
            nextButton.interactable = false;
        }
        
        
    }

    public void CloseHelp()
    {
        helpPopup.SetActive(false);
        position.x -= offset.x;
        whiteCircleImage.GetComponent<RectTransform>().localPosition = position;
        whiteCircleImage.color = _backgroundColor;
        iImage.GetComponent<RectTransform>().localPosition = position;
        toggle = true;
    }
   



}

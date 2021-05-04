using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject number_text;
    public List<GameObject> number_notes;

    public bool note_active;
    private int number_ = 0;
    private int hintToggle;
    private int correct_number_ = 0;

    private bool selected_ = false;
    private int square_index_ = -1;
    private bool has_default_value_ = false;
    private bool has_wrong_value = false;
    private int prevCanvasColourValue;
    private Color _backgroundColor;
    public Text hintText;

    //List<string> colourList = new List<string>() { "#2D3A49", "#F57F4F", "#32533D", "#E8B447", "#B7386E" };
    List<string> colourList = new List<string>() { "#3E7EC7", "#F57F4F", "#497A5A", "#E8B447", "#DE4587" };

    void Start()
    {
        selected_ = false;
        note_active = false;
        hintText.text = " ";
        
        // get the canvas colour to be user elsewhere
        prevCanvasColourValue = PlayerPrefs.GetInt("canvasColour");
        ColorUtility.TryParseHtmlString(colourList[prevCanvasColourValue], out _backgroundColor);

        if (GameSettings.Instance.GetContinuePreviousGame() == false)
        {
            SetNoteNumberValue(0);
        }
        else
        {
            SetClearEmptyNotes();
        }

    }

    public int GetSquareNumber()
    {
        return number_;
    }
    public bool IsCorrectNumberSet()
    { 
        return number_ == correct_number_; 
    }

    public bool HasWrongValue()
    {
        return has_wrong_value;
    }

    public void SetHasDefaultValue(bool has_default)
    {
        has_default_value_ = has_default;
    }

    public bool GetHasDefaultValue() { return has_default_value_; }

    public bool IsSelected() { return selected_; }
    public void SetSquareIndex(int index)
    {
        square_index_ = index;
    }

    public void SetCorrectNumber(int number)
    {
        //Debug.Log("**********************Inside SetCorrectNumberA function********************");
        correct_number_ = number;
        has_wrong_value = false;
        
        
        // while continue game
        if(number_ != 0 && number_ != correct_number_)
        {
            /*has_wrong_value = true;
            SetSquareColour(Color.white);
            //Debug.Log("Showing red circle A");
            image.sprite = Resources.Load<Sprite>("RedCircle");*/


            has_wrong_value = true;
            //Debug.Log("Showing red circle B");
            image.enabled = true;
            image.color = Color.red;
            image.sprite = Resources.Load<Sprite>("RedCircle");

        }
        // echo to del
        Debug.Log("hinttext.text = : " + hintText.text);
        if(hintText.text == "inputText")
        {
            image.enabled = false;  
        }

    }

    public void SetCorrectNumber()
    {

        //Debug.Log("**********************Inside SetCorrectNumberB function********************");
        number_ = correct_number_;
        SetNoteNumberValue(0);
        DisplayText();
    }


    public List<string> GetSquareNotes()
    {
        List<string> notes = new List<string>();

        foreach (var number in number_notes)
        {
            notes.Add(number.GetComponent<Text>().text);
        }

        return notes;

    }

    private void SetClearEmptyNotes()
    {
        //Debug.Log("GridSquare.Inside setclearemptynotes function");
        foreach (var number in number_notes)
        {
            if(number.GetComponent<Text>().text == "0")
            {
                number.GetComponent<Text>().text = " ";
            }
        }
    }

    private void SetNoteNumberValue(int value)
    {
        foreach (var number in number_notes)
        {
            if(value <= 0)
            {
                number.GetComponent<Text>().text = " ";
            }
            else
            {
                //Debug.Log("GridSquare.Inside setNoteNumber Value function");
                number.GetComponent<Text>().text = value.ToString();
                //Debug.Log("GridSquare.value = " + value.ToString());
            }
        }
    }

    private void SetNoteSingleNumberValue(int value, bool force_update = false)
    {
        if (note_active == false && force_update == false)
        {
            return;
        }

        if (value < 0)
        {
            //Debug.Log("GridSquare.Inside setnotesinglenumber Value function A");
            number_notes[value - 1].GetComponent<Text>().text = " ";
        }
        else
        {
            if(number_notes[value -1].GetComponent<Text>().text == " " || force_update)
            {
                
                number_notes[value - 1].GetComponent<Text>().color = Color.white;
                number_notes[value - 1].GetComponent<Text>().text = value.ToString();
                //Debug.Log("GridSquare. Inside set note single number value function +++ colour is => " + number_notes[value - 1].GetComponent<Text>().color);
                //Debug.Log("GridSquare.Inside setnotesinglenumber Value function ---> SETTING VALUE  = "  + value.ToString());
            }
            else
            {
                //Debug.Log("GridSquare.Inside setnotesinglenumber Value function B");
                number_notes[value - 1].GetComponent<Text>().text = " ";
            }
        }

    }


    public void SetGridNotes(List<int> notes)
    {
        foreach (var note in notes)
        {
            SetNoteSingleNumberValue(note, true);
        }
    }

    public void OnNoteActive(bool active)
    {
        note_active = active;
    }


    public void DisplayText()
    {
        //Debug.Log("**********************Inside DisplayText function********************");
        // This function is called when the game starts. The default cells are made white and the others are filled with canvas colour.
        // This function run after SetSquareNumber when a number is entered on the board.
        
        if (number_ <= 0)
        {
            number_text.GetComponent<Text>().text = " ";
            image.color = _backgroundColor;

            // to show highlight when selected, there must be a sprite 
            image.sprite = Resources.Load<Sprite>("Rectangle 2");
        }
        else
        {
            number_text.GetComponent<Text>().color = Color.black;
            number_text.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            number_text.GetComponent<Text>().text = number_.ToString();
            // change entered numbers to have light green sprite
            //image.sprite = Resources.Load<Sprite>("Grid_Circle_Light");
            image.enabled = false;
            

        }

        if (has_default_value_)
        {
            //number_text.GetComponent<Text>().fontStyle = FontStyle.Bold;

            //   // change default numbers to have dark green sprite
            //      image.sprite = Resources.Load<Sprite>("Grid_Circle_Dark");

            // change default numbers to have white sprite
            image.enabled = true;
            
            image.sprite = Resources.Load<Sprite>("WhiteCircle");
            // set sprite colour to white
            image.color = Color.white;

        }
    }


    public void SetNumber(int number)
    {
        number_ = number;
        DisplayText();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Clicked");
        selected_ = true;
        GameEvents.SquareSelectedMethod(square_index_);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        
    }

    private void OnEnable()
    {
        GameEvents.OnUpdateSquareNumber += OnSetNumber;
        GameEvents.OnSquareSelected += OnSquareSelected;
        GameEvents.OnNotesActive += OnNoteActive;
        GameEvents.OnClearNumber += OnClearNumber;
        GameEvents.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameEvents.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.OnSquareSelected -= OnSquareSelected;
        GameEvents.OnNotesActive -= OnNoteActive;
        GameEvents.OnClearNumber -= OnClearNumber;
        GameEvents.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        //this fn runs when game over pop up comes
        //Debug.Log("**********************Inside OnGameOver function********************");
        
        if (number_ != 0 && number_ != correct_number_)
        {
            has_wrong_value = false;
            number_ = 0;
            DisplayText();

        }

        //SetSquareColour(Color.white);
        
        if (!has_default_value_)
        {
            // To remove red circles when resuming after ad, iamge disabled
            // commenting below line for test Feb 5
            //image.sprite = Resources.Load<Sprite>("Rectangle 2");
            image.enabled = false;
            image.color = _backgroundColor;
        }
        
    }

    public void OnClearNumber()
    {
        if(selected_ && !has_default_value_)
        {
            number_ = 0;
            has_wrong_value = false;
            SetSquareColour(Color.white);
            // change sprite back to white 
            // comment below line Feb 5
                //image.sprite = Resources.Load<Sprite>("Rectangle 2");
                image.enabled = false;
            
            
            SetNoteNumberValue(0);
            DisplayText();
        }
    }

    public void SetCorrectValueOnHint()
    {
        SetSquareNumber(correct_number_);
    }

    public void OnSetNumber(int number)
    {
        if (selected_ && has_default_value_ == false)
        {
            SetSquareNumber(number);
        }
    }

    private void SetSquareNumber(int number)
    {
        // This function is first called when a number is entered on the board.
        //Debug.Log("**********************Inside SetSquareNumber function********************");
        //Debug.Log("gridSquare script");

        if (note_active == true && has_wrong_value == false)
        {
            SetNoteSingleNumberValue(number);
        }
        else if (note_active == false)
        {
            SetNoteNumberValue(0);
            SetNumber(number);

            if (number != correct_number_)
            {
                has_wrong_value = true;
                //Debug.Log("Showing red circle B");
                image.enabled = true;
                image.color = Color.red;
                image.sprite = Resources.Load<Sprite>("RedCircle");

                //var colors = this.colors;
                //colors.normalColor = Color.red; // can change error colour
                //this.colors = colors;

                // change sprite back to white 
                //image.sprite = Resources.Load<Sprite>("WhiteCircle");

                GameEvents.OnWrongNumberMethod();
            }
            else
            {
                has_wrong_value = false;
                has_default_value_ = true;
                hintText.text = "inputText";

                //var colors = this.colors;
                //colors.normalColor = Color.white;
                //this.colors = colors;
                //image.sprite = Resources.Load<Sprite>("WhiteCircle");

                hintToggle = PlayerPrefs.GetInt("hintToggle");
                if (hintToggle == 1)
                {
                    // change hint numbers to have white sprite + alpha 50%
                    image.enabled = true;
                    image.sprite = Resources.Load<Sprite>("HintCircle");
                    //image.color = Color.white;
                    image.color = Color.gray;
                    hintText.text = "hintText";
                    PlayerPrefs.SetInt("hintToggle", 0);                    
                }

            }
        }

        GameEvents.CheckBoardCompletedMethod();
    }

    public void OnSquareSelected(int square_index)
    {
        if (square_index_ != square_index)
        {
            selected_ = false;
        }
    }


    public void SetSquareColour(Color col)
    {
        var colors = this.colors;
        colors.normalColor = col;
        this.colors = colors;
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject number_text;
    public List<GameObject> number_notes;
    private bool note_active;
    private int number_ = 0;
    private int correct_number_ = 0;

    private bool selected_ = false;
    private int square_index_ = -1;
    private bool has_default_value_ = false;
    private bool has_wrong_value = false;

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
        correct_number_ = number;
        has_wrong_value = false;
        
        if(number_ != 0 && number_ != correct_number_)
        {
            has_wrong_value = true;
            SetSquareColour(Color.red);
        }
    }

    public void SetCorrectNumber()
    {
        number_ = correct_number_;
        SetNoteNumberValue(0);
        DisplayText();
    }

    void Start()
    {
        selected_ = false;
        note_active = false;
        if (GameSettings.Instance.GetContinuePreviousGame() == false)
        {
            SetNoteNumberValue(0);
        }
        else
        {
            SetClearEmptyNotes();
        }
        
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
                number.GetComponent<Text>().text = value.ToString();
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
            number_notes[value - 1].GetComponent<Text>().text = " ";
        }
        else
        {
            if(number_notes[value -1].GetComponent<Text>().text == " " || force_update)
            {
                number_notes[value - 1].GetComponent<Text>().text = value.ToString();
            }
            else
            {
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
        if (number_ <= 0)
        {
            number_text.GetComponent<Text>().text = " ";
        }
        else
        {
            number_text.GetComponent<Text>().text = number_.ToString();
            // change entered numbers to have light green sprite
            image.sprite = Resources.Load<Sprite>("Grid_Circle_Light");
        }

        if (has_default_value_)
        {
            number_text.GetComponent<Text>().fontStyle = FontStyle.Bold;
            // change default numbers to have dark green sprite
            image.sprite = Resources.Load<Sprite>("Grid_Circle_Dark");
        }
    }


    public void SetNumber(int number)
    {
        number_ = number;
        DisplayText();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
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
        if(number_ != 0 && number_ != correct_number_)
        {
            has_wrong_value = false;
            number_ = 0;
            DisplayText();
        }

        SetSquareColour(Color.white);
        if (!has_default_value_)
        {
            // change sprite back to white 
            image.sprite = Resources.Load<Sprite>("Rectangle 2");
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
            image.sprite = Resources.Load<Sprite>("Rectangle 2");
            //
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
                var colors = this.colors;
                colors.normalColor = Color.red; // can change error colour
                this.colors = colors;
                // change sprite back to white 
                image.sprite = Resources.Load<Sprite>("cylinder");

                GameEvents.OnWrongNumberMethod();
            }
            else
            {
                has_wrong_value = false;
                has_default_value_ = true;
                var colors = this.colors;
                colors.normalColor = Color.white;
                this.colors = colors;
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

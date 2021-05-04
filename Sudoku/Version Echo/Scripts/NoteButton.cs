using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteButton : Selectable, IPointerClickHandler
{
    public Sprite on_image;
    public Sprite off_image;
    public Image subImage;
    
    private Color _backgroundColor;
    private int prevCanvasColourValue;

    //List<string> colourList = new List<string>() { "#2D3A49", "#F57F4F", "#32533D", "#E8B447", "#B7386E" };
    List<string> colourList = new List<string>() { "#3E7EC7", "#F57F4F", "#497A5A", "#E8B447", "#DE4587" };

    private bool active_;

    void Start()
    {
        //Debug.Log("Notebutton. Start method");
        active_ = false;
        prevCanvasColourValue = PlayerPrefs.GetInt("canvasColour");
        //PlayerPrefs.SetInt("notebuttonstate", -1);
        //Debug.Log("NoteButton.Start state " + PlayerPrefs.GetInt("notebuttonstate"));
        ColorUtility.TryParseHtmlString(colourList[prevCanvasColourValue], out _backgroundColor);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        active_ = !active_;

        if (active_)
        {
            //Debug.Log("NoteButton.notebutton activated");
            GetComponent<Image>().sprite = on_image;
            subImage.color = _backgroundColor;
            //PlayerPrefs.SetInt("notebuttonstate", 1);
            
        }
        else
        {
            //Debug.Log("NoteButton.notebutton deactivated");
            //PlayerPrefs.SetInt("notebuttonstate", 0);
            GetComponent<Image>().sprite = off_image;
            subImage.color = Color.white;
        }

        GameEvents.OnNotesActiveMethod(active_);
    }
}
